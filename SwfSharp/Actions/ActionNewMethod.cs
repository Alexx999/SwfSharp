using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionNewMethod : ActionBase
    {
        public ActionNewMethod()
            : base(ActionType.NewMethod)
        {}
    }
}