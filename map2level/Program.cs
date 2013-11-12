using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace map2level
{
    class Program
    {
        private static bool noPause = false;
        private static string toLoadFrom = string.Empty;
        private static string ToSaveTo = string.Empty;

        static void Main(string[] args)
        {
            args = new string[] { "-loadfrom", "desert.map", "-save", @"C:\Users\Nick\Documents\visual studio 2012\Projects\ZMProject\map2level\bin\Debug"};
            if (args.Length != -1 && args.Length != 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-nopause")
                    {
                        noPause = true;
                    }
                    else if (args[i] == "-loadfrom")
                    {
                        toLoadFrom = args[i + 1];
                        i++;
                    }
                    else if (args[i] == "-save")
                    {
                        ToSaveTo = args[i + 1];
                        i++;
                    }
                }
            }
            else
            {
                Console.WriteLine("You cannot use mod2level without ModTools!");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("map2level - A tool used to convert GtkRadiants *.map file to xml format .lvl.");
            MapParser parse = new MapParser();
            Console.WriteLine("Parsing level...");
            parse.parse(toLoadFrom);
            Console.WriteLine("Level parsed, invoking output, Merge 10, 60 Join...");
            if (ToSaveTo != string.Empty)
            {
                using (StreamWriter fs = new StreamWriter(Path.GetFileNameWithoutExtension(toLoadFrom) + ".lvl"))
                {
                    fs.WriteLine("<Level>");
                    parse.OutputMapXML(0.125f, fs, 10, 60);
                    fs.WriteLine("</Level>");
                }
            }
            else
            {
                using (StreamWriter fs = new StreamWriter(ToSaveTo + "\\map.lvl"))
                {
                    fs.WriteLine("<Level>");
                    parse.OutputMapXML(0.125f, fs, 10, 60);
                    fs.WriteLine("</Level>");
                }
            }
            Console.WriteLine("Output complete. [" + Path.GetFileNameWithoutExtension(toLoadFrom) + ".lvl]");
            if (noPause == false)
            {
                Console.ReadKey();
            }
        }
    }
}
