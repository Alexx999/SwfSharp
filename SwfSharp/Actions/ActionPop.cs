using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionPop : ActionBase
    {
        public ActionPop()
            : base(ActionType.Pop)
        {}
    }
}