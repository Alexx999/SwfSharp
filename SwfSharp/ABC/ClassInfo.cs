using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class ClassInfo
    {
        private void FromStream(BitReader reader, CpoolInfo cpool)
        {
        }

        internal static ClassInfo CreateFromStream(BitReader reader, CpoolInfo cpool)
        {
            var result = new ClassInfo();
            result.FromStream(reader, cpool);
            return result;
        }

        internal void ToStream(BitWriter writer, CpoolInfo cpool)
        {
        }
    }
}
