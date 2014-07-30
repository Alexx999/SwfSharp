using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionToNumber : ActionBase
    {
        public ActionToNumber()
            : base(ActionType.ToNumber)
        {}
    }
}