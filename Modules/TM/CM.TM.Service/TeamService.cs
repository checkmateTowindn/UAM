using CM.Common;
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
        AjaxMsgResult result = new AjaxMsgResult();
        public AjaxMsgResult Add(UC_Team tm)
        {
            result.Success = false;
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
            result.Source = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("insert into UC_Team(id,name,description,leader,createtime,createuser,sign,logo,projectcount,status) values (?Id,?Name,?Description,?Leader,?CreateTime,?CreateUser,?Sign,?Logo,?ProjectCount,?Status)", dic);
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
            result.Source = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("update UC_Team set status=0  where id in(?Id)", dic);
            if (Convert.ToInt32(result.Source) > 0)
            {
                result.Success = true;
            }
            return result;
        }
        public AjaxMsgResult Update(UC_Team tm)
        {
            result.Success = false;
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
            result.Source = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteNonQuery("update UC_Team set name=?Name,description=?Description,leader=?Leader,createtime=?CreateTime,createuser=?CreateUser,sign=?Sign,logo=?Logo,projectcount=?ProjectCount,status=?Status where id=?Id", dic);
            if (Convert.ToInt32(result.Source) > 0)
            {
                result.Success = true;
            }
            return result;
        }
        public AjaxMsgResult Query(UC_Team tm, int count)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "count", count }
            };
            result.Source = DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_Team>("select * from UC_Team order by createtime limit ?count", dic);
            return result;
        }
        public AjaxMsgResult Get(String id)
        {
            var dic = new Dictionary<String, Object>()
            {
                { "id", id }
            };
            result.Source= DataBaseFactory.GetDataBase(DataBaseType.main).ExecuteReader<UC_Team>("select * from UC_Team where id=?id", dic)[0];
            return result;
        }
    }
}
