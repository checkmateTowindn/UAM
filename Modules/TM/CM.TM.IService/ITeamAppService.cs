using System;
using System.Collections.Generic;
using System.Text;

namespace CM.TM.IService
{
    public interface ITeamAppService<TeamApp> where TeamApp : class
    {

        int Add(TeamApp ta);
        int Delete(List<string> id);
        int Update(TeamApp tm);
        IList<TeamApp> Query(List<string> id);
    }
}
