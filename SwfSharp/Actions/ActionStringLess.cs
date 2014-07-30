using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStringLess : ActionBase
    {
        public ActionStringLess()
            : base(ActionType.StringLess)
        {}
    }
}