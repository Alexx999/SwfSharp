using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStringLength : ActionBase
    {
        public ActionStringLength()
            : base(ActionType.StringLength)
        {}
    }
}