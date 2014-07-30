using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionSetVariable : ActionBase
    {
        public ActionSetVariable()
            : base(ActionType.SetVariable)
        {}
    }
}