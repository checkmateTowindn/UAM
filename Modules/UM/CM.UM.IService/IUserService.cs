using CM.Common;
using CM.Common.JQuery;
using CM.UM.Model;
using CM.UM.Model.Ext;
using System;
using System.Collections.Generic;

namespace CM.UM.IService
{
    public interface IUserService
    {
        AjaxMsgResult Add(UC_User model, string CreateUser = null);
        AjaxMsgResult Del(List<string> ids, UC_User operationUser);
        AjaxMsgResult Update(List<string> ids, UC_User model, UC_User operationUser);
        AjaxMsgResult Query(UC_User model, int orderType, int pageSize = 10, int pageIndex = 0, int recordCount = 0);
        AjaxMsgResult Verify(UC_User model);
        bool IsRepeat(string loginName = null, string mobile = null, string email = null);
        string UserCount();
        JQResult<UC_User_Ext> Get(JQParas Paras);
    }
}
