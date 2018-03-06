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
using CM.Common.JQuery;
using Newtonsoft.Json;
using CM.UM.Model.Ext;
using CM.Common.Data;

namespace CM.UM.Service
{
    public class UserService : IUserService
    {
        AjaxMsgResult result = new AjaxMsgResult();
        IUserExtendService userExtendService = new UserExtendService();
        //IDataBase con = new PostgreSQL();
        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AjaxMsgResult Add(UC_User model,string CreateUser=null)
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
                sql.Append(@" INSERT INTO UC_User(Id,UserName,LoginName,PassWord,IsValid,Status,Mobile,CreateUser,CreateTime) VALUES (?Id,?UserName,?LoginName,?PassWord,?IsValid,?Status,?Mobile,?CreateUser,?CreateTime)");
                string id = NewData.NewId("YH");
                dic.Add("Id", id);
                dic.Add("UserName", model.UserName);
                dic.Add("LoginName", model.LoginName);
                dic.Add("PassWord", Com.SHA512Encrypt(model.PassWord));
                dic.Add("IsValid",model.IsValid);
                dic.Add("Status", model.Status);
                dic.Add("Mobile", model.Mobile);
                dic.Add("CreateUser", CreateUser==null? id:model.CreateUser);
                dic.Add("CreateTime", DateTime.Now);
                int count = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery(sql.ToString(), dic);
                if (count == 1)
                {
                    if (userExtendService.Add(id).Success == true) {
                        result = Verify(model);
                    }
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
                int count = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery(sql.ToString(), dic);
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
                int count = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery(sql.ToString(), dic);
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
                IList<UC_User> list = (DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_User>(sublistSql.ToString(),true,dic));
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
                IList<UC_User> data = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_User>(sql.ToString(), true, dic);
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
                IList <UC_User> data = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_User>(sql.ToString(), true, dic);
                
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

        /// <summary>
        /// 用户个数
        /// </summary>
        /// <returns></returns>
        public string UserCount()
        {
            result.Success = false;
            string sql = " SELECT COUNT(*) FROM UC_User ";
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteSingle(sql);
        }





        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public JQResult<UC_User_Ext> Get(JQParas Paras)
        {
            //默认初始化
            JQResult<UC_User_Ext> result = new JQResult<UC_User_Ext>();
            result.page = Paras.page;
                Dictionary<string, object> DbParas = new Dictionary<string, object>();

                string SQL_base = "SELECT Id,UserName,LoginName,IsValid,Status,Mobile,Email,CreateUser,DATE_FORMAT(CreateTime,'%Y-%c-%d %h:%i:%s') AS CreateTimeStr FROM UC_User WHERE 1=1 AND IsValid=0 ";

                #region 搜索(注意字符串类型注入)

                StringBuilder serchstr = new StringBuilder();
            UC_User_Ext _csearch;
                if (!string.IsNullOrWhiteSpace(Paras.cSearch))
                    _csearch = JsonConvert.DeserializeObject<UC_User_Ext>(Paras.cSearch);
                else
                    _csearch = new UC_User_Ext();


            if (!string.IsNullOrWhiteSpace(_csearch.UserName))
            {
                if (serchstr.Length > 0) serchstr.Append(" AND ");
                DbParas.Add("@UserName", "%" + _csearch.UserName+"%");
                serchstr.Append("AND UserName LIKE @UserName");
            }
            if (!string.IsNullOrWhiteSpace(_csearch.LoginName))
            {
                if (serchstr.Length > 0) serchstr.Append(" AND ");
                DbParas.Add("@LoginName", "%" + _csearch.LoginName + "%");
                serchstr.Append(" AND LoginName LIKE @LoginName");
            }
            if (!string.IsNullOrWhiteSpace(_csearch.Mobile))
            {
                if (serchstr.Length > 0) serchstr.Append(" AND ");
                DbParas.Add("@Mobile", "%" + _csearch.Mobile + "%");
                serchstr.Append(" AND Mobile LIKE @Mobile");
            }
            if (!string.IsNullOrWhiteSpace(_csearch.Email))
            {
                if (serchstr.Length > 0) serchstr.Append(" AND ");
                DbParas.Add("@Email", "%"+_csearch.Email + "%");
                serchstr.Append(" AND Email LIKE @Email");
            }
            if (_csearch.Status!=null)
            {
                if (serchstr.Length > 0) serchstr.Append(" AND ");
                DbParas.Add("@Status", _csearch.Status);
                serchstr.Append(" AND Status LIKE @Status");
            }

            if (serchstr.Length > 0) SQL_base += serchstr.ToString();
                #endregion

                #region 排序

                string SortUnion = "ORDER BY ";
                //默认排序方式
                SortUnion += string.IsNullOrWhiteSpace(Paras.sidx) ? "__TMP__ID" : Paras.sidx;
                SortUnion += " " + (string.IsNullOrWhiteSpace(Paras.sord) ? "DESC" : Paras.sord);
                #endregion

                PageHelperParas php = new PageHelperParas()
                {
                    types = 1,
                    rows = Paras.rows,
                    page = Paras.page,
                    SortUnion = SortUnion
                };

                if (Paras.export)//导出
                {
                    php.dataSql = string.Format("SELECT {0} FROM ({1}) B", "*", SQL_base);
                    php.exportColumnName = new List<string> { "序号", "名称" };
                    php.exportIgnoreColumnName = new List<string>() { "__TMP__ID", "FormTypeID", "OUID", "ParentID", "Gradecode", "parent", "leaf", "expanded", "loaded", "level" };

                    php.export = Paras.export;
                    php.exportAll = (Paras.exportType == 0);
                    php.exportFileName = Paras.exportFileName;
                    php.start_page = Paras.exportStartPage;
                    php.end_page = Paras.exportEndPage;
                }
                else//分页查询
                {
                    php.dataSql = string.Format("SELECT {0} FROM ({1}) B", "*", SQL_base);
                    php.countSql = string.Format("SELECT {0} FROM ({1}) B", "count(1)", SQL_base);
                }

                PageHelper.Query<UC_User_Ext>(php, ref result, DbParas);
            
            return result;
        }



    }
}
