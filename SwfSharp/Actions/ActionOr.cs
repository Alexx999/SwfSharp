using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionOr : ActionBase
    {
        public ActionOr()
            : base(ActionType.Or)
        {}
    }
}