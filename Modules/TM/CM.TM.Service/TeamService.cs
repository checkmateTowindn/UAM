using CM.Common.Data;
using CM.TM.IService;
using CM.TM.Model;
using System;
using System.Collections.Generic;

namespace CM.TM.Service
{
    public class TeamService : ITeamService<UC_Team>
    {
        UC_Team team = new UC_Team();
        public int Add(UC_Team tm)
        {
            Dictionary<String, Object> dic = new Dictionary<String, Object>()
            {
                { "Id", tm.Id },
                { "Name", tm.Name },
                { "Description", tm.Description },
                { "Leader", tm.Leader },
                 { "CreateTime", tm.CreateTime },
                  { "CreateUser", tm.CreateUser },
                  { "Sign", tm.Sign },
                  { "Logo", tm.Logo },
                   { "ProjectCount", tm.ProjectCount },
                   { "Status", tm.Status },
            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("insert into UC_Team(id,name,description,leader,createtime,createuser,sign,logo,projectcount,status) values (:Id,:Name,:Description,:Leader,:CreateTime,:CreateUser,:Sign,:Logo,:ProjectCount,:Status)", dic);
        }
        public int Delete(List<string> id)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "Id", string.Join(",", id.ToArray())}

            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("update UC_Team set status=0  where id in(:Id)", dic);
        }
        public int Update(UC_Team tm)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "Id", tm.Id},
                { "Name", tm.Name },
                { "Description", tm.Description },
                { "Leader", tm.Leader },
                 { "CreateTime", tm.CreateTime },
                  { "CreateUser", tm.CreateUser },
                  { "Sign", tm.Sign },
                  { "Logo", tm.Logo },
                   { "ProjectCount", 0 },
                   { "Status", tm.Status },
            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("update UC_Team set name=:Name,description=:Description,leader=:Leader,createtime=:CreateTime,createuser=:CreateUser,sign=:Sign,logo=:Logo,projectcount=:ProjectCount,status=:Status where id=:Id", dic);
        }
        public IList<UC_Team> Query(UC_Team tm, int count)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "count", count }
            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_Team>("select * from UC_Team order by createtime limit :count", dic);
        }
        public UC_Team Get(String id)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "id", id }
            };
            return DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_Team>("select * from UC_Team where id=:id", dic)[0];
        }
    }
}
