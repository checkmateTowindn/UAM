using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CM.Common;
using CM.UAM.WebAPI.Models;

namespace CM.UAM.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/App")]
    public class AppController : Controller
    {
        AjaxMsgResult result = new AjaxMsgResult();
        public IActionResult Get()
        {
            return View();
        }
        public IActionResult AddOrEdit()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Add(AppInfo data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Delete(AppInfo data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Update(AppInfo data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Query(AppInfo data)
        {
            return Json(result);
        }
    }
}