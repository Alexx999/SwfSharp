using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SwfSharp.Utils
{
    internal class BitReader : IDisposable
    {
        private const int BufferSize = 64;
        private BinaryReader _reader;
        private bool _keepOpen;
        private uint _bitPos;
        private ulong _tempBuf;
        private long _tagEndPos;

        public BitReader(Stream stream) : this(stream, false)
        {
        }

        public BitReader(Stream stream, bool keepOpen)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            _reader = new BinaryReader(stream);
            _keepOpen = keepOpen;
        }

        public long TagEndPos
        {
            get { return _tagEndPos; }
        }

        public void Align()
        {
            _bitPos = 0;
            _tempBuf = 0;
        }

        public bool ReadBoolBit()
        {
            return ReadBits(1) != 0;
        }

        public byte ReadUI8()
        {
            Align();
            return _reader.ReadByte();
        }

        public sbyte ReadSI8()
        {
            Align();
            return _reader.ReadSByte();
        }

        public ushort ReadUI16()
        {
            Align();
            return _reader.ReadUInt16();
        }

        public short ReadSI16()
        {
            Align();
            return _reader.ReadInt16();
        }

        public uint ReadUI24()
        {
            Align();
            return ReadBits(24);
        }

        public int ReadSI24()
        {
            Align();
            return ReadBitsSigned(24);
        }

        public uint ReadUI32()
        {
            Align();
            return _reader.ReadUInt32();
        }

        public int ReadSI32()
        {
            Align();
            return _reader.ReadInt32();
        }

        public ulong ReadUI64()
        {
            Align();
            return _reader.ReadUInt64();
        }

        public long ReadSI64()
        {
            Align();
            return _reader.ReadInt64();
        }

        public float ReadFloat16()
        {
            Align();
            return Half.ToHalf(_reader.ReadUInt16());
        }

        public float ReadFloat()
        {
            Align();
            return _reader.ReadSingle();
        }

        public double ReadDouble()
        {
            Align();
            return _reader.ReadDouble();
        }

        public float ReadFixed8()
        {
            return ReadSI16() / 256.0f;
        }

        public double ReadFixed()
        {
            return ReadSI32() / 65536.0;
        }

        public uint ReadBits(uint length)
        {
            if (length == 0)
            {
                return 0;
            }
            if (length > 32)
            {
                throw new ArgumentOutOfRangeException("length", "Cannot read more than 32 bits at a time");
            }

            while (_bitPos < length)
            {
                _tempBuf <<= 8;
                _tempBuf |= _reader.ReadByte();
                _bitPos += 8;
            }

            var shift = (int)(_bitPos - length);
            var mask = ulong.MaxValue >> (BufferSize - shift);

            var result = (uint)(_tempBuf >> shift);
            _tempBuf &= mask;
            if (shift == 0)
            {
                _tempBuf = 0;
            }
            _bitPos -= length;

            return result;
        }

        public int ReadBitsSigned(uint length)
        {
            if (length == 0)
            {
                return 0;
            }
            if (length > 32)
            {
                throw new ArgumentOutOfRangeException("length", "Cannot read more than 32 bits at a time");
            }
            var signBit = ReadBits(1);
            var bits = ReadBits(length - 1);
            var mask = (int)(uint.MaxValue << (int)(length - 1)) * signBit;
            var value = (int)(mask | bits);
            return value;
        }

        public double ReadFBits(uint length)
        {
            return ReadBitsSigned(length) / 65536.0;
        }

        public byte[] ReadBytes(int size)
        {
            Align();
            return _reader.ReadBytes(size);
        }

        public uint ReadEncodedU32()
        {
            uint result = ReadUI8();
            if ((result & 0x00000080) == 0)
            {
                return result;
            }
            result &= 0x0000007f;
            result |= (uint)(ReadUI8() << 7);
            if ((result & 0x00004000) == 0)
            {
                return result;
            }
            result &= 0x00003fff;
            result |= (uint)(ReadUI8() << 14);
            if ((result & 0x00200000) == 0)
            {
                return result;
            }
            result &= 0x001fffff;
            result |= (uint)(ReadUI8() << 21);
            if ((result & 0x10000000) == 0)
            {
                return result;
            }
            result &= 0x0fffffff;
            result |= (uint)(ReadUI8() << 28);
            return result;
        }

        public string ReadString()
        {
            var ms = new MemoryStream();
            byte b;
            while ((b = ReadUI8()) != 0)
            {
                ms.WriteByte(b);
            }
            return Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Position);
        }

        public string ReadString(int size)
        {
            Align();
            return Encoding.UTF8.GetString(ReadBytes(size), 0, size);
        }

        public string ReadSizeString()
        {
            var size = ReadUI8();
            var str = ReadString(size);
            if (str.Last() == '\0')
            {
                return str.Substring(0, str.Length - 1);
            }
            return str;
        }

        public void Close()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            var reader = _reader;
            _reader = null;
            if (disposing && !_keepOpen)
            {
                reader.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public void BeginReadTag(int size)
        {
            _tagEndPos = _reader.BaseStream.Position + size;
        }

        public void EndReadTag()
        {
            _reader.BaseStream.Position = TagEndPos;
        }

        public bool AtTagEnd()
        {
            return _tagEndPos == _reader.BaseStream.Position;
        }

        public long TagBytesRemaining
        {
            get
            {
                return _tagEndPos - _reader.BaseStream.Position;
            }
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            _reader.BaseStream.Seek(offset, origin);
            if (_bitPos != 0)
            {
                throw new NotSupportedException("BitPos must be 0 when seeking");
            }
        }

        public long Position
        {
            get { return _reader.BaseStream.Position; }
        }

        public int ReadExtendableCount()
        {
            int len = ReadUI8();
            if (len == byte.MaxValue)
            {
                len = ReadUI16();
            }
            return len;
        }
    }
}
