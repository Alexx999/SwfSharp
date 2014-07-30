using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionMultiply : ActionBase
    {
        public ActionMultiply()
            : base(ActionType.Multiply)
        {}
    }
}