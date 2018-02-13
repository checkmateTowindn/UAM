
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CM.Common
{
    public class TypesHelper
    {
        public static bool ObjectToBool(object source, bool defValue = false)
        {
            bool result = false;
            if (TypesHelper.ObjectIsNullOrWhiteSpace(source) || !bool.TryParse(source.ToString(), out result))
                return defValue;
            return result;
        }

        public static Decimal ObjectTodecimal(object source, Decimal defValue = 0M)
        {
            Decimal result = new Decimal(0);
            if (TypesHelper.ObjectIsNullOrWhiteSpace(source) || !Decimal.TryParse(source.ToString(), out result))
                return defValue;
            return result;
        }

        public static long ObjectToInt64(object source, long defValue = 0L)
        {
            long result = 0L;
            if (TypesHelper.ObjectIsNullOrWhiteSpace(source) || !long.TryParse(source.ToString(), out result))
                return defValue;
            return result;
        }

        public static int ObjectToInt(object source, int defValue = 0)
        {
            int result = 0;
            if (TypesHelper.ObjectIsNullOrWhiteSpace(source) || !int.TryParse(source.ToString(), out result))
                return defValue;
            return result;
        }

        public static float ObjectToFloat(object source, float defValue = 0.0f)
        {
            float result = 0.0f;
            if (TypesHelper.ObjectIsNullOrWhiteSpace(source) || !float.TryParse(source.ToString(), out result))
                return defValue;
            return result;
        }

        public static bool ObjectToDateTime(object source, ref DateTime Result)
        {
            try
            {
                DateTime dateTime = Convert.ToDateTime(source);
                if (dateTime == DateTime.MinValue)
                    throw new Exception();
                Result = dateTime;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DateTime ObjectToDateTime(object source, DateTime defValue)
        {
            DateTime result;
            if (TypesHelper.ObjectIsNullOrWhiteSpace(source) || !DateTime.TryParse(source.ToString(), out result))
                return defValue;
            return result;
        }

        public static DateTime ObjectToDateTime(object source)
        {
            return Convert.ToDateTime(source);
        }

        public static string ObjectToString(object source, string defValue = "", bool IsRidNullstring = false, string IFvalueToDefault = null)
        {
            if (TypesHelper.ObjectIsNullOrWhiteSpace(source) || (IsRidNullstring && source.ToString().Trim().ToLower() == "null" || IFvalueToDefault == source.ToString().Trim()))
                return defValue;
            return source.ToString().Trim();
        }

        public static IList<T> StringToList<T>(object source, char separator = ',')
        {
            IList<T> list = (IList<T>)new List<T>();
            string str1 = TypesHelper.ObjectToString(source, "", false, (string)null);
            char[] chArray = new char[1]
            {
        separator
            };
            foreach (string str2 in str1.Split(chArray))
            {
                try
                {
                    list.Add((T)Convert.ChangeType((object)str2, typeof(T), (IFormatProvider)CultureInfo.InvariantCulture));
                }
                catch
                {
                }
            }
            return list;
        }

        public static IList<T> StringToList<T>(object source, T defValue, char separator = ',')
        {
            IList<T> list = (IList<T>)new List<T>();
            string str1 = TypesHelper.ObjectToString(source, "", false, (string)null);
            char[] chArray = new char[1]
            {
        separator
            };
            foreach (string str2 in str1.Split(chArray))
            {
                try
                {
                    list.Add((T)Convert.ChangeType((object)str2, typeof(T), (IFormatProvider)CultureInfo.InvariantCulture));
                }
                catch
                {
                    list.Add(defValue);
                }
            }
            return list;
        }

        public static bool ObjectIsNull(object source)
        {
            return object.Equals(source, (object)null);
        }

        public static bool ObjectIsNullOrWhiteSpace(object source)
        {
            return TypesHelper.ObjectIsNull(source) || string.IsNullOrWhiteSpace(source.ToString());
        }
    }
}
