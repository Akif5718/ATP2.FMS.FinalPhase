using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FMS.Core.Entities;
using FMS.Core.Service;
using FMS.Core.Service.Interfaces;
using Newtonsoft.Json;

namespace ATP2.FMS.Controllers
{
    public class UserController : Controller
    {
 
        private IUserInfoService _uservice;
        private IOwnerService _oservice;
        private IWorkerService _wservice;
        private IEducationalService _eservice;
        private IWorkHistoryService _whservice;

        public UserController(IUserInfoService service, IOwnerService oservice, IWorkerService wservice, IEducationalService eservice, IWorkHistoryService whservice)
        {
            _uservice = service;
            _oservice = oservice;
            _wservice = wservice;
            _eservice = eservice;
            _whservice = whservice;
        }

        public ActionResult RegisterForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterForm(UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(userInfo);
            }

            try
            {
                var result = _uservice.Save(userInfo);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm", userInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var jasonUserInfo = JsonConvert.SerializeObject(userInfo);
            //FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(jasonUserInfo, false);

            //CurrentUser.User = userInfo;
            if (userInfo.UserType.Equals("Owner"))
                return RedirectToAction("OwnerForm", "User");

            else
            {
                return RedirectToAction("WorkerForm", "User");

            }


        }

        public ActionResult OwnerForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OwnerForm(OwnerInfo ownerInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(ownerInfo);
            }
            try
            {
                var result = _oservice.Save(ownerInfo);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("OwnerForm", ownerInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("ProjectList", "Owner");

        }

        public ActionResult WorkerForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WorkerForm(WorkerInfo workerInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(workerInfo);
            }
            try
            {
                var result = _wservice.Save(workerInfo);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm", workerInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("ProjectList", "Worker");

        }

        public ActionResult EducationForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EducationForm(EducationalBackground educationalBackground)
        {

            if (!ModelState.IsValid)
            {
                return View(educationalBackground);
            }


            try
            {
                var result = _eservice.Save(educationalBackground);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm", educationalBackground);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("OwnerForm", "User");

        }

        public ActionResult WorkHistoryForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WorkHistoryForm(WorkHistory workHistory)
        {
            if (!ModelState.IsValid)
            {
                return View(workHistory);
            }
            try
            {
                var result = _whservice.Save(workHistory);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm", workHistory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("OwnerForm", "User");

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}