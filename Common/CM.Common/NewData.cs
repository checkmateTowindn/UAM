using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Common
{
    public static class NewData
    {
        public static string NewId()
        {
            string str = Guid.NewGuid().ToString();
            str = str.Replace("-", "");
            return str;
        }
        /// <summary>
        /// 加前缀
        /// </summary>
        /// <param name="prefix">前缀不超过四个字符</param>
        /// <returns></returns>
        public static string NewId(string prefix)
        {
            string str = Guid.NewGuid().ToString();
            str = prefix+str.Replace("-", "");
            return str;
        }
    }
}
