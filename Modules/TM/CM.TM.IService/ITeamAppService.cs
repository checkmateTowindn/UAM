using CM.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.TM.IService
{
    public interface ITeamAppService<TeamApp> where TeamApp : class
    {

        AjaxMsgResult Add(TeamApp ta);
        AjaxMsgResult Delete(List<string> id);
        AjaxMsgResult Update(TeamApp tm);
        AjaxMsgResult Query(List<string> id);
    }
}
