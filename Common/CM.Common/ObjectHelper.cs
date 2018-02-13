using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CM.Common
{
    public class ObjectHelper
    {
        public static List<T2> Copy<T1, T2>(List<T1> source) where T1 : class where T2 : class
        {
            List<T2> result = new List<T2>();
            if (source == null || source.Count < 1)
                return result;
            source.ForEach((Action<T1>)(o =>
            {
                try
                {
                    result.Add(ObjectHelper.Copy<T1, T2>(o));
                }
                catch
                {
                }
            }));
            return result;
        }

        public static T2 Copy<T1, T2>(T1 source) where T1 : class where T2 : class
        {
            if ((object)source == null)
                return default(T2);
            T2 obj = (T2)Activator.CreateInstance(typeof(T2));
            PropertyInfo[] properties1 = obj.GetType().GetProperties();
            PropertyInfo[] properties2 = source.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo1 in properties1)
            {
                PropertyInfo Ptarget = propertyInfo1;
                try
                {
                    PropertyInfo propertyInfo2 = Enumerable.FirstOrDefault<PropertyInfo>((IEnumerable<PropertyInfo>)properties2, (Func<PropertyInfo, bool>)(o => Ptarget.Name.Equals(o.Name, StringComparison.CurrentCultureIgnoreCase)));
                    Ptarget.SetValue((object)obj, propertyInfo2.GetValue((object)source, (object[])propertyInfo2.GetIndexParameters()), (object[])null);
                }
                catch
                {
                }
            }
            return obj;
        }

        public T Clone<T>(T RealObject)
        {
            using (Stream serializationStream = (Stream)new MemoryStream())
            {
                IFormatter formatter = (IFormatter)new BinaryFormatter();
                formatter.Serialize(serializationStream, (object)RealObject);
                serializationStream.Seek(0L, SeekOrigin.Begin);
                return (T)formatter.Deserialize(serializationStream);
            }
        }

        public static IList<T> DataTableToIList<T>(DataTable _DataTable) where T : class
        {
            IList<T> list = (IList<T>)new List<T>();
            if (_DataTable == null || _DataTable.Rows.Count < 1)
                return list;
            Type type = typeof(T);
            foreach (DataRow dataRow in (InternalDataCollectionBase)_DataTable.Rows)
            {
                T obj = (T)Activator.CreateInstance(typeof(T));
                foreach (PropertyInfo propertyInfo in type.GetProperties())
                {
                    if (_DataTable.Columns.Contains(propertyInfo.Name))
                    {
                        try
                        {
                            propertyInfo.SetValue((object)obj, Convert.ChangeType(dataRow[propertyInfo.Name], propertyInfo.PropertyType), (object[])null);
                        }
                        catch
                        {
                            try
                            {
                                switch (propertyInfo.PropertyType.Name)
                                {
                                    case "Guid":
                                        propertyInfo.SetValue((object)obj, (object)Guid.Parse(dataRow[propertyInfo.Name].ToString()), (object[])null);
                                        break;
                                    default:
                                        propertyInfo.SetValue((object)obj, dataRow[propertyInfo.Name], (object[])null);
                                        break;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static List<string> ToList(object source)
        {
            return Enumerable.ToList<string>(Enumerable.Select<object, string>(Enumerable.Select<PropertyInfo, object>((IEnumerable<PropertyInfo>)source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public), (Func<PropertyInfo, object>)(p => p.GetValue(source, (object[])null))), (Func<object, string>)(o => o == null ? "" : o.ToString())));
        }

        public static StringBuilder DataTableToJson(DataTable _DataTable, Dictionary<string, string> keys = null)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (_DataTable == null || _DataTable.Rows.Count < 1 || _DataTable.Columns.Count < 1)
                return stringBuilder.Append("[]");
            if (keys == null)
                keys = new Dictionary<string, string>();
            string[] strArray1 = new string[_DataTable.Columns.Count - 1];
            string[] strArray2 = new string[_DataTable.Columns.Count - 1];
            for (int index = 0; index < strArray1.Length; ++index)
            {
                strArray1[index] = _DataTable.Columns[index].ColumnName;
                int num = !Enumerable.Contains<string>((IEnumerable<string>)keys.Keys, strArray1[index]) ? 1 : (string.IsNullOrWhiteSpace(keys[strArray1[index]]) ? 1 : 0);
                strArray2[index] = num != 0 ? strArray1[index] : keys[strArray1[index].Trim()];
            }
            string columnName = _DataTable.Columns[_DataTable.Columns.Count - 1].ColumnName;
            string index1 = columnName;
            if (Enumerable.Contains<string>((IEnumerable<string>)keys.Keys, columnName) && !string.IsNullOrWhiteSpace(keys[columnName]))
                index1 = keys[index1];
            stringBuilder.Append("[");
            foreach (DataRow dataRow in (InternalDataCollectionBase)_DataTable.Rows)
            {
                stringBuilder.Append("{");
                for (int index2 = 0; index2 < strArray1.Length; ++index2)
                    stringBuilder.Append("\"").Append(strArray2[index2]).Append("\":\"").Append(dataRow[strArray1[index2]]).Append("\",");
                stringBuilder.Append("\"").Append(index1).Append("\":\"").Append(dataRow[columnName]).Append("\"},");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append("]");
            return stringBuilder;
        }
    }
}
