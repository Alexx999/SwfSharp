using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class PlaceObject2Tag : SwfTag
    {
        public bool PlaceFlagHasClipActions { get; set; }
        public bool PlaceFlagHasClipDepth { get; set; }
        public bool PlaceFlagHasName { get; set; }
        public bool PlaceFlagHasRatio { get; set; }
        public bool PlaceFlagHasColorTransform { get; set; }
        public bool PlaceFlagHasMatrix { get; set; }
        public bool PlaceFlagHasCharacter { get; set; }
        public bool PlaceFlagMove { get; set; }
        public ushort Depth { get; set; }
        public ushort CharacterId { get; set; }
        public MatrixStruct Matrix { get; set; }
        public CXformWithAlphaStruct ColorTransform { get; set; }
        public ushort Ratio { get; set; }
        public string Name { get; set; }
        public ushort ClipDepth { get; set; }
        public ClipActionsStruct ClipActions { get; set; }

        public PlaceObject2Tag(int size)
            : base(TagType.PlaceObject2, size)
        {
        }

        protected PlaceObject2Tag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            PlaceFlagHasClipActions = reader.ReadBoolBit();
            PlaceFlagHasClipDepth = reader.ReadBoolBit();
            PlaceFlagHasName = reader.ReadBoolBit();
            PlaceFlagHasRatio = reader.ReadBoolBit();
            PlaceFlagHasColorTransform = reader.ReadBoolBit();
            PlaceFlagHasMatrix = reader.ReadBoolBit();
            PlaceFlagHasCharacter = reader.ReadBoolBit();
            PlaceFlagMove = reader.ReadBoolBit();
            Depth = reader.ReadUI16();
            if (PlaceFlagHasCharacter)
            {
                CharacterId = reader.ReadUI16();
            }
            if (PlaceFlagHasMatrix)
            {
                Matrix = MatrixStruct.CreateFromStream(reader);
            }
            if (PlaceFlagHasColorTransform)
            {
                ColorTransform = CXformWithAlphaStruct.CreateFromStream(reader);
            }
            if (PlaceFlagHasRatio)
            {
                Ratio = reader.ReadUI16();
            }
            if (PlaceFlagHasName)
            {
                Name = reader.ReadString();
            }
            if (PlaceFlagHasClipDepth)
            {
                ClipDepth = reader.ReadUI16();
            }
            if (PlaceFlagHasClipActions)
            {
                ClipActions = ClipActionsStruct.CreateFromStream(reader, swfVersion);
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteBoolBit(PlaceFlagHasClipActions);
            writer.WriteBoolBit(PlaceFlagHasClipDepth);
            writer.WriteBoolBit(PlaceFlagHasName);
            writer.WriteBoolBit(PlaceFlagHasRatio);
            writer.WriteBoolBit(PlaceFlagHasColorTransform);
            writer.WriteBoolBit(PlaceFlagHasMatrix);
            writer.WriteBoolBit(PlaceFlagHasCharacter);
            writer.WriteBoolBit(PlaceFlagMove);
            writer.WriteUI16(Depth);
            if (PlaceFlagHasCharacter)
            {
                writer.WriteUI16(CharacterId);
            }
            if (PlaceFlagHasMatrix)
            {
                Matrix.ToStream(writer);
            }
            if (PlaceFlagHasColorTransform)
            {
                ColorTransform.ToStream(writer);
            }
            if (PlaceFlagHasRatio)
            {
                writer.WriteUI16(Ratio);
            }
            if (PlaceFlagHasName)
            {
                writer.WriteString(Name, swfVersion);
            }
            if (PlaceFlagHasClipDepth)
            {
                writer.WriteUI16(ClipDepth);
            }
            if (PlaceFlagHasClipActions)
            {
                ClipActions.ToStream(writer, swfVersion);
            }
        }
    }
}
