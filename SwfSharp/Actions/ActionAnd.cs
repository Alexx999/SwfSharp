using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionAnd : ActionBase
    {
        public ActionAnd()
            : base(ActionType.And)
        {}
    }
}