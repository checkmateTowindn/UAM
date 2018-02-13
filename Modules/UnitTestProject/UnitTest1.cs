using CM.Common;
using CM.Common.PostgreSQL;
using CM.UM.IService;
using CM.UM.Model;
using CM.UM.Service;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using YunpianInternationalSMSApi;
using YunpianInternationalSMSApi.Models;
using YunpianInternationalSMSApi.ReturnModel;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        IUserService con = new UserService();
        IUserExtendService userExtendService = new UserExtendService();
        AjaxMsgResult result = new AjaxMsgResult();
        [TestMethod]
        public void RedisTest()
        {

            RedisHelper.StringSet("1", "2", new System.TimeSpan(1, 1, 1, 1, 1));

            var i = RedisHelper.StringGet("1");

            if (i.Equals("2")) return;

            throw new System.Exception();
        }

        [TestMethod]
        public void PostgreSQLTest()
        {
            //var list = new PostgreSQL().ExecuteReader<Number>("select * from test");
            //list.ForEach(t => Console.WriteLine(t.ToString()));
            UC_User model = new UC_User();
            model.Mobile = "18600522656";
            model.PassWord = "123456";
            result = con.Add(model);
            Console.ReadLine();
        }
        [TestMethod]
        public void Update()
        {
            UC_User model = new UC_User();
            model.Mobile = "18600522656";
            model.PassWord = "123456";
            List<string> ids = new List<string>();
            ids.Add("YH4F2127A3521E42709FD371BF9434C051");
            result = con.Update(ids, model, model);
        }
        [TestMethod]
        public void Sss()
        {
            SmsSingleSendModel sm = new SmsSingleSendModel();
            Config config = new Config("ee4096858a640a3938261e8057a0d8b3");
            sm.mobile = "18600522656";
            sm.apikey = config.apikey;
            Random rd = new Random();
            int number = rd.Next(100000, 999999);
            sm.text = "【问鼎科技】欢迎注册问鼎科技，您的验证码是" + number.ToString();
            IYunpianInternationalSMS ys = new YunpianInternationalSMS();
            string res = ys.SingleSendVerificationCode(sm).DataObj.ToString();
            SmsSingleSendReturnModel descJsonStu = JsonConvert.DeserializeObject<SmsSingleSendReturnModel>(res);//反序列化
        }
        [TestMethod]
        public void Aaa()
        {
            string a = "{\"code\":0,\"msg\":\"发送成功\",\"count\":1,\"fee\":0.05,\"unit\":\"RMB\",\"mobile\":\"18600522656\",\"sid\":21684538470}";
            string jsonData = JsonConvert.SerializeObject(a);
            SmsSingleSendReturnModel descJsonStu = JsonConvert.DeserializeObject<SmsSingleSendReturnModel>(a);
        }

        [TestMethod]
        public void Verify()
        {
            UC_User model = new UC_User();
            model.Mobile = "18600522656";
            model.PassWord = "123456";
            result = con.Verify(model);
        }

        [TestMethod]
        public void addUserExtendService()
        {
            result = userExtendService.Add("YH04eb525ecb64426ea23e209fb7a5982c");
        }
        
        public class Number
        {
            public string a1;
            public string a2;

            public override string ToString()
            {
                return "a1:" + a1 + ";a2:" + a2;
            }
        }
       
    }
}
