using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;
using Newtonsoft.Json;

namespace ATP2.FMS.Controllers
{
    public class LogInController : Controller
    {
        private IAuthenticationService _service;

        public LogInController(IAuthenticationService service)
        {
            _service = service;
        }
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserInfo userInfo)
        {
            try
            {
                var result = _service.Login(userInfo.Email, userInfo.Password);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("Login", userInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var obj = _service.GetByEmail(userInfo.Email);

            var jasonUserInfo = JsonConvert.SerializeObject(obj.Data);
            FormsAuthentication.SetAuthCookie(jasonUserInfo, false);
           // CurrentUser.User = obj.Data;

            if (obj.Data.UserType.Equals("Owner"))
                return RedirectToAction("OwnerForm","User");

            else
            {
                return RedirectToAction("Login");

            }
        }

    }
}