using CM.Common;
using CM.AM.Model;
using System;
using System.Collections.Generic;


namespace CM.AM.IService
{
    public interface IAppService
    {

        AjaxMsgResult Add(UC_AppInfo model);
        //AjaxMsgResult Del(List<string> ids, UC_AppInfo operationUser);
        //AjaxMsgResult Update(List<string> ids, UC_AppInfo model, UC_AppInfo operationUser);
        AjaxMsgResult Query(UC_AppInfo model, int orderType, int pageSize = 10, int pageIndex = 0, int recordCount = 0);
        AjaxMsgResult Verify(UC_AppInfo model);
    }
}
