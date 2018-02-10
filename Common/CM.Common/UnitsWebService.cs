using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CM.Common
{
    public class UnitsWebService
    {
        public static string QuerySoapWebService(String URL, String MethodName, Hashtable Pars, string XmlNs, int Timeout=30000)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = Timeout;
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            request.Headers.Add("SOAPAction", "\"" + XmlNs + MethodName + "\"");

            byte[] data = EncodeParsToSoap(Pars, XmlNs, MethodName);

            request.ContentLength = data.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();

            StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.UTF8);
            return sr.ReadToEnd();
        }

        private static byte[] EncodeParsToSoap(Hashtable Pars, String XmlNs, String MethodName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"></soap:Envelope>");

            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(decl, doc.DocumentElement);

            XmlElement soapBody = doc.CreateElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement soapMethod = doc.CreateElement(MethodName);
            soapMethod.SetAttribute("xmlns", XmlNs);
            foreach (string k in Pars.Keys)
            {
                XmlElement soapPar = doc.CreateElement(k);
                soapPar.InnerXml = ObjectToSoapXml(Pars[k]);
                soapMethod.AppendChild(soapPar);
            }
            soapBody.AppendChild(soapMethod);
            doc.DocumentElement.AppendChild(soapBody);
            return Encoding.UTF8.GetBytes(doc.OuterXml);
        }
        private static string ObjectToSoapXml(object o)
        {
            XmlSerializer mySerializer = new XmlSerializer(o.GetType());
            MemoryStream ms = new MemoryStream();
            mySerializer.Serialize(ms, o);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));
            if (doc.DocumentElement != null)
            {
                return doc.DocumentElement.InnerXml;
            }
            else
            {
                return o.ToString();
            }
        }

        //const String Url = "http://192.168.33.179:8081/WebService.asmx";
        public static RequestResult GetWebservice(string Url,string methed, Hashtable pars, string XmlNs = "http://tempuri.org/", int Timeout = 120000)
        {
            RequestResult result = new RequestResult();
            result.start = DateTime.Now;
            try
            {
                result.message = UnitsWebService.QuerySoapWebService(Url, methed, pars, XmlNs, Timeout);
                result.succ = true;
            }
            catch (Exception err1)
            {
                result.message = err1.Message;
                result.succ = false;
            }
            result.end = DateTime.Now;
            return result;
        }
    }

    public class RequestResult {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public double span
        {
            get
            {
                return end.Subtract(start).TotalMilliseconds;
            }
        }
        public string message { get; set; }
        public bool succ { get; set; }
    }
}
