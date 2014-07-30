using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionJump : ActionBase
    {
        public ActionJump()
            : base(ActionType.Jump)
        {}
    }
}