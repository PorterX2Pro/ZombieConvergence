using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModTools
{
    internal class ModToolUtil
    {
        public static string ModDir = string.Empty;

        public static void InitDirectories()
        {
            ModDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\mods";
            if (!System.IO.Directory.Exists(ModDir))
            {
                System.IO.Directory.CreateDirectory(ModDir);
            }
        }

        public static List<string> GetMods()
        {
            List<string> toReturn = new List<string>();
            foreach (string Mod in System.IO.Directory.GetDirectories(ModDir))
            {
                toReturn.Add(Mod);
            }
            return toReturn;
        }

        public static bool CreateMod(string ModName)
        {
            if (!System.IO.Directory.Exists(ModDir + "\\" + ModName))
            {
                System.IO.Directory.CreateDirectory(ModDir + "\\" + ModName);
                System.IO.Directory.CreateDirectory(ModDir + "\\" + ModName + "\\textures");
                System.IO.Directory.CreateDirectory(ModDir + "\\" + ModName + "\\scripts");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
