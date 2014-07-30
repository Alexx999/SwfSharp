using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionDivide : ActionBase
    {
        public ActionDivide()
            : base(ActionType.Divide)
        {}
    }
}