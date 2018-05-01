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
    public class HomeController : Controller
    {
        private IUserInfoService _infoService; 

        public HomeController(IUserInfoService infoService)
        {
            _infoService = infoService;
        }

        public ActionResult Index()
        {
            var str = _infoService.GetAll();
            return View();
        }

        public ActionResult About()
        {
           // ViewBag.Message = "Your application description page.";
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(User.Identity.Name);


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            FormsAuthentication.SignOut();
            return View();
        }
    }
}