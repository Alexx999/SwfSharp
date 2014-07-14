using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SwfSharp.Utils
{
    class BitWriter : IDisposable
    {
        private BinaryWriter _writer;
        private bool _keepOpen;
        private uint _bitPos = 32;
        private uint _tempBuf;

        public BitWriter(Stream stream) : this(stream, false)
        {
        }

        public BitWriter(Stream stream, bool keepOpen)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            _writer = new BinaryWriter(stream);
            _keepOpen = keepOpen;
        }

        public void Align()
        {
            Flush();
            _bitPos = 32;
            _tempBuf = 0;
        }

        private void Flush()
        {
            while (_bitPos != 32)
            {
                WriteByteFromBuffer();
            }
        }

        public void WriteUI8(byte data)
        {
            Align();
            _writer.Write(data);
        }

        private void WriteUI8Internal(byte data)
        {
            _writer.Write(data);
        }

        public void WriteUI16(ushort data)
        {
            Align();
            _writer.Write(data);
        }

        public void WriteUI32(uint data)
        {
            Align();
            _writer.Write(data);
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
            Flush();
            var writer = _writer;
            _writer = null;
            if (disposing && !_keepOpen)
            {
                writer.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public static uint MinBitsPerField(IEnumerable<int> values)
        {
            return values.Max(v => GetBitsForField(v));
        }

        public static uint MinBitsPerField(IEnumerable<uint> values)
        {
            return values.Max(v => GetBitsForField(v));
        }

        private static uint GetBitsForField(int value)
        {
            if (value == 0) return 0;

            return value > 0 ? GetBitsForFieldPos((uint) value) + 1 : GetBitsForFieldNeg(value) + 1;
        }

        private static uint GetBitsForField(uint value)
        {
            if (value == 0) return 0;
            return GetBitsForFieldPos(value);
        }

        private static uint GetBitsForFieldNeg(int value)
        {
            const uint mask = 0x80000000;
            var bits = 32U;
            while ((value & mask) != 0 && bits > 0)
            {
                value <<= 1;
                bits--;
            }
            return bits;
        }

        private static uint GetBitsForFieldPos(uint value)
        {
            const uint mask = 0x80000000;
            var bits = 32U;
            while ((value & mask) == 0 && bits > 0)
            {
                value <<= 1;
                bits--;
            }
            return bits;
        }

        public void WriteBits(uint numBits, uint value)
        {
            var valueMask = uint.MaxValue >> (int)(32 - numBits);
            value &= valueMask;
            var shift = _bitPos - numBits;
            value <<= (int)shift;
            _tempBuf |= value;
            _bitPos -= numBits;
            while (_bitPos <= 24)
            {
                WriteByteFromBuffer();
            }
        }

        public void WriteBitsSigned(uint numBits, int data)
        {
            WriteBits(numBits, (uint)data);
        }

        public void WriteFixed8(float frameRate)
        {
            var value = frameRate*256.0;
            WriteUI16((ushort) value);
        }

        private void WriteByteFromBuffer()
        {
            var toWrite = _tempBuf >> 24;
            WriteUI8Internal((byte)toWrite);
            _tempBuf <<= 8;
            if (_bitPos <= 24)
            {
                _bitPos += 8;
            }
            else
            {
                _bitPos = 32;
            }
        }

        public long Position
        {
            get { return _writer.BaseStream.Position; }
        }

        public void WriteBytes(byte[] data)
        {
            _writer.Write(data);
        }

        public void WriteBytes(byte[] data, int index, int count)
        {
            _writer.Write(data, index, count);
        }

        public void WriteBoolBit(bool data)
        {
            if (data)
            {
                WriteBits(1, 1);
            }
            else
            {
                WriteBits(1, 0);
            }
        }

        public void WriteUI24(uint data)
        {
            Align();
            WriteBits(24, data);
        }

        public void WriteStringBytes(string data, byte swfVersion)
        {
            WriteBytes(swfVersion > 5 
                ? Encoding.UTF8.GetBytes(data) 
                : Encoding.GetEncoding("ISO-8859-1").GetBytes(data));
        }

        public void WriteString(string name, byte swfVersion)
        {
            WriteStringBytes(name, swfVersion);
            WriteUI8(0);
        }

        public void WriteSI32(int data)
        {
            Align();
            _writer.Write(data);
        }

        public void WriteSI8(sbyte data)
        {
            Align();
            _writer.Write(data);
        }

        public void WriteSI64(long data)
        {
            Align();
            _writer.Write(data);
        }
    }
}
