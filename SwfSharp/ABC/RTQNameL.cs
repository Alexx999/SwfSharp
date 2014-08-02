using System;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class RTQNameL : MultinameInfo
    {
        private RTQNameL()
            : base(MultinameKind.RTQNameL)
        {}

        public RTQNameL(MultinameKind kind)
            : base(kind)
        {}

        internal static RTQNameL CreateFromStream(BitReader reader, MultinameKind kind)
        {
            var result = new RTQNameL(kind);
            return result;
        }
    }
}