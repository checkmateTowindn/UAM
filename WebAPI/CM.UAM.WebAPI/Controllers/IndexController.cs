using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using CM.Common;
using CM.UM.IService;
using CM.TM.IService;
using CM.UM.Model;
using CM.TM.Model;
using CM.TM.Service;
using CM.UM.Service;

namespace CM.UAM.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class IndexController : Controller
    {
        AjaxMsgResult result = new AjaxMsgResult();
        IAppService<UC_AppInfo> appService = new AppService();
        IUserService service = new UserService();
        UC_User userModel = new UC_User();
        UC_AppInfo appInfoModel = new UC_AppInfo();
        [HttpPost]
        public JsonResult Get()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //获取会员数
            dic.Add("UserCount",service.UserCount());
            return Json(dic);
        }
    }
}