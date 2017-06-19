﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Republic.Magic.Extensions.List;
using Republic.Magic.Logic;
using Republic.Magic.Extensions;
using Republic.Magic.Logic.Structure.Slots;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Republic.Magic.Packets.Messages.Server.Battle
{
    internal class Pc_Battle_Data_V2 : Message
    {
        internal JsonSerializerSettings Client_JsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None,
        };

        internal Level Enemy;
        internal JObject EnemyObject;

        public Pc_Battle_Data_V2(Device Device, Level Enemy) : base(Device)
        {
            this.Identifier = 25023;
            this.Enemy = Enemy;
            this.EnemyObject = Enemy.GameObjectManager.JSON;
        }

        internal override void Encode()
        {
            //this.Data.AddHexa("00 00 00 0D 00 21 97 30 00 00 00 0D 00 21 97 30 01 00 00 00 23 00 48 1D 98 00 00 00 0D 42 61 72 62 61 72 69 61 6E 6C 61 6E 64 00 00 00 00 00 00 00 02 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 07 00 00 00 06 00 00 00 08 00 00 00 00 00 00 00 03 00 00 00 19 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 09 00 00 00 02 00 00 00 08 49 6E 66 69 6E 69 74 65 00 00 00 0F 31 30 30 30 30 31 32 33 30 34 35 32 37 34 34 00 00 00 66 00 00 0C F4 00 00 00 02 00 00 00 02 00 00 04 B0 00 00 00 3C 00 00 08 2E 00 00 00 EC 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 01 52 02 E8 A8 A0 03 00 00 00 01 00 00 1A F4 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 08 00 2D C6 C1 00 81 B3 20 00 2D C6 C2 00 81 B3 20 00 2D C6 C3 00 02 71 00 00 2D C6 C4 00 1E 84 80 00 2D C6 C5 00 1E 84 80 00 2D C6 C6 00 00 27 10 00 2D C6 C7 00 01 86 A0 00 2D C6 C8 00 01 86 A0 00 00 00 08 00 2D C6 C1 00 0A 82 B6 00 2D C6 C2 00 0A 82 40 00 2D C6 C3 00 00 10 8A 00 2D C6 C4 00 00 00 00 00 2D C6 C5 00 00 00 00 00 2D C6 C6 00 00 00 00 00 2D C6 C7 00 00 12 2D 00 2D C6 C8 00 01 62 A9 00 00 00 0F 00 3D 09 00 00 00 00 1A 00 3D 09 01 00 00 00 00 00 3D 09 02 00 00 00 00 00 3D 09 03 00 00 00 01 00 3D 09 04 00 00 00 01 00 3D 09 05 00 00 00 00 00 3D 09 06 00 00 00 01 00 3D 09 07 00 00 00 00 00 3D 09 08 00 00 00 00 00 3D 09 09 00 00 00 00 00 3D 09 0A 00 00 00 00 00 3D 09 0B 00 00 00 00 00 3D 09 0C 00 00 00 00 00 3D 09 0D 00 00 00 00 00 3D 09 0F 00 00 00 00 00 00 00 06 01 8C BA 80 00 00 00 01 01 8C BA 81 00 00 00 01 01 8C BA 82 00 00 00 01 01 8C BA 83 00 00 00 01 01 8C BA 85 00 00 00 01 01 8C BA 89 00 00 00 01 00 00 00 0D 00 3D 09 00 00 00 00 05 00 3D 09 01 00 00 00 05 00 3D 09 02 00 00 00 04 00 3D 09 03 00 00 00 05 00 3D 09 04 00 00 00 04 00 3D 09 05 00 00 00 04 00 3D 09 06 00 00 00 04 00 3D 09 07 00 00 00 02 00 3D 09 08 00 00 00 02 00 3D 09 09 00 00 00 01 00 3D 09 0A 00 00 00 02 00 3D 09 0B 00 00 00 03 00 3D 09 1F 00 00 00 01 00 00 00 03 01 8C BA 80 00 00 00 03 01 8C BA 81 00 00 00 04 01 8C BA 82 00 00 00 04 00 00 00 02 01 AB 3F 00 00 00 00 0A 01 AB 3F 01 00 00 00 08 00 00 00 02 01 AB 3F 00 00 00 00 00 01 AB 3F 01 00 00 00 00 00 00 00 02 01 AB 3F 00 00 00 00 03 01 AB 3F 01 00 00 00 03 00 00 00 48 00 3D 09 00 00 00 00 00 00 00 00 00 00 3D 09 00 00 00 00 00 00 00 00 01 00 3D 09 00 00 00 00 00 00 00 00 02 00 3D 09 00 00 00 00 00 00 00 00 03 00 3D 09 00 00 00 00 00 00 00 00 04 00 3D 09 00 00 00 00 00 00 00 00 05 00 3D 09 00 00 00 00 00 00 00 00 06 00 3D 09 01 00 00 00 00 00 00 00 00 00 3D 09 01 00 00 00 00 00 00 00 01 00 3D 09 01 00 00 00 00 00 00 00 02 00 3D 09 01 00 00 00 00 00 00 00 03 00 3D 09 01 00 00 00 00 00 00 00 04 00 3D 09 01 00 00 00 00 00 00 00 05 00 3D 09 01 00 00 00 00 00 00 00 06 00 3D 09 02 00 00 00 00 00 00 00 00 00 3D 09 02 00 00 00 00 00 00 00 01 00 3D 09 02 00 00 00 00 00 00 00 02 00 3D 09 02 00 00 00 00 00 00 00 03 00 3D 09 02 00 00 00 00 00 00 00 04 00 3D 09 03 00 00 00 00 00 00 00 00 00 3D 09 03 00 00 00 00 00 00 00 01 00 3D 09 03 00 00 00 00 00 00 00 02 00 3D 09 03 00 00 00 00 00 00 00 03 00 3D 09 03 00 00 00 00 00 00 00 04 00 3D 09 03 00 00 00 00 00 00 00 05 00 3D 09 03 00 00 00 00 00 00 00 06 00 3D 09 04 00 00 00 00 00 00 00 00 00 3D 09 04 00 00 00 00 00 00 00 01 00 3D 09 04 00 00 00 00 00 00 00 02 00 3D 09 04 00 00 00 00 00 00 00 03 00 3D 09 04 00 00 00 00 00 00 00 04 00 3D 09 05 00 00 00 00 00 00 00 00 00 3D 09 05 00 00 00 00 00 00 00 01 00 3D 09 05 00 00 00 00 00 00 00 02 00 3D 09 05 00 00 00 00 00 00 00 03 00 3D 09 05 00 00 00 00 00 00 00 04 00 3D 09 05 00 00 00 00 00 00 00 05 00 3D 09 06 00 00 00 00 00 00 00 00 00 3D 09 06 00 00 00 00 00 00 00 01 00 3D 09 06 00 00 00 00 00 00 00 02 00 3D 09 06 00 00 00 00 00 00 00 03 00 3D 09 06 00 00 00 00 00 00 00 04 00 3D 09 06 00 00 00 00 00 00 00 05 00 3D 09 07 00 00 00 00 00 00 00 00 00 3D 09 07 00 00 00 00 00 00 00 01 00 3D 09 07 00 00 00 00 00 00 00 02 00 3D 09 08 00 00 00 00 00 00 00 00 00 3D 09 08 00 00 00 00 00 00 00 01 00 3D 09 08 00 00 00 00 00 00 00 02 00 3D 09 08 00 00 00 00 00 00 00 03 00 3D 09 08 00 00 00 00 00 00 00 04 00 3D 09 09 00 00 00 00 00 00 00 00 00 3D 09 09 00 00 00 00 00 00 00 01 00 3D 09 09 00 00 00 00 00 00 00 02 00 3D 09 09 00 00 00 00 00 00 00 03 00 3D 09 09 00 00 00 00 00 00 00 04 00 3D 09 0A 00 00 00 00 00 00 00 00 00 3D 09 0A 00 00 00 00 00 00 00 01 00 3D 09 0A 00 00 00 00 00 00 00 02 00 3D 09 0A 00 00 00 00 00 00 00 03 00 3D 09 0A 00 00 00 00 00 00 00 04 00 3D 09 0A 00 00 00 00 00 00 00 05 00 3D 09 0B 00 00 00 00 00 00 00 00 00 3D 09 0B 00 00 00 00 00 00 00 01 00 3D 09 0B 00 00 00 00 00 00 00 02 00 3D 09 0B 00 00 00 00 00 00 00 03 00 3D 09 0B 00 00 00 00 00 00 00 04 00 3D 09 0C 00 00 00 00 00 00 00 00 00 3D 09 0F 00 00 00 00 00 00 00 00 00 3D 09 0F 00 00 00 00 00 00 00 01 01 8C BA 89 00 00 00 00 00 00 00 01 01 8C BA 89 00 00 00 00 00 00 00 02 00 00 00 19 01 40 6F 40 01 40 6F 41 01 40 6F 42 01 40 6F 43 01 40 6F 44 01 40 6F 45 01 40 6F 46 01 40 6F 47 01 40 6F 48 01 40 6F 49 01 40 6F 4A 01 40 6F 4B 01 40 6F 4C 01 40 6F 4D 01 40 6F 4E 01 40 6F 4F 01 40 6F 51 01 40 6F 52 01 40 6F 53 01 40 6F 54 01 40 6F 55 01 40 6F 56 01 40 6F 57 01 40 6F 58 01 40 6F 59 00 00 00 38 01 5E F3 C0 01 5E F3 C1 01 5E F3 C2 01 5E F3 C3 01 5E F3 C4 01 5E F3 C5 01 5E F3 C6 01 5E F3 C7 01 5E F3 C8 01 5E F3 C9 01 5E F3 CA 01 5E F3 CB 01 5E F3 CC 01 5E F3 CD 01 5E F3 CE 01 5E F3 CF 01 5E F3 D0 01 5E F3 D1 01 5E F3 D2 01 5E F3 D3 01 5E F3 D4 01 5E F3 D5 01 5E F3 D6 01 5E F3 D7 01 5E F3 D8 01 5E F3 D9 01 5E F3 DA 01 5E F3 DB 01 5E F3 DC 01 5E F3 DD 01 5E F3 DE 01 5E F3 DF 01 5E F3 E1 01 5E F3 E2 01 5E F3 E3 01 5E F3 E4 01 5E F3 E5 01 5E F3 E7 01 5E F3 E8 01 5E F3 EA 01 5E F3 EB 01 5E F3 EC 01 5E F3 ED 01 5E F3 EE 01 5E F3 F0 01 5E F3 F1 01 5E F3 F3 01 5E F3 F4 01 5E F3 F6 01 5E F3 F7 01 5E F3 F9 01 5E F3 FC 01 5E F3 FD 01 5E F3 FF 01 5E F4 08 01 5E F4 0F 00 00 00 53 01 5E F3 C0 00 00 00 0B 01 5E F3 C1 00 00 00 0B 01 5E F3 C2 00 00 00 0B 01 5E F3 C3 00 00 00 99 01 5E F3 C4 00 00 00 99 01 5E F3 C5 00 00 00 99 01 5E F3 C6 00 00 00 0A 01 5E F3 C7 00 00 00 0A 01 5E F3 C8 00 00 00 0A 01 5E F3 C9 00 00 05 61 01 5E F3 CA 00 00 05 61 01 5E F3 CB 00 00 05 61 01 5E F3 CC 00 00 00 01 01 5E F3 CD 00 00 00 01 01 5E F3 CE 00 00 00 01 01 5E F3 CF 13 80 93 0F 01 5E F3 D0 13 80 93 0F 01 5E F3 D1 13 80 93 0F 01 5E F3 D2 16 5F 94 6D 01 5E F3 D3 16 5F 94 6D 01 5E F3 D4 16 5F 94 6D 01 5E F3 D5 00 00 0B 79 01 5E F3 D6 00 00 0B 79 01 5E F3 D7 00 00 0B 79 01 5E F3 D8 00 00 00 04 01 5E F3 D9 00 00 00 04 01 5E F3 DA 00 00 00 04 01 5E F3 DB 00 00 55 D7 01 5E F3 DC 00 00 55 D7 01 5E F3 DD 00 00 55 D7 01 5E F3 DE 00 00 06 6D 01 5E F3 DF 00 00 06 6D 01 5E F3 E0 00 00 06 6D 01 5E F3 E1 00 00 1B 7F 01 5E F3 E2 00 00 1B 7F 01 5E F3 E3 00 00 1B 7F 01 5E F3 E4 00 00 09 D3 01 5E F3 E5 00 00 09 D3 01 5E F3 E6 00 00 09 D3 01 5E F3 E7 00 00 05 03 01 5E F3 E8 00 00 05 03 01 5E F3 E9 00 00 05 03 01 5E F3 EA 00 00 A2 F9 01 5E F3 EB 00 00 A2 F9 01 5E F3 EC 00 00 A2 F9 01 5E F3 ED 00 00 0D 02 01 5E F3 EE 00 00 0D 02 01 5E F3 EF 00 00 0D 02 01 5E F3 F0 00 0F 29 D9 01 5E F3 F1 00 0F 29 D9 01 5E F3 F2 00 0F 29 D9 01 5E F3 F3 00 00 00 0E 01 5E F3 F4 00 00 00 0E 01 5E F3 F5 00 00 00 0E 01 5E F3 F6 00 00 02 10 01 5E F3 F7 00 00 02 10 01 5E F3 F8 00 00 02 10 01 5E F3 F9 00 00 00 6B 01 5E F3 FA 00 00 00 6B 01 5E F3 FB 00 00 00 6B 01 5E F3 FC 00 00 00 EC 01 5E F3 FD 00 00 00 EC 01 5E F3 FE 00 00 00 EC 01 5E F3 FF 00 DE 17 F2 01 5E F4 00 00 DE 17 F2 01 5E F4 01 00 DE 17 F2 01 5E F4 02 00 00 00 01 01 5E F4 03 00 00 00 01 01 5E F4 04 00 00 00 01 01 5E F4 05 00 00 00 03 01 5E F4 06 00 00 00 03 01 5E F4 07 00 00 00 03 01 5E F4 08 00 00 00 01 01 5E F4 09 00 00 00 03 01 5E F4 0A 00 00 00 03 01 5E F4 0B 00 00 00 03 01 5E F4 0C 00 00 00 01 01 5E F4 0F 00 00 00 0D 01 5E F4 10 00 00 00 0D 01 5E F4 11 00 00 00 0D 01 5E F4 12 00 00 00 FB 01 5E F4 13 00 00 00 FB 01 5E F4 14 00 00 00 FB 00 00 00 33 01 03 66 40 00 00 00 03 01 03 66 41 00 00 00 03 01 03 66 42 00 00 00 03 01 03 66 43 00 00 00 03 01 03 66 44 00 00 00 03 01 03 66 45 00 00 00 03 01 03 66 46 00 00 00 03 01 03 66 47 00 00 00 03 01 03 66 48 00 00 00 03 01 03 66 49 00 00 00 03 01 03 66 4A 00 00 00 03 01 03 66 4B 00 00 00 03 01 03 66 4C 00 00 00 03 01 03 66 4D 00 00 00 03 01 03 66 4E 00 00 00 03 01 03 66 4F 00 00 00 03 01 03 66 50 00 00 00 03 01 03 66 51 00 00 00 03 01 03 66 52 00 00 00 03 01 03 66 53 00 00 00 03 01 03 66 54 00 00 00 03 01 03 66 55 00 00 00 03 01 03 66 56 00 00 00 03 01 03 66 57 00 00 00 03 01 03 66 58 00 00 00 03 01 03 66 59 00 00 00 03 01 03 66 5A 00 00 00 03 01 03 66 5B 00 00 00 03 01 03 66 5C 00 00 00 03 01 03 66 5D 00 00 00 03 01 03 66 5E 00 00 00 03 01 03 66 5F 00 00 00 03 01 03 66 60 00 00 00 03 01 03 66 61 00 00 00 03 01 03 66 62 00 00 00 03 01 03 66 63 00 00 00 03 01 03 66 64 00 00 00 03 01 03 66 65 00 00 00 03 01 03 66 66 00 00 00 03 01 03 66 67 00 00 00 03 01 03 66 68 00 00 00 03 01 03 66 69 00 00 00 03 01 03 66 6A 00 00 00 03 01 03 66 6B 00 00 00 03 01 03 66 6C 00 00 00 03 01 03 66 6D 00 00 00 03 01 03 66 6E 00 00 00 03 01 03 66 6F 00 00 00 03 01 03 66 70 00 00 00 03 01 03 66 71 00 00 00 03 01 03 66 72 00 00 00 03 00 00 00 32 01 03 66 40 00 00 01 F4 01 03 66 41 00 00 01 F4 01 03 66 42 00 00 01 F4 01 03 66 43 00 00 03 E8 01 03 66 44 00 00 03 E8 01 03 66 45 00 00 03 E8 01 03 66 46 00 00 07 D0 01 03 66 47 00 00 07 D0 01 03 66 48 00 00 07 D0 01 03 66 49 00 00 0B B8 01 03 66 4A 00 00 0B B8 01 03 66 4B 00 00 0B B8 01 03 66 4C 00 00 0F A0 01 03 66 4D 00 00 0F A0 01 03 66 4E 00 00 0F A0 01 03 66 4F 00 00 13 88 01 03 66 50 00 00 13 88 01 03 66 51 00 00 13 88 01 03 66 52 00 00 17 70 01 03 66 53 00 00 17 70 01 03 66 54 00 00 17 70 01 03 66 55 00 00 1B 58 01 03 66 56 00 00 1B 58 01 03 66 57 00 00 27 10 01 03 66 58 00 00 27 10 01 03 66 59 00 00 27 10 01 03 66 5A 00 00 3A 98 01 03 66 5B 00 00 3A 98 01 03 66 5C 00 00 3A 98 01 03 66 5D 00 00 4E 20 01 03 66 5E 00 00 4E 20 01 03 66 5F 00 00 4E 20 01 03 66 60 00 00 75 30 01 03 66 61 00 00 75 30 01 03 66 62 00 00 75 30 01 03 66 63 00 00 C3 50 01 03 66 64 00 01 86 A0 01 03 66 65 00 01 86 A0 01 03 66 66 00 01 86 A0 01 03 66 67 00 02 49 F0 01 03 66 68 00 02 49 F0 01 03 66 69 00 02 49 F0 01 03 66 6A 00 03 0D 40 01 03 66 6B 00 03 D0 90 01 03 66 6C 00 04 93 E0 01 03 66 6D 00 06 1A 80 01 03 66 6E 00 07 A1 20 01 03 66 6F 00 09 27 C0 01 03 66 70 00 0A AE 60 01 03 66 71 00 0C 35 00 00 00 00 32 01 03 66 40 00 00 01 F4 01 03 66 41 00 00 01 F4 01 03 66 42 00 00 01 F4 01 03 66 43 00 00 03 E8 01 03 66 44 00 00 03 E8 01 03 66 45 00 00 03 E8 01 03 66 46 00 00 07 D0 01 03 66 47 00 00 07 D0 01 03 66 48 00 00 07 D0 01 03 66 49 00 00 0B B8 01 03 66 4A 00 00 0B B8 01 03 66 4B 00 00 0B B8 01 03 66 4C 00 00 0F A0 01 03 66 4D 00 00 0F A0 01 03 66 4E 00 00 0F A0 01 03 66 4F 00 00 13 88 01 03 66 50 00 00 13 88 01 03 66 51 00 00 13 88 01 03 66 52 00 00 17 70 01 03 66 53 00 00 17 70 01 03 66 54 00 00 17 70 01 03 66 55 00 00 1B 58 01 03 66 56 00 00 1B 58 01 03 66 57 00 00 27 10 01 03 66 58 00 00 27 10 01 03 66 59 00 00 27 10 01 03 66 5A 00 00 3A 98 01 03 66 5B 00 00 3A 98 01 03 66 5C 00 00 3A 98 01 03 66 5D 00 00 4E 20 01 03 66 5E 00 00 4E 20 01 03 66 5F 00 00 4E 20 01 03 66 60 00 00 75 30 01 03 66 61 00 00 75 30 01 03 66 62 00 00 75 30 01 03 66 63 00 00 C3 50 01 03 66 64 00 01 86 A0 01 03 66 65 00 04 1E B0 01 03 66 66 00 01 86 A0 01 03 66 67 00 02 49 F0 01 03 66 68 00 02 49 F0 01 03 66 69 00 02 49 F0 01 03 66 6A 00 03 0D 40 01 03 66 6B 00 03 D0 90 01 03 66 6C 00 04 93 E0 01 03 66 6D 00 06 1A 80 01 03 66 6E 00 07 A1 20 01 03 66 6F 00 09 27 C0 01 03 66 70 00 0A AE 60 01 03 66 71 00 0C 35 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0E 02 34 93 40 00 00 00 03 02 34 93 42 00 00 00 00 02 34 93 45 00 00 00 01 02 34 93 46 00 00 00 01 02 34 93 47 59 2A 8F 37 02 34 93 4A 00 00 00 03 02 34 93 4C 00 00 00 01 02 34 93 4D 00 00 00 00 02 34 93 4E 00 00 00 00 02 34 93 4F 59 25 5E 2D 02 34 93 50 00 00 00 00 02 34 93 51 00 00 00 01 02 34 93 52 00 00 00 01 02 34 93 53 00 00 00 01 00 00 00 0B 00 3D 09 00 00 00 00 01 00 3D 09 01 00 00 00 02 00 3D 09 03 00 00 00 01 00 3D 09 04 00 00 00 01 00 3D 09 06 00 00 00 01 00 3D 09 07 00 00 00 02 00 3D 09 08 00 00 00 02 00 3D 09 09 00 00 00 01 00 3D 09 0A 00 00 00 01 00 3D 09 0B 00 00 00 01 00 3D 09 0D 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 08 00 3D 09 00 00 00 00 1D 00 3D 09 01 00 00 00 00 00 3D 09 03 00 00 00 01 00 3D 09 04 00 00 00 01 00 3D 09 06 00 00 00 01 00 3D 09 08 00 00 00 00 00 3D 09 0D 00 00 00 00 01 8C BA 89 00 00 00 00 00 00 00 0F 00 3D 09 03 00 0A 00 00 00 3D 09 05 00 0A 00 00 00 3D 09 06 00 0C 00 00 00 3D 09 07 00 03 00 00 00 3D 09 08 00 02 00 00 00 3D 09 09 00 02 00 00 00 3D 09 0A 00 18 00 00 00 3D 09 0B 00 0A 00 00 00 3D 09 0C 00 06 00 00 00 3D 09 0D 00 02 00 00 00 3D 09 0F 00 04 00 00 00 3D 09 11 00 02 00 00 00 3D 09 17 00 05 00 00 00 3D 09 18 00 0A 00 00 00 3D 09 1E 00 0C 00 00 00 00 00 0E 00 3D 09 1F 00 00 00 14 00 3D 09 20 00 00 00 04 00 3D 09 21 00 00 00 00 00 3D 09 22 00 00 00 00 00 3D 09 23 00 00 00 00 00 3D 09 24 00 00 00 00 00 3D 09 25 00 00 00 00 00 3D 09 26 00 00 00 00 00 3D 09 27 00 00 00 00 00 3D 09 28 00 00 00 00 00 3D 09 29 00 00 00 00 00 3D 09 2A 00 00 00 00 00 3D 09 2B 00 00 00 00 00 3D 09 2C 00 00 00 00 00 00 00 02 00 3D 09 1F 00 00 00 00 00 3D 09 20 00 00 00 00 00 00 00 3D 01 8B 78 70 00 00 00 00 00 00 00 00 00 00 00 00".Replace(" ", ""));
            this.Data.AddRange(this.Device.Player.Avatar.ToBytes);
            this.Data.AddLong(this.Enemy.Avatar.UserId); //Opponent id
            this.Data.AddInt(0);
            this.Data.AddInt(0);
            this.Data.AddInt(0);
            this.Data.AddCompressed(this.Json);
            this.Data.AddCompressed("{\"event\":[]}");
            this.Data.AddCompressed("{\"Village2\":{\"TownHallMaxLevel\":8}}");
        }

        internal override void Process()
        {
            this.Device.State = Logic.Enums.State.IN_PC_BATTLE;
        }

        internal string Json => JsonConvert.SerializeObject(new
        {
            exp_ver = 1,
            buildings = EnemyObject.SelectToken("buildings"),
            obstacles = EnemyObject.SelectToken("obstacles"),
            traps = EnemyObject.SelectToken("traps"),
            decos = EnemyObject.SelectToken("decos"),
            vobjs = EnemyObject.SelectToken("vobjs"),
            buildings2 = EnemyObject.SelectToken("buildings2"),
            obstacles2 = EnemyObject.SelectToken("obstacles2"),
            traps2 = EnemyObject.SelectToken("traps2"),
            decos2 = EnemyObject.SelectToken("decos2"),
            vobjs2 = EnemyObject.SelectToken("vobjs2"),
            avatar_id_high = this.Enemy.Avatar.UserHighId,
            avatar_id_low = this.Enemy.Avatar.UserLowId,
            name = this.Enemy.Avatar.Name,
            alliance_name = this.Enemy.Avatar.Alliance_Name,
            xp_level = this.Enemy.Avatar.Level,
            alliance_id_high = this.Enemy.Avatar.ClanHighID,
            alliance_id_low = this.Enemy.Avatar.ClanLowID,
            badge_id = this.Enemy.Avatar.Badge_ID,
            alliance_exp_level = this.Enemy.Avatar.Alliance_Level,
            alliance_unit_visit_capacity = 0,
            alliance_unit_spell_visit_capacity = 0,
            league_type = this.Enemy.Avatar.League,
            resources = this.Enemy.Avatar.Resources,
            alliance_units = this.Enemy.Avatar.Castle_Units,
            hero_states = this.Enemy.Avatar.Heroes_States,
            hero_health = this.Enemy.Avatar.Heroes_Health,
            hero_upgrade = this.Enemy.Avatar.Heroes_Upgrades,
            hero_modes = this.Enemy.Avatar.Heroes_Modes,
            variables = this.Enemy.Avatar.Variables,
            castle_lvl = this.Enemy.Avatar.Castle_Level,
            castle_total = this.Enemy.Avatar.Castle_Total,
            castle_used = this.Enemy.Avatar.Castle_Used,
            castle_total_sp = this.Enemy.Avatar.Castle_Total_SP,
            castle_used_sp = this.Enemy.Avatar.Castle_Used_SP,
            town_hall_lvl = this.Enemy.Avatar.TownHall_Level,
            th_v2_lvl = this.Enemy.Avatar.Builder_TownHall_Level,
            score = this.Enemy.Avatar.Trophies,
            duel_score = this.Enemy.Avatar.Builder_Trophies,

        }, this.Client_JsonSettings);
    }
}
