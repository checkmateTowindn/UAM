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
        public int Add(UC_TeamApp ta)
        {
            var dic = new Dictionary<string, object>()
             {
                { "Id", ta.Id },
                { "TeamId", ta.TeamId },
                { "AppId", ta.AppId },
                 { "CreateTime", ta.CreateTime },
                  { "CreateUser", ta.CreateUser }
            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("insert into UC_TeamApp(id,teamid,appid,leader,createtime,createuser) values (:Id,:TeamId,:AppId,:Leader,:CreateTime,:CreateUser)", dic);
        }
        public int Delete(List<string> id)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "Id", string.Join(",", id.ToArray())}

            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("delete UC_TeamApp where id in(:Id)", dic);
        }
        public int Update(UC_TeamApp tm)
        {
            var dic = new Dictionary<string, object>()
             {
                { "Id", ta.Id },
                { "TeamId", ta.TeamId },
                { "AppId", ta.AppId },
                 { "CreateTime", ta.CreateTime },
                  { "CreateUser", ta.CreateUser }
            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("update UC_TeamApp set teamid=:TeamId,appid=:AppId,createtime=:CreateTime,createuser=:CreateUser where id=:Id", dic);
        }
        public IList<UC_TeamApp> Query(List<string> id)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "Id", string.Join(",", id.ToArray())}

            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_TeamApp>("select * from UC_TeamApp where id in(:Id)", dic);
        }
        public UC_TeamApp Get(string id)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "Id", id}

            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_TeamApp>("select * from UC_TeamApp where id =:Id", dic)[0];
        }
    }
}
