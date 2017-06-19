﻿using Republic.Magic.Logic;

namespace Republic.Magic.Packets.Messages.Server.Battle
{
    internal class Battle_Failed : Message
    {
        public Battle_Failed(Device _Device) : base(_Device)
        {
            this.Identifier = 24103;
        }
    }
}
