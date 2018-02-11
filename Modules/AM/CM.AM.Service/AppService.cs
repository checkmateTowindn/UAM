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
using CM.AM.IService;
using CM.AM.Model;

namespace CM.AM.Service
{
    public class AppService : IAppService
    {
        AjaxMsgResult result = new AjaxMsgResult();

        #region 注册APP
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AjaxMsgResult Add(UC_AppInfo model)
        {
            result.Success = false;
            try
            {
               
                if (string.IsNullOrWhiteSpace(model.AppName) || string.IsNullOrWhiteSpace(model.AddressURL))
                {
                    result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                    result.Msg = "应用名称和地址不能为空！";
                    return result;
                }
                model.Token = Com.SHA512Encrypt(NewData.NewId());
                Dictionary<string, object> dic = new Dictionary<string, object>();
                StringBuilder sql = new StringBuilder();
                sql.Append(@" INSERT INTO UC_AppInfo(Id,AppName,Token,Description,AddressURL,Status,CreateUser,CreateTime) VALUES (?Id,?AppName,?Token,?Description,?AddressURL,?Status,?CreateUser,?CreateTime)");
                model.Id= NewData.NewId("APP");
                model.Token= Com.SHA512Encrypt(model.Id+"checkmate");
                dic.Add("Id", model.Id);
                dic.Add("AppName", model.AppName);
                dic.Add("LoginName", model.Token);
                dic.Add("PassWord", Com.SHA512Encrypt(model.Description));
                dic.Add("IsValid", model.AddressURL);
                dic.Add("Status", model.Status);
                dic.Add("Mobile", model.CreateUser);
                dic.Add("CreateUser", model.CreateUser);
                dic.Add("CreateTime", DateTime.Now);
                int count = MySqlHelper.ExecuteNonQuery(sql.ToString(), dic);
                if (count == 1)
                {
                    result.Success = true;
                }
                else
                {
                    result.Msg = "添加失败，请检查数据合法性!";
                }
            }
            catch (Exception e)
            {
                result.Msg = e.ToString();
            }
            return result;
        }

        #endregion
        /// <summary>
        /// 查询该应用
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orderType"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public AjaxMsgResult Query(UC_AppInfo model, int orderType, int pageSize = 10, int pageIndex = 0, int recordCount = 0)
        {
            result.Success = false;
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                List<string> fileds = new List<string>();
                fileds.Add("*");
                string orderColumnName = "CreateTime";
                StringBuilder sublistSql = new StringBuilder();
                sublistSql.Append(@" SELECT * FROM UC_AppInfo WHERE 1=1 ");
                if (model.Id != null)
                {
                    sublistSql.Append(@" AND Id=?Id ");
                    dic.Add("Id", model.Id);
                }
                if (model.Status != null)
                {
                    sublistSql.Append(@" AND Status LIKE ?Status ");
                    dic.Add("Status", "%" + model.Status + "%");
                }
                if (model.AppName != null)
                {
                    sublistSql.Append(@" AND AppName LIKE ?AppName ");
                    dic.Add("AppName", "%" + model.AppName + "%");
                }
                if (model.CreateUser != null)
                {
                    sublistSql.Append(@" AND IsValid=?IsValid ");
                }
                
                RecordAsPageModel page = new RecordAsPageModel();
                page.FldName = "";
                page.OrderType = Convert.ToInt16(orderType);
                page.PageIndex = pageIndex;
                page.SortName = orderColumnName;
                IList<UC_AppInfo> list = (MySqlHelper.ExecuteReader<UC_AppInfo>(sublistSql.ToString(), dic));
            }
            catch (Exception e)
            {
                result.Msg = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AjaxMsgResult Verify(UC_AppInfo model)
        {
            result.Success = false;
            try
            {
                if (string.IsNullOrWhiteSpace(model.Token))
                {
                    result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                    return result;
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                StringBuilder sql = new StringBuilder();
                sql.Append(@" select * from UC_User WHERE 1=1 AND Token=?Token ");
                dic.Add("?Token",model.Token);
                List<UC_AppInfo> data = MySqlHelper.ExecuteReader<UC_AppInfo>(sql.ToString(), dic);
                if (data != null)
                {
                    if (data.Count > 0)
                    {
                        result.Success = true;
                        result.Source = data;
                    }
                }

            }
            catch (Exception e)
            {
                result.Msg = e.ToString();
            }
            return result;
        }
    }
}