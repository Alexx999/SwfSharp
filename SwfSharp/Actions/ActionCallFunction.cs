using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionCallFunction : ActionBase
    {
        public ActionCallFunction()
            : base(ActionType.CallFunction)
        {}
    }
}