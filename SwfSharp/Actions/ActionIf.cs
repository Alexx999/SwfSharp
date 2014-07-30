using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionIf : ActionBase
    {
        public ActionIf()
            : base(ActionType.If)
        {}
    }
}