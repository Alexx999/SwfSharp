using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionReturn : ActionBase
    {
        public ActionReturn()
            : base(ActionType.Return)
        {}
    }
}