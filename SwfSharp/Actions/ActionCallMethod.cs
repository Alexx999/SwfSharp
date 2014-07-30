using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionCallMethod : ActionBase
    {
        public ActionCallMethod()
            : base(ActionType.CallMethod)
        {}
    }
}