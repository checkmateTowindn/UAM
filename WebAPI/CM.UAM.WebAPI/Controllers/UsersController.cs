using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CM.Common;
using Microsoft.AspNetCore.Cors;
using CM.UAM.WebAPI.Models;

namespace CM.UAM.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    [EnableCors("any")]
    public class UsersController : Controller
    {
        AjaxMsgResult result = new AjaxMsgResult();
        // GET: api/Users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public IActionResult Login(string token)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register(string token)
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(Login data)
        {
            


            return Json(result);
        }
        [HttpPost]
        public JsonResult Register(Register data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Update(UserInfo data)
        {
            return Json(result);
        }
        [HttpPost]
        public JsonResult Delete(Register data)
        {
            return Json(result);
        }
    }
}
