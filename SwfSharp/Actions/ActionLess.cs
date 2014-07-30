using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionLess : ActionBase
    {
        public ActionLess()
            : base(ActionType.Less)
        {}
    }
}