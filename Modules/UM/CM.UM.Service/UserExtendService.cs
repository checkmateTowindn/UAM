using CM.Common;
using CM.Common.MySQL;
using CM.UM.IService;
using CM.UM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Service
{
   public class UserExtendService: IUserExtendService
    {
        AjaxMsgResult result = new AjaxMsgResult();
        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AjaxMsgResult Add(string userId)
        {
            result.Success = false;
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                StringBuilder sql = new StringBuilder();
                sql.Append(@" INSERT INTO UC_UserExtend(Id,QQ,OpenId,RealName,AddRess,UpdateTime) VALUES (?Id,?QQ,?OpenId,?RealName,?AddRess,?UpdateTime)");
                dic.Add("Id", userId);
                dic.Add("QQ", null);
                dic.Add("OpenId", null);
                dic.Add("RealName", null);
                dic.Add("AddRess", null);
                dic.Add("UpdateTime", DateTime.Now);
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

    }
}
