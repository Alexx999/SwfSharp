using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionInitObject : ActionBase
    {
        public ActionInitObject()
            : base(ActionType.InitObject)
        {}
    }
}