using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace utils
{
    public static class BoolUtils
    {
        public static bool? ParseN(string s)
        {
            if (s == null) return null;
            var sl = s.ToLower();
            switch (sl)
            {
                case "true":
                case "yes":
                case "y":
                case "1":
                    return true;
                case "false":
                case "no":
                case "n":
                case "0":
                    return false;
                default:
                    return null;
            }
        }
    }
}
