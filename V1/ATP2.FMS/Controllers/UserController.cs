using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ATP2.FMS.Web.Framework;
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
        private ICategoryService _categoryService;
        private IskillService _skillService;


        public UserController(IUserInfoService service, IOwnerService oservice, IWorkerService wservice, IEducationalService eservice, IWorkHistoryService whservice, ICategoryService categoryService, IskillService skillService)
        {
            _uservice = service;
            _oservice = oservice;
            _wservice = wservice;
            _eservice = eservice;
            _whservice = whservice;
            _categoryService = categoryService;
            _skillService = skillService;
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
            var obj = _uservice.GetByEmail(userInfo.Email);

            var jasonUserInfo = JsonConvert.SerializeObject(obj.Data);
            FormsAuthentication.SetAuthCookie(jasonUserInfo, false);
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
                ownerInfo.UserId=HttpUtil.CurrentUser.UserId;
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
                workerInfo.UserId = HttpUtil.CurrentUser.UserId;
                var result = _wservice.Save(workerInfo);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("ProjectList", "Worker");

        }

        public ActionResult Category()
        {
            var result = _categoryService.GetAll();
            var categories = new List<SkillCategory>();
            categories = result.Data;
            return View(categories);
        }

        public ActionResult Skills(int id)
        {
            var result = _skillService.GetAll().Data.Where(m => m.CategoryId == id).ToList();

            var skills=new List<Skill>();
            skills = result;
            return View(skills);

        }
        [HttpPost]
        public ActionResult Skills(List<string> skillNames)
        {
            //var objtosave=new WorkerSkill();
            //objtosave.SkillId = model.SkillId;
            //objtosave.UserId=model.
            return null;

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
                educationalBackground.UserId = HttpUtil.CurrentUser.UserId;

                var result = _eservice.Save(educationalBackground);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm");
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
                workHistory.UserId = HttpUtil.CurrentUser.UserId;

                var result = _whservice.Save(workHistory);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm");
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