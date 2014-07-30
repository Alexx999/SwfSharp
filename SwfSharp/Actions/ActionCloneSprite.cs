using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionCloneSprite : ActionBase
    {
        public ActionCloneSprite()
            : base(ActionType.CloneSprite)
        {}
    }
}