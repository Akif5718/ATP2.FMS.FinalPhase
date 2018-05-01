using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;

namespace ATP2.FMS.Controllers
{
    public class LogInController : Controller
    {
        private IAuthenticationService _service;

        public LogInController(IAuthenticationService service)
        {
            _service = service;
        }
        public ActionResult LogInForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginForm(UserInfo userInfo)
        {
            try
            {
                var result = _service.Login(userInfo.Email, userInfo.Password);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("LoginForm", userInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //var jasonUserInfo = JsonConvert.SerializeObject(obj.Data);
            //FormsAuthentication.SetAuthCookie(jasonUserInfo, false);
            //CurrentUser.User = obj.Data;

            var obj = _service.GetByEmail(userInfo.Email);
            if (obj.Data.UserType.Equals("Owner"))
                return RedirectToAction("RegisterForm","User");

            else
            {
                return RedirectToAction("LoginForm");

            }
        }

    }
}