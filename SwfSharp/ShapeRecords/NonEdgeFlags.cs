using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    public class NonEdgeFlags
    {
        public bool StateNewStyles { get; set; }
        public bool StateLineStyle { get; set; }
        public bool StateFillStyle1 { get; set; }
        public bool StateFillStyle0 { get; set; }
        public bool StateMoveTo { get; set; }

        internal void FromStream(BitReader reader)
        {
            StateNewStyles = reader.ReadBoolBit();
            StateLineStyle = reader.ReadBoolBit();
            StateFillStyle1 = reader.ReadBoolBit();
            StateFillStyle0 = reader.ReadBoolBit();
            StateMoveTo = reader.ReadBoolBit();
        }

        public bool AllZero()
        {
            return !StateNewStyles && !StateLineStyle && !StateFillStyle1 && !StateFillStyle0 && !StateMoveTo;
        }
    }
}