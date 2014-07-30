using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionDefineLocal : ActionBase
    {
        public ActionDefineLocal()
            : base(ActionType.DefineLocal)
        {}
    }
}