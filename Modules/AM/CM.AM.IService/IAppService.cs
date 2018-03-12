using CM.Common;
using CM.TM.Model;
using System;
using System.Collections.Generic;


namespace CM.TM.IService
{
    public interface IAppService<App> where App : class
    {
        AjaxMsgResult Add(App app);
        AjaxMsgResult Delete(List<string> id);
        AjaxMsgResult Update(App app);
        AjaxMsgResult Query(App app, int count);
        AjaxMsgResult Get(String id);
    }
}
