using System;
using System.Collections.Generic;

namespace CM.TM.IService
{
    public interface ITeamService<Team> where Team : class
    {

        int Add(Team tm);
        int Delete(List<string> id);
        int Update(Team tm);
        IList<Team> Query(Team tm,int count);
        Team Get(String id);
    }
}
