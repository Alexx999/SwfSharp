using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionRemoveSprite : ActionBase
    {
        public ActionRemoveSprite()
            : base(ActionType.RemoveSprite)
        {}
    }
}