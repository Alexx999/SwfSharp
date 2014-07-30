using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionMBCharToAscii : ActionBase
    {
        public ActionMBCharToAscii()
            : base(ActionType.MBCharToAscii)
        {}
    }
}