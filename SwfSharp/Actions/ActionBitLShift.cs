using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionBitLShift : ActionBase
    {
        public ActionBitLShift()
            : base(ActionType.BitLShift)
        {}
    }
}