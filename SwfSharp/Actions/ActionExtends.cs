using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionExtends : ActionBase
    {
        public ActionExtends()
            : base(ActionType.Extends)
        {}
    }
}