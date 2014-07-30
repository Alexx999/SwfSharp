using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStringEquals : ActionBase
    {
        public ActionStringEquals()
            : base(ActionType.StringEquals)
        {}
    }
}