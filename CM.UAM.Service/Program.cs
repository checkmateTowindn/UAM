using CM.UM.IService;
using CM.UM.Service;
using Newtonsoft.Json;
using SSCore;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace CM.UAM.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketServer.Start();
            SocketServer.SetCallBack(arg =>
            {
                Console.WriteLine("");
                string data = "没有找到";
                string received = arg;
                int moduleNameLength = received.IndexOf('/');
                String moduleName = String.Empty;
                if (moduleNameLength == -1)
                    moduleName = received;
                else
                    moduleName = received.Substring(0, received.IndexOf('/'));

                if (moduleName.Equals("Index"))
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    IUserService service = new UserService();
                    //获取会员数
                    dic.Add("UserCount", service.UserCount());
                    data = JsonConvert.SerializeObject(dic);
                }
                else
                {
                    data += "模块名为" + moduleName + "的模块";
                }

                return data;
            });
        }
    }
}
