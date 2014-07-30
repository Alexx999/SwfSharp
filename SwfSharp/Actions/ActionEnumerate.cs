using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionEnumerate : ActionBase
    {
        public ActionEnumerate()
            : base(ActionType.Enumerate)
        {}
    }
}