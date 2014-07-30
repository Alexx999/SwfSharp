using System;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionPush : ActionBase
    {
        public PushType Type { get; set; }
        public string String { get; set; }
        public float Float { get; set; }
        public byte RegisterNumber { get; set; }
        public bool Boolean { get; set; }
        public double Double { get; set; }
        public int Integer { get; set; }
        public ushort Constant { get; set; }


        public ActionPush()
            : base(ActionType.Push)
        { }

        public enum PushType : byte
        {
            String = 0,
            Float = 1,
            Null = 2,
            Undefined = 3,
            Register = 4,
            Boolean = 5,
            Double = 6,
            Integer = 7,
            Constant8 = 8,
            Constant16 = 9
        }
    }
}