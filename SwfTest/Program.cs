using System;
using System.Collections.Concurrent;
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
            var files = Directory.GetFiles("./", "*.swf");

            var swfs = new ConcurrentDictionary<string, SwfFile>();

            files.AsParallel().ForAll(f => swfs.AddOrUpdate(f, SwfFile.FromFile(f), (s, file) => file));
        }
    }
}
