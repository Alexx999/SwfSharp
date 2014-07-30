using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionGetTime : ActionBase
    {
        public ActionGetTime()
            : base(ActionType.GetTime)
        {}
    }
}