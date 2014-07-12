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
            var swf1 = SwfFile.FromStream(File.OpenRead("TestLZMA.swf"));
            var swf2 = SwfFile.FromStream(File.OpenRead("TestZLIB.swf"));
            var swf3 = SwfFile.FromStream(File.OpenRead("expressInstall.swf"));
            var swf4 = SwfFile.FromStream(File.OpenRead("Economics.swf"));
            var swf5 = SwfFile.FromStream(File.OpenRead("relog.swf"));
            //var swf6 = SwfFile.FromStream(File.OpenRead("blend.swf"));
            var swf7 = SwfFile.FromStream(File.OpenRead("23272_kidspoof.swf"));
            var swf8 = SwfFile.FromStream(File.OpenRead("11940_AYB_1_.swf"));
            var swf9 = SwfFile.FromStream(File.OpenRead("10603_sperm.swf"));
        }
    }
}
