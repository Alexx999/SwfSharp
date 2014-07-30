using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionThrow : ActionBase
    {
        public ActionThrow()
            : base(ActionType.Throw)
        {}
    }
}