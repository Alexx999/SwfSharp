using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionPlay : ActionBase
    {
        public ActionPlay()
            : base(ActionType.Play)
        {}
    }
}