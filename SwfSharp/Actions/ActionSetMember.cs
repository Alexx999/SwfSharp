using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionSetMember : ActionBase
    {
        public ActionSetMember()
            : base(ActionType.SetMember)
        {}
    }
}