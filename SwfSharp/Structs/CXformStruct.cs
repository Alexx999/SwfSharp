using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class CXformStruct
    {
        protected int? _redMultTerm;
        protected int? _greenMultTerm;
        protected int? _blueMultTerm;
        protected int? _redAddTerm;
        protected int? _greenAddTerm;
        protected int? _blueAddTerm;

        [XmlAttribute]
        public int RedMultTerm
        {
            get { return _redMultTerm.GetValueOrDefault(256); }
            set { _redMultTerm = value; }
        }

        [XmlIgnore]
        public bool RedMultTermSpecified
        {
            get { return _redMultTerm.HasValue; }
        }

        [XmlAttribute]
        public int GreenMultTerm
        {
            get { return _greenMultTerm.GetValueOrDefault(256); }
            set { _greenMultTerm = value; }
        }

        [XmlIgnore]
        public bool GreenMultTermSpecified
        {
            get { return _greenMultTerm.HasValue; }
        }

        [XmlAttribute]
        public int BlueMultTerm
        {
            get { return _blueMultTerm.GetValueOrDefault(256); }
            set { _blueMultTerm = value; }
        }

        [XmlIgnore]
        public bool BlueMultTermSpecified
        {
            get { return _blueMultTerm.HasValue; }
        }

        [XmlAttribute]
        public int RedAddTerm
        {
            get { return _redAddTerm.GetValueOrDefault(); }
            set { _redAddTerm = value; }
        }

        [XmlIgnore]
        public bool RedAddTermSpecified
        {
            get { return _redAddTerm.HasValue; }
        }

        [XmlAttribute]
        public int GreenAddTerm
        {
            get { return _greenAddTerm.GetValueOrDefault(); }
            set { _greenAddTerm = value; }
        }

        [XmlIgnore]
        public bool GreenAddTermSpecified
        {
            get { return _greenAddTerm.HasValue; }
        }

        [XmlAttribute]
        public int BlueAddTerm
        {
            get { return _blueAddTerm.GetValueOrDefault(); }
            set { _blueAddTerm = value; }
        }

        [XmlIgnore]
        public bool BlueAddTermSpecified
        {
            get { return _blueAddTerm.HasValue; }
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
            }
            if (hasAddTerms)
            {
                _redAddTerm = reader.ReadBitsSigned(nbits);
                _greenAddTerm = reader.ReadBitsSigned(nbits);
                _blueAddTerm = reader.ReadBitsSigned(nbits);
            }
        }

        internal static CXformStruct CreateFromStream(BitReader reader)
        {
            var result = new CXformStruct();

            result.FromStream(reader);

            return result;
        }

        internal virtual void ToStream(BitWriter writer)
        {
            writer.Align();

            var hasAddTerms = _redAddTerm.HasValue && _greenAddTerm.HasValue && _blueAddTerm.HasValue;
            var hasMultTerms = _redMultTerm.HasValue && _greenMultTerm.HasValue && _blueMultTerm.HasValue;

            writer.WriteBoolBit(hasAddTerms);
            writer.WriteBoolBit(hasMultTerms);

            uint nbits = 0;

            if (hasMultTerms)
            {
                nbits = BitWriter.MinBitsPerField(new[] { _redMultTerm.Value, _greenMultTerm.Value, _blueMultTerm.Value });
            }
            if (hasAddTerms)
            {
                nbits = Math.Max(BitWriter.MinBitsPerField(new[] { _redAddTerm.Value, _greenAddTerm.Value, _blueAddTerm.Value }), nbits);
            }

            writer.WriteBits(4, nbits);

            if (hasMultTerms)
            {
                writer.WriteBitsSigned(nbits, _redMultTerm.Value);
                writer.WriteBitsSigned(nbits, _greenMultTerm.Value);
                writer.WriteBitsSigned(nbits, _blueMultTerm.Value);
            }
            if (hasAddTerms)
            {
                writer.WriteBitsSigned(nbits, _redAddTerm.Value);
                writer.WriteBitsSigned(nbits, _greenAddTerm.Value);
                writer.WriteBitsSigned(nbits, _blueAddTerm.Value);
            }
        }
    }
}
