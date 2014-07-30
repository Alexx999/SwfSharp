using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionGetVariable : ActionBase
    {
        public ActionGetVariable()
            : base(ActionType.GetVariable)
        {}
    }
}