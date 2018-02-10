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
    [Route("api/Log")]
    public class LogController : Controller
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
        public JsonResult Add(LoginLog data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Delete(LoginLog data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Update(LoginLog data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Query(LoginLog data)
        {
            return Json(result);
        }
    }
}