﻿using System;
using System.Collections.Generic;
using Republic.Magic.Packets.Debugs;

namespace Republic.Magic.Packets
{
    internal class DebugFactory
    {
        internal const string Delimiter = "/";

        public static Dictionary<string, Type> Debugs;

        public DebugFactory()
        {
            Debugs = new Dictionary<string, Type>
            {
                {"resource", typeof(Resource_Update)},
                {"stats", typeof(Statistics)},
                {"max_village", typeof(Max_Village)},
                {"rank", typeof(Set_Rank) }

            };
        }
    }
}