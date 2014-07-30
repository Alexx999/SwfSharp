using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionAdd : ActionBase
    {
        public ActionAdd()
            : base(ActionType.Add)
        {}
    }
}