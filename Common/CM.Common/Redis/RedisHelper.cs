using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RedisHelper
    {
        private static string conn = "slightcold.date:6379,Password=123456";
        //private static ConfigurationOptions conn = new ConfigurationOptions() {
        //    ServiceName = "slightcold.date:6379",
        //    EndPoints = 6379,
        //    DefaultDatabase =1,
        //    Password = "123456",

        //};

        public static string StringGet(string key)
        {
            try
            {
                using (var client = ConnectionMultiplexer.Connect(conn))
                {
                    return client.GetDatabase().StringGet(key);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string[] StringGetMany(string[] keyStrs)
        {
            var count = keyStrs.Length;
            var keys = new RedisKey[count];
            var addrs = new string[count];

            for (var i = 0; i < count; i++)
            {
                keys[i] = keyStrs[i];
            }
            try
            {
                using (var client = ConnectionMultiplexer.Connect(conn))
                {
                    var values = client.GetDatabase().StringGet(keys);
                    for (var i = 0; i < values.Length; i++)
                    {
                        addrs[i] = values[i];
                    }
                    return addrs;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool StringSet(string key, string value, TimeSpan ts)
        {

            using (var client = ConnectionMultiplexer.Connect(conn))
            {
                return client.GetDatabase().StringSet(key, value, ts);
            }
        }
        public static bool StringSetMany(string[] keysStr, string[] valuesStr)
        {
            var count = keysStr.Length;
            var keyValuePair = new KeyValuePair<RedisKey, RedisValue>[count];
            for (int i = 0; i < count; i++)
            {
                keyValuePair[i] = new KeyValuePair<RedisKey, RedisValue>(keysStr[i], valuesStr[i]);
            }
            using (var client = ConnectionMultiplexer.Connect(conn))
            {
                return client.GetDatabase().StringSet(keyValuePair);
            }
        }
        public static bool Set<T>(string key, T t, TimeSpan ts)
        {
            String str = JsonConvert.SerializeObject(t);
            using (var client = ConnectionMultiplexer.Connect(conn))
            {
                return client.GetDatabase().StringSet(key, str, ts);
            }
        }
        public static T Get<T>(string key) where T : class
        {
            using (var client = ConnectionMultiplexer.Connect(conn))
            {
                String strValue = client.GetDatabase().StringGet(key);
                return string.IsNullOrEmpty(strValue) ? null : JsonConvert.DeserializeObject<T>(strValue);
            }
        }
        public static bool Remove(String key)
        {
            using (var client = ConnectionMultiplexer.Connect(conn))
            {
                if(client.GetDatabase().KeyDelete(key))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
