﻿using System;
using System.Collections.Generic;
using BL.Servers.CoC.Extensions;
using BL.Servers.CoC.Files;
using BL.Servers.CoC.Logic.Enums;

namespace BL.Servers.CoC.Logic.Structure
{
    using BL.Servers.CoC.Files.CSV_Helpers;
    using BL.Servers.CoC.Files.CSV_Logic;
    using Newtonsoft.Json.Linq;

    internal class Obstacle : GameObject
    {
        public Obstacle(Data data, Level l) : base(data, l)
        {
        }

        internal override int ClassId => 3;

        internal Obstacles GetObstacleData() => (Obstacles)GetData();
        internal Timer Timer;
        internal bool IsClearing;

        internal void CancelClearing()
        {
            if (!IsClearing)
                throw new InvalidOperationException("Obstacle object is not being cleared.");

            this.Level.WorkerManager.DeallocateWorker(this);
            this.IsClearing = false;
            this.Timer = null;
            var od = GetObstacleData();
            this.Level.Avatar.Resources.ResourceChangeHelper(od.GetClearingResource().GetGlobalID(), od.ClearCost);
        }

        internal void StartClearing()
        {
            var constructionTime = GetObstacleData().ClearTimeSeconds;
            if (constructionTime < 1)
            {
                ClearingFinished();
            }
            else
            {
                this.Timer = new Timer();
                this.IsClearing = true;
                this.Timer.StartTimer(this.Level.Avatar.LastTick, constructionTime);
                this.Level.WorkerManager.AllocateWorker(this);
            }
        }

        internal readonly int[] GemDrops =
        {
            3, 0, 1, 2, 0, 1, 1, 0, 0, 3,
            1, 0, 2, 2, 0, 0, 3, 0, 1, 0
        };

        internal DateTime ClearEndTime
        {
            get
            {
                if (!IsClearing)
                    throw new InvalidOperationException("Obstacle object is not clearing.");

                return TimeUtils.FromUnixTimestamp(this.Timer.EndTime);
            }
        }

        internal void ClearingFinished()
        {
            this.Level.WorkerManager.DeallocateWorker(this);
            this.IsClearing = false;
            this.Timer = null;
            var constructionTime = GetObstacleData().ClearTimeSeconds;
            var exp = (int)Math.Pow(constructionTime, 0.5f);

            var gems = this.GemDrops[this.Level.Avatar.ObstacleClearCount++];
            if (this.Level.Avatar.ObstacleClearCount >= this.GemDrops.Length)
                this.Level.Avatar.ObstacleClearCount = 0;

            this.Level.Avatar.AddExperience(exp);
            this.Level.Avatar.Resources.Plus(Enums.Resource.Diamonds, gems);

            var rd = CSV.Tables.Get(Gamefile.Resources).GetData(GetObstacleData().LootResource);

            this.Level.Avatar.Resources.ResourceChangeHelper(rd.GetGlobalID(), GetObstacleData().LootCount);

            this.Level.GameObjectManager.RemoveGameObject(this);
        }

        internal int GetRemainingClearingTime()
        {
            return this.Timer.GetRemainingSeconds(this.Level.Avatar.LastTick);
        }

        public new void Load(JObject jsonObject)
        {
            var remTimeToken = jsonObject["clear_t"];
            var remTimeEndToken = jsonObject["clear_t_end"];
            if (remTimeToken != null && remTimeEndToken != null)
            {
                this.Timer = new Timer();
                this.IsClearing = true;
                var remainingClearingEndTime = remTimeEndToken.ToObject<int>();
                var startTime = (int)TimeUtils.ToUnixTimestamp(this.Level.Avatar.LastTick);
                var duration = remainingClearingEndTime - startTime;

                if (duration < 0)
                    duration = 0;

                this.Timer.StartTimer(this.Level.Avatar.LastTick, duration);
                this.Level.WorkerManager.AllocateWorker(this);
            }
            base.Load(jsonObject);
        }

        public new JObject Save(JObject jsonObject)
        {
            if (this.IsClearing)
            {
                jsonObject.Add("clear_t", this.Timer.GetRemainingSeconds(this.Level.Avatar.LastTick));
                jsonObject.Add("clear_t_end", this.Timer.EndTime);
            }
            base.Save(jsonObject);
            return jsonObject;
        }

        public override void Tick()
        {
            base.Tick();
            if (this.IsClearing)
            {
                if (this.Timer.GetRemainingSeconds(this.Level.Avatar.LastTick) <= 0)
                    ClearingFinished();
            }
        }
    }
}