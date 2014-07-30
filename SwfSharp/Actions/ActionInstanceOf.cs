using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionInstanceOf : ActionBase
    {
        public ActionInstanceOf()
            : base(ActionType.InstanceOf)
        {}
    }
}