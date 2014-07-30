using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionBitAnd : ActionBase
    {
        public ActionBitAnd()
            : base(ActionType.BitAnd)
        {}
    }
}