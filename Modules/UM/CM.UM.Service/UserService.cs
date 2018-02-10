using System;
using System.Collections.Generic;
using System.Text;
using CM.UM.Model;
using CM.Common.Interface;
using StackExchange.Redis;
using CM.Common;
using CM.Common.PostgreSQL;
using CM.UM.IService;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using CM.Common.MySQL;
using CM.Common.Model;

namespace CM.UM.Service
{
    public class UserService : IUserService
    {
        AjaxMsgResult result = new AjaxMsgResult();
        //IDataBase con = new PostgreSQL();
        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AjaxMsgResult Add(UC_User model)
        {
            result.Success = false;
            try
            {
                if (IsRepeat(model.LoginName, model.Mobile,model.Email) == true)
                {
                    result.State = AjaxMsgResult.StateEnum.IsExist;
                    result.Msg = "添加失败，存在相同的数据!";
                    return result;
                }
                if(string.IsNullOrWhiteSpace(model.LoginName)&&string.IsNullOrWhiteSpace(model.Mobile)&&string.IsNullOrWhiteSpace(model.Email))
                {
                    result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                    result.Msg = "添加失败，注册用户名为空!";
                    return result;
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                StringBuilder sql = new StringBuilder();
                sql.Append(@" INSERT INTO UC_User(Id,UserName,LoginName,PassWord,IsValid,Status,Mobile,CreateUser,CreateTime,UserExtendId) VALUES (?Id,?UserName,?LoginName,?PassWord,?IsValid,?Status,?Mobile,?CreateUser,?CreateTime,?UserExtendId)");
                dic.Add("Id", NewData.NewId("YH"));
                dic.Add("UserName", model.UserName);
                dic.Add("LoginName", model.LoginName);
                dic.Add("PassWord", Com.SHA512Encrypt(model.PassWord));
                dic.Add("IsValid", model.IsValid);
                dic.Add("Status", model.Status);
                dic.Add("Mobile", model.Mobile);
                dic.Add("CreateUser", model.CreateUser);
                dic.Add("CreateTime", DateTime.Now);
                dic.Add("UserExtendId", model.UserExtendId);
                int count = MySqlHelper.ExecuteNonQuery(sql.ToString(), dic);
                if (count == 1)
                {
                    result = Verify(model);
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

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="operationUser"></param>
        /// <returns></returns>
        public AjaxMsgResult Del(List<string> ids, UC_User operationUser)
        {
            result.Success = false;
            try
            {
                if (ids.Count == 0)
                {
                    result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                    result.Msg = "未检测到要修改的id！";
                    return result;
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                StringBuilder sql = new StringBuilder();
                sql.Append(@"UPDATE UC_User SET IsValid=1 WHERE 1=1 ");
                for (int i = 0; i < ids.Count; i++)
                {

                    if (ids.Count > 0)
                    {
                        if (i > 0)
                        {
                            sql.AppendFormat(@" OR Id=?Id{0} ", i);
                            if (i == ids.Count - 1)
                            {
                                sql.Append(@" ) ");
                            }
                        }
                        else
                        {
                            sql.AppendFormat(@" AND ( Id=?Id{0} ", i);
                        }
                    }
                    else
                    {
                        sql.AppendFormat(@" AND Id=?Id{0} ", i);
                    }
                    dic.Add("Id" + i, ids[i]);
                }
                int count = MySqlHelper.ExecuteNonQuery(sql.ToString(), dic);
                if (count == ids.Count)
                {
                    result.Success = true;
                }
                else
                {
                    result.Msg = "删除失败，请检查数据合法性!";
                }
            }
            catch (Exception e)
            {
                result.Msg = e.ToString();
            }
            return result;
        }
        #endregion

        public AjaxMsgResult Update(List<string> ids, UC_User model, UC_User operationUser)
        {
            result.Success = false;
            try
            {
                if (ids.Count == 0)
                {
                    result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                    result.Msg = "未检测到要修改的id！";
                    return result;
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                StringBuilder sql = new StringBuilder();
                sql.Append(@"UPDATE UC_User SET ");
                if (model.UserName != null)
                {
                    sql.Append(@" UserName=?UserName,");
                    dic.Add("UserName",model.UserName);
                }
                if (model.PassWord != null)
                {
                    sql.Append(@" PassWord=?PassWord,");
                    dic.Add("PassWord",Com.SHA512Encrypt(model.PassWord));
                }
                if (model.IsValid != null)
                {
                    sql.Append(@" IsValid=?IsValid,");
                    dic.Add("IsValid", model.IsValid);
                }
                if (model.Status != null)
                {
                    sql.Append(@" Status=?Status,");
                    dic.Add("Status", model.Status);
                }
                if (model.Mobile != null)
                {
                    sql.Append(@" Mobile=?Mobile,");
                    dic.Add("Mobile", model.Mobile);
                }
                if (model.Email != null)
                {
                    sql.Append(@" Email=?Email,");
                    dic.Add("Email", model.Email);
                }
                //去掉最后一个逗号
                sql.Remove(sql.Length-1,1);
                sql.Append(@" WHERE 1=1 ");
                for (int i = 0; i < ids.Count; i++)
                {

                    if (ids.Count > 1)
                    {
                        if (i > 0)
                        {
                            sql.AppendFormat(@" OR Id=?Id{0} ", i);
                            if (i == ids.Count - 1)
                            {
                                sql.Append(@" ) ");
                            }
                        }
                        else
                        {
                            sql.AppendFormat(@" AND ( Id=?Id{0} ", i);
                        }
                    }
                    else
                    {
                        sql.AppendFormat(@" AND Id=?Id{0} ", i);
                    }
                    dic.Add("Id"+i,ids[i]);
                }
                int count = MySqlHelper.ExecuteNonQuery(sql.ToString(), dic);
                if (count == ids.Count)
                {
                    result.Success = true;
                }
                else
                {
                    result.Msg = "修改失败，请检查数据合法性!";
                }
            }
            catch (Exception e)
            {
                result.Msg = e.ToString();
            }
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AjaxMsgResult Query(UC_User model,int orderType=1, int pageSize = 10, int pageIndex = 0,int recordCount=0)
        {
            result.Success = false;
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                List<string> fileds = new List<string>();
                fileds.Add("*");
                string orderColumnName = "CreateTime";
                StringBuilder sublistSql = new StringBuilder();
                sublistSql.Append(@" SELECT * FROM UC_User WHERE 1=1 ");
                if (model.Id != null)
                {
                    sublistSql.Append(@" AND Id=?Id ");
                    dic.Add("Id", model.Id);
                }
                if (model.UserName != null)
                {
                    sublistSql.Append(@" AND UserName LIKE ?UserName ");
                    dic.Add("UserName", "%"+model.UserName + "%");
                }
                if (model.LoginName != null)
                {
                    sublistSql.Append(@" AND LoginName LIKE ?LoginName ");
                    dic.Add("LoginName", "%" + model.LoginName + "%");
                }
                if (model.IsValid != null)
                {
                    sublistSql.Append(@" AND IsValid=?IsValid ");
                }
                if (model.Status != null)
                {
                    sublistSql.Append(@" AND Status=?Status ");
                }
                if (model.Mobile != null)
                {
                    sublistSql.Append(@" AND Mobile LIKE ?Mobile ");
                    dic.Add("Mobile", "%" + model.Mobile + "%");
                }
                if (model.CreateUser != null)
                {
                    sublistSql.Append(@" AND CreateUser=?CreateUser ");
                }
                if (model.CreateTime != null)
                {
                    sublistSql.Append(@" AND IsValid=?IsValid ");
                }
                if (model.Email != null)
                {
                    sublistSql.Append(@" AND Email LIKE ?Email ");
                    dic.Add("Email", "%" + model.Email + "%");
                }
                RecordAsPageModel page = new RecordAsPageModel();
                page.FldName = "";
                page.OrderType= Convert.ToInt16(orderType);
                page.PageIndex = pageIndex;
                page.SortName = orderColumnName;
                IList<UC_User> list = (MySqlHelper.ExecuteReader<UC_User>(sublistSql.ToString(),dic));
            }
            catch (Exception e)
            {
                result.Msg = e.ToString();
            }
            return result;
        }

        #region 验证
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AjaxMsgResult Verify(UC_User model)
        {
            result.Success = false;
            try
            {
                if (string.IsNullOrWhiteSpace(model.LoginName)&& string.IsNullOrWhiteSpace(model.Mobile)&& string.IsNullOrWhiteSpace(model.Email))
                {
                    result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                    return result;
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                StringBuilder sql = new StringBuilder();
                sql.Append(@" select * from UC_User WHERE 1=1 ");
                if (!string.IsNullOrWhiteSpace(model.LoginName))
                {
                    sql.Append(@" AND LoginName=?LoginName");
                    dic.Add("LoginName", model.LoginName);
                }
                if (!string.IsNullOrWhiteSpace(model.Mobile))
                {
                    sql.Append(@" AND Mobile=?Mobile");
                    dic.Add("Mobile", model.Mobile);
                }
                if (!string.IsNullOrWhiteSpace(model.Email))
                {
                    sql.Append(@" AND Email=?Email");
                    dic.Add("Email", model.Email);
                }
                sql.Append(@" AND PassWord=?PassWord ");
                dic.Add("PassWord", Com.SHA512Encrypt(model.PassWord));
               // List<UC_User> data = null;
                List<UC_User> data = MySqlHelper.ExecuteReader<UC_User>(sql.ToString(), dic);
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
        #endregion

        #region 判断是否重复
        /// <summary>
        /// 判断是否重复
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="mobile">手机号码</param>
        /// <returns></returns>
        public bool IsRepeat(string loginName = null, string mobile = null,string email=null)
        {
            bool isRepeat = true;
            try
            {
                if (loginName == null && mobile == null&&email==null)
                {
                    result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                    return isRepeat;
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                StringBuilder sql = new StringBuilder();
                sql.Append(@" select * from UC_User WHERE 1=1 ");
                if (loginName != null)
                {
                    sql.Append(@" AND LoginName=?LoginName ");
                    dic.Add("LoginName", loginName);
                }
                if (mobile != null)
                {
                    sql.Append(@" AND Mobile=?Mobile ");
                    dic.Add("Mobile", mobile);
                }
                if (email != null)
                {
                    sql.Append(@" AND Email=?Email ");
                    dic.Add("Email", email);
                }
               // List<UC_User> data = null;
                List <UC_User> data = MySqlHelper.ExecuteReader<UC_User>(sql.ToString(), dic);
                
                if (data!=null)
                {
                    if (data.Count > 0)
                    {
                        isRepeat = true;
                    }
                }
                else
                {
                    isRepeat = false;
                }

            }
            catch (Exception e)
            {
                result.Msg = e.ToString();
            }
            return isRepeat;
        }
        #endregion


        //public AjaxMsgResult SSO()
        //{
        //    //生成token
        //    var token = Guid.NewGuid().ToString();
        //    //写入token
        //    //Common.Common.AddCookie("token", token, Int32.Parse(ConfigurationManager.AppSettings["Timeout"]));
        //    ////写入凭证
        //    RedisClient client = new RedisClient(ConfigurationManager.AppSettings["RedisServer"], 6379);
        //    client.Set<UserInfo>(token, userInfo);
        //}
    }
}
