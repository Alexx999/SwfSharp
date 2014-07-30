using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionToString : ActionBase
    {
        public ActionToString()
            : base(ActionType.ToString)
        {}
    }
}