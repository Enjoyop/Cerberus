﻿using System;
using System.Collections.Concurrent;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CRepublic.Magic.Core.Database;
using CRepublic.Magic.Extensions;
using CRepublic.Magic.Logic;
using Newtonsoft.Json;
using CRepublic.Magic.Logic.Enums;

namespace CRepublic.Magic.Core
{
    internal class Players : ConcurrentDictionary<long, Level>
    {
        internal JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling            = TypeNameHandling.Auto,            MissingMemberHandling   = MissingMemberHandling.Ignore,
            DefaultValueHandling        = DefaultValueHandling.Include,     NullValueHandling       = NullValueHandling.Ignore,
            PreserveReferencesHandling  = PreserveReferencesHandling.All,   ReferenceLoopHandling   = ReferenceLoopHandling.Ignore,
            Formatting                  = Formatting.Indented,              Converters              = { new Utils.ArrayReferencePreservngConverter() },
        };

        internal long Seed;
        internal object Gate = new object();
        internal object GateAdd = new object();

        internal Players()
        {
        }

        internal void Add(Level Player)
        {
            lock (this.GateAdd)
            {
                if (this.ContainsKey(Player.Avatar.UserId))
                {
                    this[Player.Avatar.UserId] = Player;
                }
                else
                {
                    this.TryAdd(Player.Avatar.UserId, Player);
                }
            }
        }

        internal void Remove(Level Player)
        {
            if (Player != null)
            {
                Player.Tick();
                this.Save(Player, Constants.Database).Wait();

                this.TryRemove(Player.Avatar.UserId);

                if (Player.Client != null)
                {
                    if (Resources.Devices.ContainsKey(Player.Client.SocketHandle))
                    {
                        Resources.Devices.Remove(Player.Client.SocketHandle);
                    }
                    Resources.GChat.Remove(Player.Client);
                }
            }
        }

        internal Level Get(long UserId, DBMS DBMS = DBMS.Mysql, bool Store = true)
        {
            if (!this.ContainsKey(UserId))
            {
                Level Player = null;

                switch (DBMS)
                {
                    case DBMS.Mysql:
                        using (MysqlEntities Database = new MysqlEntities())
                        {
                            var Data = Database.Player.Find(UserId);

                            if (!string.IsNullOrEmpty(Data?.Data))
                            {
                                string[] _Datas =
                                    Data.Data.Split(new string[1] {"#:#:#:#"}, StringSplitOptions.None);

                                if (!string.IsNullOrEmpty(_Datas[0]) && !string.IsNullOrEmpty(_Datas[1]))
                                {
                                    Player = new Level
                                    {
                                        Avatar = JsonConvert.DeserializeObject<Logic.Player>(_Datas[0], this.Settings),
                                        JSON = _Datas[1],
                                    };

                                    if (Store)
                                    {
                                        this.Add(Player);
                                    }
                                }
                            }
                        }
                        break;
                    case DBMS.Redis:
                        string Property = Redis.Players.StringGet(UserId.ToString()).ToString();

                        if (!string.IsNullOrEmpty(Property))
                        {
                            string[] _Datas = Property.Split(new string[1] {"#:#:#:#"}, StringSplitOptions.None);

                            if (!string.IsNullOrEmpty(_Datas[0]) && !string.IsNullOrEmpty(_Datas[1]))
                            {
                                Player = new Level
                                {
                                    Avatar = JsonConvert.DeserializeObject<Logic.Player>(_Datas[0], this.Settings),
                                    JSON = _Datas[1],
                                };

                                if (Store)
                                {
                                    this.Add(Player);
                                }
                            }
                        }
                        break;
                    case DBMS.Both:
                        Player = this.Get(UserId, DBMS.Redis, Store);

                        if (Player == null)
                        {
                            Player = this.Get(UserId, DBMS.Mysql, Store);
                            if (Player != null)
                                Redis.Players.StringSet(Player.Avatar.UserId.ToString(),
                                    JsonConvert.SerializeObject(Player.Avatar, this.Settings) + "#:#:#:#" + Player.JSON,
                                    TimeSpan.FromHours(4));
                        }
                        break;
                }
                return Player;
            }
            return this[UserId];
        }

        internal Level New(long UserId = 0, DBMS DBMS = DBMS.Mysql, bool Store = true)
        {
            Level Player = null;

            if (UserId == 0)
            {
                lock (this.Gate)
                {
                    Player = new Level(this.Seed++);
                }
            }
            else
            {
                Player = new Level(UserId);
            }

            if (string.IsNullOrEmpty(Player.Avatar.Token))
            {
                for (int i = 0; i < 20; i++)
                {
                    char Letter = (char)Resources.Random.Next('A', 'Z');
                    Player.Avatar.Token += Letter;
                }
            }
            if (string.IsNullOrEmpty(Player.Avatar.Password))
            {
                for (int i = 0; i < 6; i++)
                {
                    char Letter = (char)Resources.Random.Next('A', 'Z');
                    char Number = (char)Resources.Random.Next('1', '9');
                    Player.Avatar.Password += Letter;
                    Player.Avatar.Password += Number;
                }
            }
            Player.JSON = Files.Home.Starting_Home;

            while (true)
            {
                switch (DBMS)
                {
                    case DBMS.Mysql:
                    {
                        using (MysqlEntities Database = new MysqlEntities())
                        {
                            Database.Player.Add(new Database.Player
                            {
                                ID = Player.Avatar.UserId,
                                Data = JsonConvert.SerializeObject(Player.Avatar, this.Settings) + "#:#:#:#" +
                                       Player.JSON,
                                FacebookID = "#:#:#:#",
                            });

                            Database.SaveChanges();
                        }

                        if (Store)
                        {
                            this.Add(Player);
                        }
                        break;
                    }

                    case DBMS.Redis:
                    {
                        Redis.Players.StringSet(Player.Avatar.UserId.ToString(),
                            JsonConvert.SerializeObject(Player.Avatar, this.Settings) + "#:#:#:#" + Player.JSON,
                            TimeSpan.FromHours(4));


                        if (Store)
                        {
                            this.Add(Player);
                        }
                        break;
                    }

                    case DBMS.Both:
                    {
                        Redis.Players.StringSet(Player.Avatar.UserId.ToString(),
                            JsonConvert.SerializeObject(Player.Avatar, this.Settings) + "#:#:#:#" + Player.JSON,
                            TimeSpan.FromHours(4));
                        DBMS = DBMS.Mysql;

                        if (Store)
                        {
                            this.Add(Player);
                        }

                        continue;
                    }
                }
                break;
            }

            return Player;
        }

        internal async Task Save(Level Player, DBMS DBMS = DBMS.Mysql)
        {
            Player.Avatar.LastSave = DateTime.UtcNow;
            while (true)
            {
                switch (DBMS)
                {
                    case DBMS.Mysql:
                    {

                        using (MysqlEntities Database = new MysqlEntities())
                        {
                            Database.Configuration.AutoDetectChangesEnabled = false;
                            Database.Configuration.ValidateOnSaveEnabled = false;
                            var Data = await Database.Player.FindAsync(Player.Avatar.UserId);

                            if (Data != null)
                            {
                                Data.Data = JsonConvert.SerializeObject(Player.Avatar, this.Settings) + "#:#:#:#" +
                                            Player.JSON;
                                Data.Trophies = Player.Avatar.Trophies;
                                Data.FacebookID = Player.Avatar.Facebook.Identifier ?? "#:#:#:#";
                                Database.Entry(Data).State = EntityState.Modified;
                            }

                            await Database.SaveChangesAsync();
                        }
                        break;
                    }

                    case DBMS.Redis:
                    {
                        await Redis.Players.StringSetAsync(Player.Avatar.UserId.ToString(),
                            JsonConvert.SerializeObject(Player.Avatar, this.Settings) + "#:#:#:#" + Player.JSON,
                            TimeSpan.FromHours(4));
                        break;
                    }

                    case DBMS.Both:
                    {
                        await this.Save(Player);
                        DBMS = DBMS.Redis;
                        continue;
                    }
                }
                break;
            }
        }

        internal async Task Save(DBMS DBMS = DBMS.Mysql)
        {
            while (true)
            {
                switch (DBMS)
                {
                    case DBMS.Mysql:
                    {

                        using (MysqlEntities Database = new MysqlEntities())
                        {
                            Database.Configuration.AutoDetectChangesEnabled = false;
                            Database.Configuration.ValidateOnSaveEnabled = false;
                            foreach (var Player in this.Values.ToList())
                            {
                                lock (Player)
                                {
                                    Player.Avatar.LastSave = DateTime.UtcNow;
                                    var Data = Database.Player.Find(Player.Avatar.UserId);

                                    if (Data != null)
                                    {
                                        Data.Data = JsonConvert.SerializeObject(Player.Avatar, this.Settings) + "#:#:#:#" + Player.JSON;
                                        Data.Trophies = Player.Avatar.Trophies;
                                        Data.FacebookID = Player.Avatar.Facebook.Identifier ?? "#:#:#:#";
                                        Database.Entry(Data).State = EntityState.Modified;
                                    }
                                }
                            }

                            await Database.SaveChangesAsync();
                        }

                        break;
                    }

                    case DBMS.Redis:
                    {
                        foreach (var Player in this.Values.ToList())
                        {
                            await Redis.Players.StringSetAsync(Player.Avatar.UserId.ToString(),
                                JsonConvert.SerializeObject(Player.Avatar, this.Settings) + "#:#:#:#" + Player.JSON,
                                TimeSpan.FromHours(4));
                        }
                        break;
                    }

                    case DBMS.Both:
                    {
                        await this.Save();
                        DBMS = DBMS.Redis;
                        continue;
                    }
                }
                break;
            }
        }
    }
}
