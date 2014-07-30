using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionBitOr : ActionBase
    {
        public ActionBitOr()
            : base(ActionType.BitOr)
        {}
    }
}