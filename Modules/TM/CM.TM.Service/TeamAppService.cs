using CM.Common;
using CM.Common.Data;
using CM.TM.IService;
using CM.TM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.TM.Service
{
    public class TeamAppService : ITeamAppService<UC_TeamApp>
    {
        UC_TeamApp ta = new UC_TeamApp();
        AjaxMsgResult result = new AjaxMsgResult();
        public AjaxMsgResult Add(UC_TeamApp ta)
        {
            result.Success = false;
            var dic = new Dictionary<string, object>()
             {
                { "Id", ta.Id },
                { "TeamId", ta.TeamId },
                { "AppId", ta.AppId },
                 { "CreateTime", ta.CreateTime },
                  { "CreateUser", ta.CreateUser }
            };
            result.Source = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("insert into UC_TeamApp(id,teamid,appid,leader,createtime,createuser) values (?Id,?TeamId,?AppId,?Leader,?CreateTime,?CreateUser)", dic);
            if (Convert.ToInt32(result.Source) > 0)
            {
                result.Success = true;
            }
            return result;
        }
        public AjaxMsgResult Delete(List<string> id)
        {
            result.Success = false;
            var dic = new Dictionary<String, Object>()
            {
                { "Id", string.Join(",", id.ToArray())}

            };
            result.Source = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("delete UC_TeamApp where id in(?Id)", dic);
            if (Convert.ToInt32(result.Source) > 0)
            {
                result.Success = true;
            }
            return result;
        }
        public AjaxMsgResult Update(UC_TeamApp tm)
        {
            result.Success = false;
            var dic = new Dictionary<string, object>()
             {
                { "Id", ta.Id },
                { "TeamId", ta.TeamId },
                { "AppId", ta.AppId },
                 { "CreateTime", ta.CreateTime },
                  { "CreateUser", ta.CreateUser }
            };
            result.Source = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("update UC_TeamApp set teamid=?TeamId,appid=?AppId,createtime=?CreateTime,createuser=?CreateUser where id=?Id", dic);
            if (Convert.ToInt32(result.Source) > 0)
            {
                result.Success = true;
            }
            return result;
        }
        public AjaxMsgResult Query(List<string> id)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "Id", string.Join(",", id.ToArray())}

            };
            result.Source = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_TeamApp>("select * from UC_TeamApp where id in(?Id)", dic);
            return result;
        }
        public AjaxMsgResult Get(string id)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "Id", id}

            };
            result.Source = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_TeamApp>("select * from UC_TeamApp where id =?Id", dic)[0];
            return result;
        }
    }
}
