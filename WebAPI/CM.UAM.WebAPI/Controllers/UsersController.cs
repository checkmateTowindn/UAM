using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CM.Common;
using Microsoft.AspNetCore.Cors;
using CM.UAM.WebAPI.Models;
using CM.UM.IService;
using CM.UM.Service;
using CM.UM.Model;
using CM.AM.IService;
using CM.AM.Service;
using CM.AM.Model;
using Common;
using Newtonsoft.Json;

namespace CM.UAM.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    [EnableCors("any")]
    public class UsersController : Controller
    {
        AjaxMsgResult result = new AjaxMsgResult();
        IUserService service = new UserService();
        IAppService appService = new AppService();
        UC_User model = new UC_User();
        UC_AppInfo appInfoModel = new UC_AppInfo();
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
        public JsonResult Login(string URL, string Token,string LoginName, string PassWord)
        {
            result.Success = false;
            //判断token是否正确
            if (string.IsNullOrWhiteSpace(URL) || string.IsNullOrWhiteSpace(Token))
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "登录来源不明！";
                return Json(result);
            }
            appInfoModel.Token = Token;
            if (appService.Verify(appInfoModel).Success == false)
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "登录来源不明！";
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(LoginName) || string.IsNullOrWhiteSpace(PassWord))
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "用户名和密码不能为空！";
                return Json(result);
            }
            else {

                model.PassWord = PassWord;
                if (Com.isTelephone(LoginName))//如果是手机
                    model.Mobile = LoginName;
                else if (Com.isMail(LoginName))//如果是邮箱
                    model.Email = LoginName;
                else//如果是登录名
                    model.LoginName = LoginName;
                result = service.Verify(model);
                if (result.Success == true)
                {
                    //缓存
                    List<UC_User> list =(List<UC_User>)result.Source;
                    string key = list[0].Id;
                    string jsonData = JsonConvert.SerializeObject(list[0]);
                    RedisHelper.StringSet(key, jsonData, new TimeSpan(1, 1, 1, 1, 1));
                    result.Source = URL;//返回此URL

                }
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult Register(string URL, string Token,string LoginName = null, string Mobile = null, string Email = null, string PassWord = null, string PassWord2 = null)
        {
            result.Success = false;
            if (string.IsNullOrWhiteSpace(URL)|| string.IsNullOrWhiteSpace(Token))
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "注册来源不明！";
                return Json(result);
            }

            if (string.IsNullOrWhiteSpace(LoginName) && string.IsNullOrWhiteSpace(Mobile) && string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(PassWord))
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "用户名和密码不能为空！";
                return Json(result);
            }
            if (PassWord.Equals(PassWord2))
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "两次密码不一致！";
                return Json(result);
            }
            else
            {
                model.LoginName = LoginName;
                model.Mobile = Mobile;
                model.Email = Email;
                model.PassWord = PassWord;
                result = service.Add(model);
                if (result.Success == true)
                {
                    //记入Redis   
                    List<UC_User> list =(List<UC_User>)result.Source;
                    string key = list[0].Id;
                    string jsonData = JsonConvert.SerializeObject(list[0]);
                    RedisHelper.StringSet(key, jsonData, new TimeSpan(1, 1, 1, 1, 1));
                    result.Source = URL;//返回此URL
                }
            }
           
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
