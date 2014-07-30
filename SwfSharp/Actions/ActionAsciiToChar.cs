using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionAsciiToChar : ActionBase
    {
        public ActionAsciiToChar()
            : base(ActionType.AsciiToChar)
        {}
    }
}