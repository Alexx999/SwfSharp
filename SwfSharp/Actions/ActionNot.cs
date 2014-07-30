using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionNot : ActionBase
    {
        public ActionNot()
            : base(ActionType.Not)
        {}
    }
}