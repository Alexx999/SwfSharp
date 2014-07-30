using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionTry : ActionBase
    {
        [XmlAttribute]
        public string CatchName { get; set; }
        [XmlAttribute]
        public byte CatchRegister { get; set; }

        [XmlIgnore]
        public bool CatchRegisterSpecified
        {
            get { return string.IsNullOrEmpty(CatchName); }
        }

        public List<ActionBase> TryBody { get; set; }
        public List<ActionBase> CatchBody { get; set; }
        public List<ActionBase> FinallyBody { get; set; } 

        public ActionTry()
            : base(ActionType.Try)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            reader.ReadBits(5);
            var catchInRegisterFlag = reader.ReadBoolBit();
            var finallyBlockFlag = reader.ReadBoolBit();
            var catchBlockFlag = reader.ReadBoolBit();

            var trySize = reader.ReadUI16();
            var catchSize = reader.ReadUI16();
            var finallySize = reader.ReadUI16();

            if (catchInRegisterFlag)
            {
                CatchRegister = reader.ReadUI8();
            }
            else
            {
                CatchName = reader.ReadString();
            }

            TryBody = new List<ActionBase>();
            var tryStartPos = reader.Position;
            while (reader.Position - tryStartPos < trySize)
            {
                TryBody.Add(ActionFactory.ReadAction(reader));
            }

            CatchBody = new List<ActionBase>();
            var catchStartPos = reader.Position;
            while (reader.Position - catchStartPos < catchSize)
            {
                CatchBody.Add(ActionFactory.ReadAction(reader));
            }

            FinallyBody = new List<ActionBase>();
            var finallyStartPos = reader.Position;
            while (reader.Position - finallyStartPos < finallySize)
            {
                FinallyBody.Add(ActionFactory.ReadAction(reader));
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
                var catchInRegisterFlag = !string.IsNullOrEmpty(CatchName);
                var finallyBlockFlag = FinallyBody != null && FinallyBody.Count > 0;
                var catchBlockFlag = CatchBody != null && CatchBody.Count > 0;

                var tryMs = GetCodeStream(TryBody, swfVersion);
                MemoryStream catchMs = null;
                if (catchBlockFlag)
                {
                    catchMs = GetCodeStream(CatchBody, swfVersion);
                }
                MemoryStream finallyMs = null;
                if (finallyBlockFlag)
                {
                    finallyMs = GetCodeStream(FinallyBody, swfVersion);
                }

                writer.WriteUI16((ushort) tryMs.Position);
                writer.WriteUI16((ushort) (catchMs != null ?catchMs.Position:0));
                writer.WriteUI16((ushort)(finallyMs != null ? finallyMs.Position : 0));

                if (catchInRegisterFlag)
                {
                    writer.WriteUI8(CatchRegister);
                }
                else
                {
                    writer.WriteString(CatchName, swfVersion);
                }


                writer.WriteBytes(tryMs.GetBuffer(), 0, (int)tryMs.Position);
                if (catchMs != null)
                {
                    writer.WriteBytes(catchMs.GetBuffer(), 0, (int)catchMs.Position);
                }
                if (finallyMs != null)
                {
                    writer.WriteBytes(finallyMs.GetBuffer(), 0, (int)finallyMs.Position);
                }
            }
            return result;
        }

        private MemoryStream GetCodeStream(List<ActionBase> actions, byte swfVersion)
        {
            var result = new MemoryStream();
            using (var writer = new BitWriter(result, true))
            {
                foreach (var action in actions)
                {
                    action.ToStream(writer, swfVersion);
                }
            }
            return result;
        }
    }
}