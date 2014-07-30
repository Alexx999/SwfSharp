using System;
using System.Collections.Generic;
using System.IO;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionWith : ActionBase
    {
        public List<ActionBase> Actions { get; set; } 

        public ActionWith()
            : base(ActionType.With)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            Actions = new List<ActionBase>();
            var codeSize = reader.ReadUI16();
            var codeStartPos = reader.Position;
            while (reader.Position - codeStartPos < codeSize)
            {
                Actions.Add(ActionFactory.ReadAction(reader));
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            base.ToStream(writer, swfVersion);
            var ms = GetCodeStream(swfVersion);
            writer.WriteUI16((ushort)(ms.Position + 2));
            writer.WriteUI16((ushort)ms.Position);
            writer.WriteBytes(ms.GetBuffer(), 0, (int)ms.Position);
        }

        private MemoryStream GetCodeStream(byte swfVersion)
        {
            var result = new MemoryStream();
            using (var writer = new BitWriter(result, true))
            {
                foreach (var action in Actions)
                {
                    action.ToStream(writer, swfVersion);
                }
            }
            return result;
        }
    }
}