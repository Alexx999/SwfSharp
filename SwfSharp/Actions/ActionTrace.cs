using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionTrace : ActionBase
    {
        public ActionTrace()
            : base(ActionType.Trace)
        {}
    }
}