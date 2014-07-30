using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionTypeOf : ActionBase
    {
        public ActionTypeOf()
            : base(ActionType.TypeOf)
        {}
    }
}