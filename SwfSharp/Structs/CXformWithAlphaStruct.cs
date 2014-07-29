using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class CXformWithAlphaStruct : CXformStruct
    {
        private int? _alphaMultTerm;
        private int? _alphaAddTerm;

        [XmlAttribute]
        public int AlphaMultTerm
        {
            get { return _alphaMultTerm.GetValueOrDefault(256); }
            set { _alphaMultTerm = value; }
        }

        [XmlIgnore]
        public bool AlphaMultTermSpecified
        {
            get { return _alphaMultTerm.HasValue; }
        }

        [XmlAttribute]
        public int AlphaAddTerm
        {
            get { return _alphaAddTerm.GetValueOrDefault(); }
            set { _alphaAddTerm = value; }
        }

        [XmlIgnore]
        public bool AlphaAddTermSpecified
        {
            get { return _alphaAddTerm.HasValue; }
        }

        private void FromStream(BitReader reader)
        {
            reader.Align();

            var hasAddTerms = reader.ReadBoolBit();
            var hasMultTerms = reader.ReadBoolBit();

            var nbits = reader.ReadBits(4);

            if (hasMultTerms)
            {
                _redMultTerm = reader.ReadBitsSigned(nbits);
                _greenMultTerm = reader.ReadBitsSigned(nbits);
                _blueMultTerm = reader.ReadBitsSigned(nbits);
                _alphaMultTerm = reader.ReadBitsSigned(nbits);
            }
            if (hasAddTerms)
            {
                _redAddTerm = reader.ReadBitsSigned(nbits);
                _greenAddTerm = reader.ReadBitsSigned(nbits);
                _blueAddTerm = reader.ReadBitsSigned(nbits);
                _alphaAddTerm = reader.ReadBitsSigned(nbits);
            }
        }

        internal new static CXformWithAlphaStruct CreateFromStream(BitReader reader)
        {
            var result = new CXformWithAlphaStruct();

            result.FromStream(reader);

            return result;
        }

        internal override void ToStream(BitWriter writer)
        {
            writer.Align();

            var hasAddTerms = _redAddTerm.HasValue && _greenAddTerm.HasValue && _blueAddTerm.HasValue && _alphaAddTerm.HasValue;
            var hasMultTerms = _redMultTerm.HasValue && _greenMultTerm.HasValue && _blueMultTerm.HasValue && _alphaMultTerm.HasValue;

            writer.WriteBoolBit(hasAddTerms);
            writer.WriteBoolBit(hasMultTerms);

            uint nbits = 0;

            if (hasMultTerms)
            {
                nbits = BitWriter.MinBitsPerField(new[] { _redMultTerm.Value, _greenMultTerm.Value, _blueMultTerm.Value, _alphaMultTerm.Value });
            }
            if (hasAddTerms)
            {
                nbits = Math.Max(BitWriter.MinBitsPerField(new[] { _redAddTerm.Value, _greenAddTerm.Value, _blueAddTerm.Value, _alphaAddTerm.Value }), nbits);
            }

            writer.WriteBits(4, nbits);

            if (hasMultTerms)
            {
                writer.WriteBitsSigned(nbits, _redMultTerm.Value);
                writer.WriteBitsSigned(nbits, _greenMultTerm.Value);
                writer.WriteBitsSigned(nbits, _blueMultTerm.Value);
                writer.WriteBitsSigned(nbits, _alphaMultTerm.Value);
            }
            if (hasAddTerms)
            {
                writer.WriteBitsSigned(nbits, _redAddTerm.Value);
                writer.WriteBitsSigned(nbits, _greenAddTerm.Value);
                writer.WriteBitsSigned(nbits, _blueAddTerm.Value);
                writer.WriteBitsSigned(nbits, _alphaAddTerm.Value);
            }
        }
    }
}