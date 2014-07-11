using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwfSharp.ShapeRecords
{
    public class ShapeRecord
    {
        public ShapeRecordType RecordType { get; set; }

        public ShapeRecord(ShapeRecordType recordType)
        {
            RecordType = recordType;
        }
    }
}
