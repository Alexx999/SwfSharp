using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionToInteger : ActionBase
    {
        public ActionToInteger()
            : base(ActionType.ToInteger)
        {}
    }
}