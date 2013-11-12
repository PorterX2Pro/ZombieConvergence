using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;
namespace modlinker
{
    class Program
    {
        private static bool noPause = false;
        private static string toLoadFrom = string.Empty;
        private static string ModName =string.Empty;

        static void Main(string[] args)
        {
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
                    else if (args[i] == "-modname")
                    {
                        ModName = args[i + 1];
                        i++;
                    }
                }
            }
            else
            {
                Console.WriteLine("You cannot run modlinker without ModTools!");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("modlinker - Links *.lvl and mod together to create a *.zmod");
            using (ZipStorer zip = ZipStorer.Create(toLoadFrom + "\\" + ModName + ".zmod", "Zombie Convergence Mod"))
            {
                using (StreamReader readFile = new StreamReader(toLoadFrom + "\\mod.lvl"))
                {
                    string File = readFile.ReadToEnd();
                    using (StreamWriter writeFile = new StreamWriter(toLoadFrom + "\\mod.btmp"))
                    {
                        writeFile.Write(File);
                    }
                    zip.AddFile(ZipStorer.Compression.Deflate, toLoadFrom + "\\mod.btmp", "mod.lvl", "map");
                }
                Console.WriteLine("Added map to mod file");
                foreach (string File in System.IO.Directory.GetFiles(toLoadFrom + "\\textures"))
                {
                    Console.WriteLine("Added texture [" + System.IO.Path.GetFileNameWithoutExtension(File));
                    zip.AddFile(ZipStorer.Compression.Deflate, File, System.IO.Path.GetFileName(File), "texture");
                }
                foreach (string File in System.IO.Directory.GetFiles(toLoadFrom + "\\scripts"))
                {
                    Console.WriteLine("Added script [" + System.IO.Path.GetFileNameWithoutExtension(File));
                    zip.AddFile(ZipStorer.Compression.Deflate, File, System.IO.Path.GetFileName(File), "script");
                }
            }
            Console.WriteLine("Cleaning up...");
            System.Threading.Thread.Sleep(1000);
            System.IO.File.Delete(toLoadFrom + "\\mod.btmp");
            Console.WriteLine("Mod created at [" + toLoadFrom + "\\" + ModName + ".zmod");
            if (noPause == false)
            {
                Console.ReadKey();
            }
        }
    }
}
