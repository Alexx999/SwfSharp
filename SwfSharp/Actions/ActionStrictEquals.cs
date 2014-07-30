using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStrictEquals : ActionBase
    {
        public ActionStrictEquals()
            : base(ActionType.StrictEquals)
        {}
    }
}