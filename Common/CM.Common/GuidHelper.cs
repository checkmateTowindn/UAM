using System;

namespace CM.Common
{
    public class GuidHelper
    {
        public static bool IsEmpty(Guid source)
        {
            return object.Equals((object)source, (object)Guid.Empty);
        }

        public static string NewGuid(string format = "D", bool? isUpper = null)
        {
            string str = Guid.NewGuid().ToString(format);
            bool? nullable = isUpper;
            if ((!nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
            {
                str = str.ToUpper();
            }
            else
            {
                nullable = isUpper;
                if ((nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
                    str = str.ToLower();
            }
            return str;
        }
    }
}
