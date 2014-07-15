﻿using System;
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
        private Encoding _swf5Encoding;

        public BitWriter(Stream stream)
            : this(stream, Encoding.GetEncoding("ISO-8859-1"), false)
        {
        }

        public BitWriter(Stream stream, bool keepOpen)
            : this(stream, Encoding.GetEncoding("ISO-8859-1"), keepOpen)
        {
        }

        public BitWriter(Stream stream, Encoding swf5Encoding)
            : this(stream, swf5Encoding, false)
        {
        }

        public BitWriter(Stream stream, Encoding swf5Encoding, bool keepOpen)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (swf5Encoding == null)
            {
                throw new ArgumentNullException("swf5Encoding");
            }

            _writer = new BinaryWriter(stream);
            _keepOpen = keepOpen;
            _swf5Encoding = swf5Encoding;
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
            return values.Max(v => GetBitsForValue(v));
        }

        public static uint MinBitsPerField(IEnumerable<uint> values)
        {
            return values.Max(v => GetBitsForValue(v));
        }

        public static uint GetBitsForValue(int value)
        {
            if (value == 0) return 0;

            return value > 0 ? GetBitsForFieldPos((uint) value) + 1 : GetBitsForFieldNeg(value) + 1;
        }

        public static uint GetBitsForValue(uint value)
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
            if(numBits == 0) return;
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

        public void WriteFixed8(float data)
        {
            var value = data*256.0;
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
            Align();
            _writer.Write(data);
        }

        public void WriteBytes(byte[] data, int index, int count)
        {
            Align();
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

        private void WriteStringBytes(string data, byte swfVersion)
        {
            WriteBytes(GetStringBytes(data, swfVersion));
        }

        private byte[] GetStringBytes(string data, byte swfVersion)
        {
            return swfVersion > 5
                ? Encoding.UTF8.GetBytes(data)
                : _swf5Encoding.GetBytes(data);
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

        public void WriteExtendableCount(int count)
        {
            if (count >= byte.MaxValue)
            {
                WriteUI8(byte.MaxValue);
                WriteUI16((ushort) count);
            }
            else
            {
                WriteUI8((byte) count);
            }
        }

        private static uint GetFixed(float data)
        {
            var value = data * 65536.0;
            return (uint) value;
        }

        public void WriteBitSizeAndData(uint sizeBits, float[] data)
        {
            var values = new uint[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                values[i] = GetFixed(data[i]);
            }
            var bitsPerValue = MinBitsPerField(values);
            WriteBits(sizeBits, bitsPerValue);
            for (int i = 0; i < values.Length; i++)
            {
                WriteBits(bitsPerValue, values[i]);
            }
        }

        public void WriteBitSizeAndData(uint sizeBits, uint[] data)
        {
            var bitsPerValue = MinBitsPerField(data);
            WriteBits(sizeBits, bitsPerValue);
            for (int i = 0; i < data.Length; i++)
            {
                WriteBits(bitsPerValue, data[i]);
            }
        }

        public void WriteBitSizeAndData(uint sizeBits, int[] data)
        {
            WriteBitSizeAndDataWithOffset(sizeBits, 0, data);
        }

        public void WriteBitSizeAndDataWithOffset(uint sizeBits, int offset, int[] data)
        {
            var bitsPerValue = MinBitsPerField(data);
            WriteBits(sizeBits, (uint) (bitsPerValue - offset));
            for (int i = 0; i < data.Length; i++)
            {
                WriteBitsSigned(bitsPerValue, data[i]);
            }
        }

        public void WriteSizeString(string data, byte swfVersion)
        {
            var bytes = GetStringBytes(data, swfVersion);
            WriteUI8((byte) (bytes.Length + 1));
            WriteBytes(bytes);
            WriteUI8(0);
        }

        public void WriteSI16(short data)
        {
            Align();
            _writer.Write(data);
        }

        public void WriteEncodedU32(uint data)
        {
            Align();
            do
            {
                byte fragment = (byte)(data & 127);
                if ((data >>= 7) > 0)
                {
                    fragment = (byte)(fragment | 128);
                }
                WriteUI8(fragment);
            } while (data > 0);
        }

        public void WriteFloat16(float data)
        {
            Align();
            var half = new Half(data);
            WriteUI16(Half.GetBits(half));
        }

        public void WriteFloat(float data)
        {
            Align();
            _writer.Write(data);
        }

        public void WriteFixed(float data)
        {
            var value = (uint)(data * 65536.0);
            WriteUI32(value);
        }
    }
}