using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionNextFrame : ActionBase
    {
        public ActionNextFrame()
            : base(ActionType.NextFrame)
        {}
    }
}