using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionEndDrag : ActionBase
    {
        public ActionEndDrag()
            : base(ActionType.EndDrag)
        {}
    }
}