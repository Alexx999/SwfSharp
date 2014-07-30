using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStartDrag : ActionBase
    {
        public ActionStartDrag()
            : base(ActionType.StartDrag)
        {}
    }
}