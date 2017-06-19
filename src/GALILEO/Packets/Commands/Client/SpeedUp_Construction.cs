﻿using Republic.Magic.Extensions.Binary;
using Republic.Magic.Logic;
using Republic.Magic.Logic.Enums;
using Republic.Magic.Logic.Structure;

namespace Republic.Magic.Packets.Commands.Client
{
    internal class SpeedUp_Construction : Command
    {
        internal int BuildingId;

        public SpeedUp_Construction(Reader reader, Device client, int id) : base(reader, client, id)
        {
        }
        internal override void Decode()
        {
            this.BuildingId = this.Reader.ReadInt32();
            this.Reader.ReadInt32();
        }

        internal override void Process()
        {
            var go =  this.Device.Player.Avatar.Variables.IsBuilderVillage ? this.Device.Player.GameObjectManager.GetBuilderVillageGameObjectByID(BuildingId) : this.Device.Player.GameObjectManager.GetGameObjectByID(BuildingId);

            if (go != null)
            {
                if (go.ClassId == 0 || go.ClassId == 4 || go.ClassId == 7 || go.ClassId == 11)
                {
                    ((ConstructionItem)go).SpeedUpConstruction();
                }
            }
        }
    }
}
