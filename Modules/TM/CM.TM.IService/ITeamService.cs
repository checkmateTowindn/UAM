using CM.Common;
using System;
using System.Collections.Generic;

namespace CM.TM.IService
{
    public interface ITeamService<Team> where Team : class
    {

        AjaxMsgResult Add(Team tm);
        AjaxMsgResult Delete(List<string> id);
        AjaxMsgResult Update(Team tm);
        AjaxMsgResult Query(Team tm,int count);
        AjaxMsgResult Get(String id);
    }
}
