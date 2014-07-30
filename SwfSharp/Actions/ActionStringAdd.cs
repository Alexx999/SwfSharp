using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStringAdd : ActionBase
    {
        public ActionStringAdd()
            : base(ActionType.StringAdd)
        {}
    }
}