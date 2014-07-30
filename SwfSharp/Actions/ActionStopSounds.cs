using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStopSounds : ActionBase
    {
        public ActionStopSounds()
            : base(ActionType.StopSounds)
        {}
    }
}