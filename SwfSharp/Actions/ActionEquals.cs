using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionEquals : ActionBase
    {
        public ActionEquals()
            : base(ActionType.Equals)
        {}
    }
}