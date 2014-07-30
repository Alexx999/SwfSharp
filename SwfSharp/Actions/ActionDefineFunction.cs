using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionDefineFunction : ActionBase
    {
        public ActionDefineFunction()
            : base(ActionType.DefineFunction)
        {}
    }
}