using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionToggleQuality : ActionBase
    {
        public ActionToggleQuality()
            : base(ActionType.ToggleQuality)
        {}
    }
}