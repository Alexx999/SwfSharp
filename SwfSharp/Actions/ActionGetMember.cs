using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionGetMember : ActionBase
    {
        public ActionGetMember()
            : base(ActionType.GetMember)
        {}
    }
}