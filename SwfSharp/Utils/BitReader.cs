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

            var sign = (ReadBits(1) == 1) ? -1 : 1;
            return (int)ReadBits(length - 1)*sign;
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
    }
}
