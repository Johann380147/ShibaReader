using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibaReader.Utils
{
    public static class StringUtils
    {
        public static int ToInteger(string str)
        {
            int.TryParse(str, out int i);
            return i;
        }

        public static string GetLeadingZeroes(int num, int numOfDigits)
        {
            string format = "{0:";
            for (int i = 0; i < numOfDigits; i++)
            {
                format += "0";
            }
            format += "}";

            return string.Format(format, num);
        }

        public static string ReplaceFirst(this string str, string search, string replacement)
        {
            int pos = str.IndexOf(search);
            if (pos < 0)
            {
                return str;
            }
            return str.Substring(0, pos) + replacement + str.Substring(pos + search.Length);
        }
        public static string ReplaceLast(this string str, string search, string replacement)
        {
            int pos = str.LastIndexOf(search);
            if (pos < 0)
            {
                return str;
            }
            return str.Substring(0, pos) + replacement + str.Substring(pos + search.Length);
        }
    }
}
