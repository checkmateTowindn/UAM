﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using CM.Common;
using CM.UM.IService;
using CM.AM.IService;
using CM.UM.Model;
using CM.AM.Model;
using CM.AM.Service;
using CM.UM.Service;

namespace CM.UAM.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class IndexController : Controller
    {
        AjaxMsgResult result = new AjaxMsgResult();
        IUserService service = new UserService();
        IAppService appService = new AppService();
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