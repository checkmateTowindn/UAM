using CM.Common;
using CM.UM.Model;
using System;
using System.Collections.Generic;

namespace CM.UM.IService
{
    public interface IUserService
    {
        AjaxMsgResult Add(UC_User model);
        AjaxMsgResult Del(List<string> ids, UC_User operationUser);
        AjaxMsgResult Update(List<string> ids, UC_User model, UC_User operationUser);
        AjaxMsgResult Query(UC_User model, int orderType, int pageSize = 10, int pageIndex = 0, int recordCount = 0);
        AjaxMsgResult Verify(UC_User model);
    }
}
