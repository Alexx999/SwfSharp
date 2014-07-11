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
        private BinaryReader _reader;
        private bool _keepOpen;
        private uint _bitPos;
        private uint _tempBuf;
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

        public byte ReadAlignedUI8()
        {
            Align();
            return ReadUI8();
        }

        public sbyte ReadAlignedSI8()
        {
            Align();
            return ReadSI8();
        }

        public byte ReadUI8()
        {
            if (_bitPos == 0)
            {
                return _reader.ReadByte();
            }
            return (byte) ReadBits(16);
        }

        public sbyte ReadSI8()
        {
            if (_bitPos == 0)
            {
                return _reader.ReadSByte();
            }
            return (sbyte) ReadBits(16);
        }

        public ushort ReadAlignedUI16()
        {
            Align();
            return ReadUI16();
        }

        public short ReadAlignedSI16()
        {
            Align();
            return ReadSI16();
        }

        public ushort ReadUI16()
        {
            if (_bitPos == 0)
            {
                return _reader.ReadUInt16();
            }
            return (ushort) ReadBits(16);
        }

        public short ReadSI16()
        {
            if (_bitPos == 0)
            {
                return _reader.ReadInt16();
            }
            return (short) ReadBits(16);
        }

        public uint ReadAlignedUI24()
        {
            Align();
            return ReadUI24();
        }

        public int ReadAlignedSI24()
        {
            Align();
            return ReadSI24();
        }

        public uint ReadUI24()
        {
            return ReadBits(24);
        }

        public int ReadSI24()
        {
            return ReadBitsSigned(24);
        }

        public uint ReadAlignedUI32()
        {
            Align();
            return ReadUI32();
        }

        public int ReadAlignedSI32()
        {
            Align();
            return ReadSI32();
        }

        public uint ReadUI32()
        {
            if (_bitPos == 0)
            {
                return _reader.ReadUInt32();
            }
            return ReadBits(32);
        }

        public int ReadSI32()
        {
            if (_bitPos == 0)
            {
                return _reader.ReadInt32();
            }
            return ReadBitsSigned(32);
        }

        public ulong ReadAlignedUI64()
        {
            Align();
            return ReadUI64();
        }

        public long ReadAlignedSI64()
        {
            Align();
            return ReadSI64();
        }

        public ulong ReadUI64()
        {
            if (_bitPos == 0)
            {
                return _reader.ReadUInt64();
            }
            return ReadBits(64);
        }

        public long ReadSI64()
        {
            if (_bitPos == 0)
            {
                return _reader.ReadInt64();
            }
            return ReadBitsSigned(64);
        }

        public float ReadFixed8()
        {
            return ReadSI16() / 256.0f;
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
            var mask = 0xFFFFFFFF >> (32 - shift);

            var result = _tempBuf >> shift;
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

        public float ReadFBits(uint length)
        {
            return ReadBitsSigned(length) / 65536.0f;
        }

        public byte[] ReadBytes(int size)
        {
            if (_bitPos == 0)
            {
                return _reader.ReadBytes(size);
            }

            var result = new byte[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = ReadUI8();
            }
            return result;
        }

        public int ReadString(out string result)
        {
            var ms = new MemoryStream();
            byte b;
            while ((b = ReadUI8()) != 0)
            {
                ms.WriteByte(b);
            }
            result = Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Position);
            return (int) (ms.Position + 1);
        }

        public string ReadString()
        {
            Align();
            string result;
            ReadString(out result);
            return result;
        }

        public string ReadString(int size)
        {
            Align();
            return Encoding.UTF8.GetString(ReadBytes(size));
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
            Debug.Assert(AtTagEnd());
            _reader.BaseStream.Position = TagEndPos;
        }

        public bool AtTagEnd()
        {
            return TagEndPos == _reader.BaseStream.Position;
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            _reader.BaseStream.Seek(offset, origin);
            if (_bitPos != 0)
            {
                throw new NotSupportedException("BitPos must be 0 when seeking");
            }
        }
    }
}
