using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionIncrement : ActionBase
    {
        public ActionIncrement()
            : base(ActionType.Increment)
        {}
    }
}