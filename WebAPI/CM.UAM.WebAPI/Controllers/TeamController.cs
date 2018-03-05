using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CM.Common;
using CM.TM.Model;

namespace CM.UAM.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Team")]
    public class TeamController : Controller
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
        public JsonResult Add(UC_Team data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Delete(UC_Team data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Update(UC_Team data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Query(UC_Team data)
        {
            return Json(result);
        }
    }
}