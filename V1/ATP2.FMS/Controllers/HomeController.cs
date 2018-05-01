using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FMS.Core.Service.Interfaces;

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

            return View();
        }

        public ActionResult Contact()
        {
           // ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}