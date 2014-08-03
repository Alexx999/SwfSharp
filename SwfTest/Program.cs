using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwfSharp;
using SwfSharp.Tags;

namespace SwfTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles("./").Where(f => f.EndsWith(".swf"));

            var swfs = new ConcurrentDictionary<string, SwfFile>();

            //files.AsParallel().ForAll(f => swfs.AddOrUpdate(f, SwfFile.FromFile(f), (s, file) => file));

            foreach (var file in files)
            {
                var sw = Stopwatch.StartNew();
                swfs[file] = SwfFile.FromFile(file);
                Debug.WriteLine("File {0} opened in {1}ms", file, sw.ElapsedMilliseconds);
            }

            foreach (var swfFile in swfs)
            {
                var sw = Stopwatch.StartNew();
                swfFile.Value.ToFile(swfFile.Key + "unp");
                Debug.WriteLine("File {0} saved in in {1}ms", swfFile.Key, sw.ElapsedMilliseconds);
            }
        }
    }
}
