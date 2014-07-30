using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionDelete : ActionBase
    {
        public ActionDelete()
            : base(ActionType.Delete)
        {}
    }
}