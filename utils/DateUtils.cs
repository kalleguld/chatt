using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace utils
{
    public static class DateUtils
    {
        public readonly static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);
        public static long ToMilis(this DateTime dt)
        {
            return (long)(dt.ToUniversalTime() - Epoch).TotalMilliseconds;
        }

        public static DateTime? FromMilisN(string s)
        {
            if (s == null) return null;

            long l;
            if (long.TryParse(s, out l))
            {
                return Epoch.AddMilliseconds(l);
            }
            return null;
        }
    }
}
