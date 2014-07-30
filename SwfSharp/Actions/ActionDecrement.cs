using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionDecrement : ActionBase
    {
        public ActionDecrement()
            : base(ActionType.Decrement)
        {}
    }
}