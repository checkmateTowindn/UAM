using CM.Common;
using CM.Common.PostgreSQL;
using CM.UM.IService;
using CM.UM.Model;
using CM.UM.Service;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        IUserService con = new UserService();
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
        public void Verify()
        {
            UC_User model = new UC_User();
            model.Mobile = "18600522656";
            model.PassWord = "123456";
            result = con.Verify(model);
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
