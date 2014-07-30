using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionSetProperty : ActionBase
    {
        public ActionSetProperty()
            : base(ActionType.SetProperty)
        {}
    }
}