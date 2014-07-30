using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionPushDuplicate : ActionBase
    {
        public ActionPushDuplicate()
            : base(ActionType.PushDuplicate)
        {}
    }
}