﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CM.Common;
using Microsoft.AspNetCore.Cors;
using CM.UM.IService;
using CM.UM.Service;
using CM.UM.Model;
using CM.TM.IService;
using CM.TM.Service;
using CM.TM.Model;
using Common;
using Newtonsoft.Json;
using YunpianInternationalSMSApi;
using YunpianInternationalSMSApi.Models;
using System.Runtime.Serialization.Json;
using YunpianInternationalSMSApi.ReturnModel;
using CM.UAM.WebAPI.Models;
using CM.Common.JQuery;

namespace CM.UAM.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class UsersController : Controller
    {
        AjaxMsgResult result = new AjaxMsgResult();
        IUserService service = new UserService();
        IAppService<UC_AppInfo> appService = new AppService();
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
        public JsonResult Login(Login login)
        {
            result.Success = false;
            //判断token是否正确
            if (string.IsNullOrWhiteSpace(login.URL) || string.IsNullOrWhiteSpace(login.Token))
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "登录来源不明！";
                return Json(result);
            }
            //appInfoModel.Token = Token;
            //if (appService.Verify(appInfoModel).Success == false)
            //{
            //    result.State = AjaxMsgResult.StateEnum.VerifyFailed;
            //    result.Msg = "登录来源不明！";
            //    return Json(result);
            //}
            if (string.IsNullOrWhiteSpace(login.LoginName) || string.IsNullOrWhiteSpace(login.PassWord))
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "用户名和密码不能为空！";
                return Json(result);
            }
            else {

                model.PassWord = login.PassWord;
                if (Com.isTelephone(login.LoginName))//如果是手机
                    model.Mobile = login.LoginName;
                else if (Com.isMail(login.LoginName))//如果是邮箱
                    model.Email = login.LoginName;
                else//如果是登录名
                    model.LoginName = login.LoginName;
                result = service.Verify(model);
                if (result.Success == true)
                {
                    //缓存
                    List<UC_User> list = (List<UC_User>)result.Source;
                    string key = list[0].Id;
                    string jsonData = JsonConvert.SerializeObject(list[0]);
                    RedisHelper.StringSet(key, jsonData, new TimeSpan(30, 0, 0, 0, 0));
                    result.Source = login.URL;//返回此URL

                }
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult Register(Register register)
        {
            result.Success = false;
            var number = RedisHelper.StringGet(register.Mobile+ "registerNumber");
            if (!register.VerifyNumber.Equals(number))
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "验证码错误！";
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(register.LoginName) && string.IsNullOrWhiteSpace(register.Mobile) && string.IsNullOrWhiteSpace(register.Email) && string.IsNullOrWhiteSpace(register.PassWord))
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "用户名和密码不能为空！";
                return Json(result);
            }
            if (!register.PassWord.Equals(register.PassWord2))
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "两次密码不一致！";
                return Json(result);
            }
            else
            {
                model.LoginName = register.LoginName;
                model.Mobile = register.Mobile;
                model.Email = register.Email;
                model.PassWord = register.PassWord;
                model.IsValid=model.IsValid == null?0:model.IsValid;
                model.Status = model.Status == null ? 0 : model.Status;
                result = service.Add(model);
                if (result.Success == true)
                {
                    //记入Redis   
                    List<UC_User> list = (List<UC_User>)result.Source;
                    string key = list[0].Id;
                    string jsonData = JsonConvert.SerializeObject(list[0]);
                    RedisHelper.StringSet(key, jsonData, new TimeSpan(30, 0, 0, 0, 0));
                    result.Source = register.URL;//返回此URL
                }
            }

            return Json(result);
        }
        [HttpPost]
        public JsonResult SendVerifyNumber(SendVerifyNumber sendVerifyNumber)
        {
            
            result.Success = false;
            if (service.IsRepeat(null,sendVerifyNumber.mobile,null))
            {
                result.State = AjaxMsgResult.StateEnum.VerifyFailed;
                result.Msg = "该手机号码已经注册！";
                return Json(result);
            }
            SmsSingleSendModel sm = new SmsSingleSendModel();
            Config config = new Config("ee4096858a640a3938261e8057a0d8b3");
            sm.mobile = sendVerifyNumber.mobile;
            sm.apikey = config.apikey;
            Random rd = new Random();
            int number = rd.Next(100000, 999999);
            sm.text = "【问鼎科技】欢迎注册问鼎科技，您的验证码是" + number.ToString();
            IYunpianInternationalSMS ys = new YunpianInternationalSMS();
            string res = ys.SingleSendVerificationCode(sm).DataObj.ToString();
            DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(SmsSingleSendReturnModel));
            SmsSingleSendReturnModel smsSingleSendReturnModel = JsonConvert.DeserializeObject<SmsSingleSendReturnModel>(res);//反序列化
            if (smsSingleSendReturnModel.code == 0)
            {
                result.Success = true;
                result.Source = sendVerifyNumber.URL;//返回此URL
                result.Msg = "发送成功！";
            }
            RedisHelper.StringSet(sendVerifyNumber.mobile+ "registerNumber", number.ToString(), new TimeSpan(0, 0, 10,0, 0));
            return Json(result);
        }
        [HttpPost]
        public JsonResult GetUserList(JQParas jq)
        {
            return Json(service.Get(jq));
        }
    }
}
