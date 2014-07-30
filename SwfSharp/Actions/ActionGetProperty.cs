using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionGetProperty : ActionBase
    {
        public ActionGetProperty()
            : base(ActionType.GetProperty)
        {}
    }
}