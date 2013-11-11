using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace ZC.Scripting
{
    public class Load
    {
        public static GSCScript LoadScript(string FileName)
        {
            string scriptCode = string.Empty;
            using (StreamReader readFile = new StreamReader(FileName))
            {
                scriptCode = readFile.ReadToEnd();
                scriptCode = scriptCode.Replace(((char)13).ToString(), "");
            }
            foreach (string Line in scriptCode.Split('\n'))
            {

            }
            return new GSCScript();
        }
    }
}
