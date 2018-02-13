using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace CM.Common
{
        public static class Extens
        {
            public static bool eBool(this object source, bool defValue = false)
            {
                return TypesHelper.ObjectToBool(source, defValue);
            }

            public static Decimal eDecimal(this object source, Decimal defValue = 0M)
            {
                return TypesHelper.ObjectTodecimal(source, defValue);
            }

            public static long eLong(this object source, long defValue = 0L)
            {
                return TypesHelper.ObjectToInt64(source, defValue);
            }

            public static int eInt(this object source, int defValue = 0)
            {
                return TypesHelper.ObjectToInt(source, defValue);
            }

            public static float eFloat(this object source, float defValue = 0.0f)
            {
                return TypesHelper.ObjectToFloat(source, defValue);
            }

            public static DateTime eDateTime(this object source)
            {
                return TypesHelper.ObjectToDateTime(source);
            }

            public static bool eDateTime(this object source, ref DateTime Result)
            {
                return TypesHelper.ObjectToDateTime(source, ref Result);
            }

            public static DateTime eDateTime(this object source, DateTime defValue)
            {
                return TypesHelper.ObjectToDateTime(source, defValue);
            }

            public static string eString(this object source, string defValue = "", bool IsRidNullstring = false, string IFvalueToDefault = null)
            {
                return TypesHelper.ObjectToString(source, defValue, IsRidNullstring, IFvalueToDefault);
            }

            public static IList<T> e_StringToList<T>(this object source, char separator = ',')
            {
                return TypesHelper.StringToList<T>(source, separator);
            }

            public static IList<T> e_StringToList<T>(this object source, T defValue, char separator = ',')
            {
                return TypesHelper.StringToList<T>(source, defValue, separator);
            }

            public static bool e_IsNull(this object source)
            {
                return TypesHelper.ObjectIsNull(source);
            }

            public static bool e_IsNullValue(this object source)
            {
                return TypesHelper.ObjectIsNullOrWhiteSpace(source);
            }

            public static bool e_IsEmpty(this Guid source)
            {
                return GuidHelper.IsEmpty(source);
            }

            public static IList<T> e_DataTableToIList<T>(this DataTable source) where T : class
            {
                return ObjectHelper.DataTableToIList<T>(source);
            }

            public static StringBuilder e_DataTableToJson(this DataTable source, Dictionary<string, string> keys = null)
            {
                return ObjectHelper.DataTableToJson(source, keys);
            }

            public static string e_Regex(this object source, string pattern = "\\r\\n", string replacement = "")
            {
                if (source == null)
                    return (string)null;
                if (string.IsNullOrWhiteSpace(source.ToString()))
                    return source.ToString();
                return Regex.Replace(source.ToString(), pattern, replacement);
            }
        }
}
