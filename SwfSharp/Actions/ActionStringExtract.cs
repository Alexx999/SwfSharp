using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStringExtract : ActionBase
    {
        public ActionStringExtract()
            : base(ActionType.StringExtract)
        {}
    }
}