using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionTargetPath : ActionBase
    {
        public ActionTargetPath()
            : base(ActionType.TargetPath)
        {}
    }
}