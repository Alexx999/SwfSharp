using System;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class PlaceObject2Tag : SwfTag
    {
        protected ushort? _characterId;
        protected ushort? _ratio;
        protected ushort? _clipDepth;

        [XmlAttribute]
        public bool PlaceFlagMove { get; set; }
        [XmlAttribute]
        public ushort Depth { get; set; }

        [XmlAttribute]
        public ushort CharacterId
        {
            get { return _characterId.GetValueOrDefault(); }
            set { _characterId = value; }
        }

        [XmlIgnore]
        public bool CharacterIdSpecified
        {
            get { return _characterId.HasValue; }
        }

        [XmlElement]
        public MatrixStruct Matrix { get; set; }
        [XmlElement]
        public CXformWithAlphaStruct ColorTransform { get; set; }

        public ushort Ratio
        {
            get { return _ratio.GetValueOrDefault(); }
            set { _ratio = value; }
        }

        [XmlIgnore]
        public bool RatioSpecified
        {
            get { return _ratio.HasValue; }
        }

        public string Name { get; set; }

        public ushort ClipDepth
        {
            get { return _clipDepth.GetValueOrDefault(); }
            set { _clipDepth = value; }
        }

        [XmlIgnore]
        public bool ClipDepthSpecified
        {
            get { return _clipDepth.HasValue; }
        }

        public ClipActionsStruct ClipActions { get; set; }

        public PlaceObject2Tag() : this(0)
        {
        }

        public PlaceObject2Tag(int size)
            : this(TagType.PlaceObject2, size)
        {
        }

        protected PlaceObject2Tag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            var placeFlagHasClipActions = reader.ReadBoolBit();
            var placeFlagHasClipDepth = reader.ReadBoolBit();
            var placeFlagHasName = reader.ReadBoolBit();
            var placeFlagHasRatio = reader.ReadBoolBit();
            var placeFlagHasColorTransform = reader.ReadBoolBit();
            var placeFlagHasMatrix = reader.ReadBoolBit();
            var placeFlagHasCharacter = reader.ReadBoolBit();
            PlaceFlagMove = reader.ReadBoolBit();
            Depth = reader.ReadUI16();
            if (placeFlagHasCharacter)
            {
                CharacterId = reader.ReadUI16();
            }
            if (placeFlagHasMatrix)
            {
                Matrix = MatrixStruct.CreateFromStream(reader);
            }
            if (placeFlagHasColorTransform)
            {
                ColorTransform = CXformWithAlphaStruct.CreateFromStream(reader);
            }
            if (placeFlagHasRatio)
            {
                Ratio = reader.ReadUI16();
            }
            if (placeFlagHasName)
            {
                Name = reader.ReadString();
            }
            if (placeFlagHasClipDepth)
            {
                ClipDepth = reader.ReadUI16();
            }
            if (placeFlagHasClipActions)
            {
                ClipActions = ClipActionsStruct.CreateFromStream(reader, swfVersion);
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            var placeFlagHasClipActions = ClipActions != null;
            var placeFlagHasClipDepth = _clipDepth.HasValue;
            var placeFlagHasName = !string.IsNullOrEmpty(Name);
            var placeFlagHasRatio = _ratio.HasValue;
            var placeFlagHasColorTransform = ColorTransform != null;
            var placeFlagHasMatrix = Matrix != null;
            var placeFlagHasCharacter = _characterId.HasValue;

            writer.WriteBoolBit(placeFlagHasClipActions);
            writer.WriteBoolBit(placeFlagHasClipDepth);
            writer.WriteBoolBit(placeFlagHasName);
            writer.WriteBoolBit(placeFlagHasRatio);
            writer.WriteBoolBit(placeFlagHasColorTransform);
            writer.WriteBoolBit(placeFlagHasMatrix);
            writer.WriteBoolBit(placeFlagHasCharacter);
            writer.WriteBoolBit(PlaceFlagMove);
            writer.WriteUI16(Depth);
            if (placeFlagHasCharacter)
            {
                writer.WriteUI16(_characterId.Value);
            }
            if (placeFlagHasMatrix)
            {
                Matrix.ToStream(writer);
            }
            if (placeFlagHasColorTransform)
            {
                ColorTransform.ToStream(writer);
            }
            if (placeFlagHasRatio)
            {
                writer.WriteUI16(_ratio.Value);
            }
            if (placeFlagHasName)
            {
                writer.WriteString(Name, swfVersion);
            }
            if (placeFlagHasClipDepth)
            {
                writer.WriteUI16(_clipDepth.Value);
            }
            if (placeFlagHasClipActions)
            {
                ClipActions.ToStream(writer, swfVersion);
            }
        }
    }
}
