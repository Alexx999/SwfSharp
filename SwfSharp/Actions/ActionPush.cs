using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionPush : ActionBase
    {
        [XmlElement("Push")]
        public List<PushRecord> PushRecords { get; set; }

        public ActionPush()
            : base(ActionType.Push)
        {
            PushRecords = new List<PushRecord>();
        }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            var size = reader.ReadUI16();
            var startPos = reader.Position;
            while (reader.Position - startPos < size)
            {
                PushRecords.Add(PushRecord.CreateFromStream(reader));
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
                foreach (var record in PushRecords)
                {
                    record.ToStream(writer, swfVersion);
                }
            }
            return result;
        }

        public enum PushType : byte
        {
            String = 0,
            Float = 1,
            Null = 2,
            Undefined = 3,
            Register = 4,
            Boolean = 5,
            Double = 6,
            Integer = 7,
            Constant8 = 8,
            Constant16 = 9,
            Constant = 9
        }

        public class PushRecord
        {
            [XmlIgnore]
            public PushType Type { get; set; }
            [XmlAttribute]
            public string String { get; set; }

            [XmlIgnore]
            public bool StringSpecified
            {
                get { return Type == PushType.String; }
            }

            [XmlAttribute]
            public float Float { get; set; }

            [XmlIgnore]
            public bool FloatSpecified
            {
                get { return Type == PushType.Float; }
            }

            [XmlAttribute]
            public byte RegisterNumber { get; set; }

            [XmlIgnore]
            public bool RegisterNumberSpecified
            {
                get { return Type == PushType.Register; }
            }

            [XmlAttribute]
            public bool Boolean { get; set; }

            [XmlIgnore]
            public bool BooleanSpecified
            {
                get { return Type == PushType.Boolean; }
            }

            [XmlAttribute]
            public double Double { get; set; }

            [XmlIgnore]
            public bool DoubleSpecified
            {
                get { return Type == PushType.Double; }
            }

            [XmlAttribute]
            public int Integer { get; set; }

            [XmlIgnore]
            public bool IntegerSpecified
            {
                get { return Type == PushType.Integer; }
            }

            [XmlAttribute]
            public ushort Constant { get; set; }

            [XmlIgnore]
            public bool ConstantSpecified
            {
                get { return Type == PushType.Constant8 || Type == PushType.Constant16; }
            }

            private void FromStream(BitReader reader)
            {
                Type = (PushType) reader.ReadUI8();
                switch (Type)
                {
                    case PushType.String:
                    {
                        String = reader.ReadString();
                        break;
                    }
                    case PushType.Float:
                    {
                        Float = reader.ReadFloat();
                        break;
                    }
                    case PushType.Null:
                    case PushType.Undefined:
                    {
                        break;
                    }
                    case PushType.Register:
                    {
                        RegisterNumber = reader.ReadUI8();
                        break;
                    }
                    case PushType.Boolean:
                    {
                        Boolean = reader.ReadUI8() != 0;
                        break;
                    }
                    case PushType.Double:
                    {
                        Double = reader.ReadDouble();
                        break;
                    }
                    case PushType.Integer:
                    {
                        Integer = reader.ReadSI32();
                        break;
                    }
                    case PushType.Constant8:
                    {
                        Constant = reader.ReadUI8();
                        break;
                    }
                    case PushType.Constant16:
                    {
                        Constant = reader.ReadUI16();
                        break;
                    }
                    default:
                    {
                        throw new InvalidDataException("Bad Push type");
                    }
                }
            }

            internal static PushRecord CreateFromStream(BitReader reader)
            {
                var result = new PushRecord();

                result.FromStream(reader);

                return result;
            }

            internal void ToStream(BitWriter writer, byte swfVersion)
            {
                /*if (Type == PushType.Constant8 || Type == PushType.Constant16)
                {
                    Type = Constant < byte.MaxValue ? PushType.Constant8 : PushType.Constant16;
                }*/
                writer.WriteUI8((byte) Type);
                switch (Type)
                {
                    case PushType.String:
                    {
                        writer.WriteString(String, swfVersion);
                        break;
                    }
                    case PushType.Float:
                    {
                        writer.WriteFloat(Float);
                        break;
                    }
                    case PushType.Null:
                    case PushType.Undefined:
                    {
                        break;
                    }
                    case PushType.Register:
                    {
                        writer.WriteUI8(RegisterNumber);
                        break;
                    }
                    case PushType.Boolean:
                    {
                        writer.WriteUI8(Boolean ? byte.MaxValue : (byte) 0);
                        break;
                    }
                    case PushType.Double:
                    {
                        writer.WriteDouble(Double);
                        break;
                    }
                    case PushType.Integer:
                    {
                        writer.WriteSI32(Integer);
                        break;
                    }
                    case PushType.Constant8:
                    {
                        writer.WriteUI8((byte) Constant);
                        break;
                    }
                    case PushType.Constant16:
                    {
                        writer.WriteUI16(Constant);
                        break;
                    }
                    default:
                    {
                        throw new InvalidDataException("Bad Push type");
                    }
                }
            }
        }
    }
}