using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionNewObject : ActionBase
    {
        public ActionNewObject()
            : base(ActionType.NewObject)
        {}
    }
}