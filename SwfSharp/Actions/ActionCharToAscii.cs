using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionCharToAscii : ActionBase
    {
        public ActionCharToAscii()
            : base(ActionType.CharToAscii)
        {}
    }
}