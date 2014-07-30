using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionCall : ActionBase
    {
        public ActionCall()
            : base(ActionType.Call)
        {}
    }
}