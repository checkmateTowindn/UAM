using CM.Common;
using CM.TM.Model;
using System;
using System.Collections.Generic;


namespace CM.TM.IService
{
    public interface IAppService<App> where App : class
    {
        int Add(App app);
        int Delete(List<string> id);
        int Update(App app);
        IList<App> Query(App app, int count);
        App Get(String id);
    }
}
