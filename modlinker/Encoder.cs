using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace modlinker
{
    internal class Encoder
    {
        public static string EncodeIntoBase(string Encode)
        {
            return Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(Encode));
        }

        public static string DecodeIntoString(string Decode)
        {
            return System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(Decode));
        }
    }
}
