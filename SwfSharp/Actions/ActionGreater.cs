using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionGreater : ActionBase
    {
        public ActionGreater()
            : base(ActionType.Greater)
        {}
    }
}