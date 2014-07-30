using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStringGreater : ActionBase
    {
        public ActionStringGreater()
            : base(ActionType.StringGreater)
        {}
    }
}