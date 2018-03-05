using System;
using System.Collections.Generic;
using System.Text;
using CM.Common.Interface;
using StackExchange.Redis;
using CM.Common;
using CM.Common.PostgreSQL;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using CM.Common.MySQL;
using CM.Common.Model;
using CM.TM.IService;
using CM.TM.Model;
using CM.Common.Data;

namespace CM.TM.Service
{
    public class AppService : IAppService<UC_AppInfo>
    {
        AjaxMsgResult result = new AjaxMsgResult();

        #region 注册APP
        /// <summary>
        /// 注册app
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(UC_AppInfo model)
        {

            Dictionary<string, object> dic = new Dictionary<string, object>();
            StringBuilder sql = new StringBuilder();
            sql.Append(@" INSERT INTO UC_AppInfo(id,appname,token,description,addressurl,status,createuser,createtime) VALUES (:Id,:AppName,:Token,:Description,:AddressURL,:Status,:CreateUser,:CreateTime)");
            model.Id = NewData.NewId("APP");
            model.Token = Com.SHA512Encrypt(model.Id + "checkmate");
            dic.Add("Id", model.Id);
            dic.Add("AppName", model.AppName);
            dic.Add("Token", model.Token);
            dic.Add("Description", model.Description);
            dic.Add("AddressURL", model.AddressURL);
            dic.Add("Status", model.Status);
            dic.Add("CreateUser", model.CreateUser);
            dic.Add("CreateTime", DateTime.Now);
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery(sql.ToString(), dic);


        }
        /// <summary>
        /// 修改appInfo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(UC_AppInfo model)
        {

            Dictionary<string, object> dic = new Dictionary<string, object>();
            StringBuilder sql = new StringBuilder();
            sql.Append(@" Update UC_AppInfo set id=:Id,appname=:AppName,token=:Token,description=:Description,addressurl=:AddressURL,status=:Status,createuser=:CreateUser,createtime:CreateTime WHERE id=:Id");
            dic.Add("Id", model.Id);
            dic.Add("AppName", model.AppName);
            dic.Add("Token", model.Token);
            dic.Add("Description", model.Description);
            dic.Add("AddressURL", model.AddressURL);
            dic.Add("Status", model.Status);
            dic.Add("CreateUser", model.CreateUser);
            dic.Add("CreateTime", DateTime.Now);
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery(sql.ToString(), dic);


        }
        public int Delete(List<string> id)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "Id", string.Join(",", id.ToArray())}

            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("delete UC_AppInfo where id in(:Id)", dic);
        }
        #endregion
        /// <summary>
        /// 查询应用
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orderType"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<UC_AppInfo> Query(UC_AppInfo model, int count)
        {

            Dictionary<string, object> dic = new Dictionary<string, object>() {
                { "count",count}
            };
            List<string> fileds = new List<string>();
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_AppInfo>("select * from UC_AppInfo order by createtime limit :count", dic);

        }
        /// <summary>
        /// 获取该应用的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UC_AppInfo Get(String id)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "id", id }
            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_AppInfo>("select * from UC_AppInfo where id=:id", dic)[0];

        }
    }
}