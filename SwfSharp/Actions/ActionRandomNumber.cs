using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionRandomNumber : ActionBase
    {
        public ActionRandomNumber()
            : base(ActionType.RandomNumber)
        {}
    }
}