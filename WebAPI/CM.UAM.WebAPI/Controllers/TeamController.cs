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
        public JsonResult Add(Team data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Delete(Team data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Update(Team data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Query(Team data)
        {
            return Json(result);
        }
    }
}