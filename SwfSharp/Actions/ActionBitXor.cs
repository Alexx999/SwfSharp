using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionBitXor : ActionBase
    {
        public ActionBitXor()
            : base(ActionType.BitXor)
        {}
    }
}