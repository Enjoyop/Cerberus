﻿using System;
using Republic.Magic.Core.Networking;
using Republic.Magic.Extensions.Binary;
using Republic.Magic.Logic;
using Republic.Magic.Packets.Commands.Server;
using Republic.Magic.Packets.Messages.Server;
using Republic.Magic.Logic.Enums;

namespace Republic.Magic.Packets.Messages.Client
{
    internal class Change_Name : Message
    {
        internal string Name;
        public Change_Name(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Change_Name.
        }
        internal override void Decode()
        {
            this.Name = this.Reader.ReadString();
            this.Reader.ReadByte();
        }
        internal override void Process()
        {
            if (!String.IsNullOrEmpty(this.Name) && this.Name.Length < 16)
                //if (!GameTools.Badwords.Contains(this.Name))
            {
                this.Device.Player.Avatar.Name = this.Name;
                this.Device.Player.Avatar.NameState += 1;

                new Server_Commands(this.Device, new Name_Change_Callback(this.Device)).Send();
            }
           // else
             //   new Change_Name_Failed(this.Device).Send();
        }
    }
}
