using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CM.Common
{
    public class UntilsHttp
    {
        //public static string QueryPost(String URL , Hashtable Pars,   int Timeout = 30000)
        //{
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
        //    request.Credentials = CredentialCache.DefaultCredentials;
        //    request.Timeout = Timeout;
        //    request.Method = "POST";
        //    request.ContentType = "text/xml; charset=utf-8"; 
        //     var pageHtml = Encoding.UTF8.GetString(pageData)
        //    byte[] data = EncodeParsToSoap(Pars, XmlNs, MethodName);

        //    request.ContentLength = data.Length;
        //    Stream writer = request.GetRequestStream();
        //    writer.Write(data, 0, data.Length);
        //    writer.Close();

        //    StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.UTF8);
        //    return sr.ReadToEnd();
        //}

        #region  向Url发送post请求
        /// <summary>
        /// 向Url发送post请求
        /// </summary>
        /// <param name="postData">发送数据</param>
        /// <param name="uriStr">接受数据的Url</param>
        /// <returns>返回网站响应请求的回复</returns>
        public static string RequestGet(  string URL)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Credentials = CredentialCache.DefaultCredentials;

            request.Method = "get";
            request.AllowAutoRedirect = true;
            request.ContentType = "application/x-www-form-urlencoded";

            Stream responseStream = null;
            try { responseStream = request.GetResponse().GetResponseStream(); }
            catch (Exception e) { throw e; }
            string stringResponse = string.Empty;
            using (StreamReader responseReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
            {
                stringResponse = responseReader.ReadToEnd();
            }
            responseStream.Close();
            return stringResponse;
        }


        /// <summary>
        /// 向Url发送post请求
        /// </summary>
        /// <param name="postData">发送数据</param>
        /// <param name="uriStr">接受数据的Url</param>
        /// <returns>返回网站响应请求的回复</returns>
        public static string RequestPost(string param, string URL)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Credentials = CredentialCache.DefaultCredentials;

            request.Method = "POST";
            request.AllowAutoRedirect = true;
            request.ContentType = "application/x-www-form-urlencoded";
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] data = encoding.GetBytes(param);
            //填充并发送要post的内容 
            request.ContentLength = data.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            Stream responseStream = null;
            try { responseStream = request.GetResponse().GetResponseStream(); }
            catch (Exception e) { throw e; }
            string stringResponse = string.Empty;
            using (StreamReader responseReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
            {
                stringResponse = responseReader.ReadToEnd();
            }
            responseStream.Close();
            return stringResponse;
        }

        #endregion
    }
}
