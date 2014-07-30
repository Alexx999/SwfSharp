using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionPreviousFrame : ActionBase
    {
        public ActionPreviousFrame()
            : base(ActionType.PreviousFrame)
        {}
    }
}