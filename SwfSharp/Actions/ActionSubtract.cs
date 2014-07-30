using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionSubtract : ActionBase
    {
        public ActionSubtract()
            : base(ActionType.Subtract)
        {}
    }
}