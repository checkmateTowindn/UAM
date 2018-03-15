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
        private static Dictionary<String, IService> App = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            SocketServerBase server = new SocketServerBase();
            server.NewClientAccepted += Server_NewClientAccepted;
            server.Start();

            Console.WriteLine("enter any key to exit.");
            Console.ReadKey();
        }

        private static void Server_NewClientAccepted(Socket client, ISocketSession session)
        {
            Console.WriteLine("");
            AsyncSocketSession ass = session as AsyncSocketSession;

            ass.SetReceiveHandler(arg =>
            {
                Console.WriteLine("");
                string data = "没有找到";
                string received = System.Text.Encoding.UTF8.GetString(arg.Buffer, arg.Offset, arg.BytesTransferred);
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

                ass.Send(data);
            });
        }
    }
}
