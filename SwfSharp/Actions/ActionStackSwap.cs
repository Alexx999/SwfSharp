using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStackSwap : ActionBase
    {
        public ActionStackSwap()
            : base(ActionType.StackSwap)
        {}
    }
}