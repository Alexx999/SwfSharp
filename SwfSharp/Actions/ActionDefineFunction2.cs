using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionDefineFunction2 : ActionBase
    {
        [XmlAttribute]
        public string FunctionName { get; set; }
        [XmlArrayItem("Param")]
        public List<string> Params { get; set; }
        public List<ActionBase> Actions { get; set; } 

        public ActionDefineFunction2()
            : base(ActionType.DefineFunction2)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            FunctionName = reader.ReadString();
            var numParams = reader.ReadUI16();
            Params = new List<string>(numParams);
            for (int i = 0; i < numParams; i++)
            {
                Params.Add(reader.ReadString());
            }
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
            var ms = GetDataStream(swfVersion);
            writer.WriteUI16((ushort)ms.Position);
            writer.WriteBytes(ms.GetBuffer(), 0, (int)ms.Position);
        }

        private MemoryStream GetDataStream(byte swfVersion)
        {
            var result = new MemoryStream();
            using (var writer = new BitWriter(result, true))
            {
                writer.WriteString(FunctionName, swfVersion);
                writer.WriteUI16((ushort) Params.Count);
                foreach (var param in Params)
                {
                    writer.WriteString(param, swfVersion);
                }
                var ms = GetCodeStream(swfVersion);
                writer.WriteUI16((ushort)ms.Position);
                writer.WriteBytes(ms.GetBuffer(), 0, (int)ms.Position);
            }
            return result;
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