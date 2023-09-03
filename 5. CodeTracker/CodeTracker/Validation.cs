using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTracker
{
    internal class Validation
    {
        public Validation() { }
        public static DateTime ValidDateFormat(string date)
        {
            string format = "yyyy-MM-dd HH:mm:ss";

            if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return DateTime.Parse(date);
            }

            throw new Exception();
        }
    }
}
