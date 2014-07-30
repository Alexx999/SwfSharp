using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStop : ActionBase
    {
        public ActionStop()
            : base(ActionType.Stop)
        {}
    }
}