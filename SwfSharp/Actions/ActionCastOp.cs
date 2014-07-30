using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionCastOp : ActionBase
    {
        public ActionCastOp()
            : base(ActionType.CastOp)
        {}
    }
}