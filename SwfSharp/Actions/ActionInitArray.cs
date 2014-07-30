using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionInitArray : ActionBase
    {
        public ActionInitArray()
            : base(ActionType.InitArray)
        {}
    }
}