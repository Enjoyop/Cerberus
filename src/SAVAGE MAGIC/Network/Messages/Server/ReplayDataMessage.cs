﻿using System.Collections.Generic;
using System.IO;
using Savage.Magic;
using Magic.Utilities.ZLib;

namespace Savage.Magic.Network.Messages.Server
{
    internal class ReplayData : Message
    {
        public ReplayData(ClashOfClans.Client client) : base(client)
        {
            MessageType = 24114;
        }

        public override void Encode()
        {
            var data = new List<byte>();
            var text = File.ReadAllText("replay-json.txt");
            //data.AddRange(ZlibStream.CompressString(text));
            data.AddCompressedString(text);
            //data.AddRange(File.ReadAllBytes("test.bin"));
            Encrypt(data.ToArray());
        }
    }
}