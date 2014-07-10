using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwfSharp;

namespace SwfTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var swf = SwfFile.FromStream(File.OpenRead("TestLZMA.swf"));
            var swf2 = SwfFile.FromStream(File.OpenRead("TestZLIB.swf"));
        }
    }
}
