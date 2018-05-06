using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ATP2.FMS.ViewModel;
using ATP2.FMS.Web.Framework;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;

namespace ATP2.FMS.Controllers
{
    public class WorkerController : Controller
    {
       private IPostAProjectService _postservice;
        private IskillService _skillservice;
        private IProjectSkillService _proskillservice;
        private IResponseToAJobService _responseservice;
        private IUserInfoService _userservice;
        private IWorkerService _workerService;
        private ISelectedWorkerService _selectedWorkerService;
        private IRatingWorkerService _ratingWorkerService;
        private IWorkerSkillService _workerSkillService;
        


        public WorkerController(IPostAProjectService postservice, IskillService skillservice, IProjectSkillService proskillservice, IResponseToAJobService responseservice, IUserInfoService userservice, IWorkerService workerService, ISelectedWorkerService selectedWorkerService, IRatingWorkerService ratingWorkerService, IWorkerSkillService workerSkillService)
        {
            _postservice = postservice;
            _skillservice = skillservice;
            _proskillservice = proskillservice;
            _responseservice = responseservice;
            _userservice = userservice;
            _workerService = workerService;
            _selectedWorkerService = selectedWorkerService;
            _ratingWorkerService = ratingWorkerService;
            _workerSkillService = workerSkillService;
        }

        public ActionResult ProjectList()
        {
            var result = _postservice.GetAll();
            //var a = result.Where(d => d.Flag == 0);

            var result2 = _skillservice.GetAll();
            ProjectListModel projectListModel = new ProjectListModel();
            projectListModel.PostAProjects = result.Data.ToList();
            projectListModel.Skills = result2.Data;

            return View(projectListModel);
        }

        [HttpPost]
        public ActionResult ProjectList(ProjectSkills skill)
        {
            ProjectListModel projectListModel = new ProjectListModel();

            var result = _proskillservice.GetAll(skill.SkillName+"");
            foreach (var projectSkillse in result.Data)
            {
                var result2 = _postservice.GetByID(projectSkillse.PostId);
                projectListModel.PostAProjects.Add(result2.Data);

            }
            var result3 = _skillservice.GetAll();
            projectListModel.Skills = result3.Data;
            return View(projectListModel);
        }

        public ActionResult ProjectDetails(int id)
        {
            //postid
            var result = _postservice.GetByID(id);
            PostProjectModel postProjectModel = new PostProjectModel();

            postProjectModel.ProjectName = result.Data.ProjectName;
            postProjectModel.Description = result.Data.Description;
            postProjectModel.Price = result.Data.Price;
            postProjectModel.StartTime = result.Data.StartTime;
            postProjectModel.EndTime = result.Data.EndTime;
            postProjectModel.WUserId = result.Data.WUserId;
            postProjectModel.PostId = result.Data.PostId;

            var result2 = _proskillservice.GetAll(result.Data.PostId + "");
            foreach (var skillid in result2.Data)
            {
                postProjectModel.SkillName.Add(skillid.SkillName);

            }

            var result4 = _userservice.GetByID(result.Data.WUserId);
            postProjectModel.UFirstName = result4.Data.FristName;
            postProjectModel.ULastName = result4.Data.LastName;

            var a = _responseservice.GetByID(HttpUtil.CurrentUser.UserId,id);
            if (a != null)
            {
                postProjectModel.Flag = 1;
            }
            else
            {
                postProjectModel.Flag = 0;
            }

            return View(postProjectModel);
        }

        [HttpPost]
        public ActionResult ProjectDetails(PostProjectModel PostProjectModel)
        {




            try
            {

                ResponseToaJob responseto = new ResponseToaJob();
                responseto.PostId = PostProjectModel.PostId;
                //responseto.WUserId = CurrentUser.User.UserId;
                responseto.WUserId =HttpUtil.CurrentUser.UserId;
                var result = _responseservice.Save(responseto);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("ProjectDetails", PostProjectModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("ProjectList", "Worker");
        }
       
        public ActionResult Profile()
        {
            var user = _userservice.GetByID(HttpUtil.CurrentUser.UserId);
            var workerInfo = _workerService.GetByID(HttpUtil.CurrentUser.UserId);
            var Selected = _selectedWorkerService.GetAll().Data.Where(d => d.UserId == HttpUtil.CurrentUser.UserId).ToList();
            var projects = new List<PostAProject>();
            foreach (var v in Selected)
            {
                projects.Add(_postservice.GetByID(v.PostId).Data);
            }
            List<RatingWorker> ratings = _ratingWorkerService.GetAll().Data.Where(d => d.UserId == HttpUtil.CurrentUser.UserId).ToList();
            var profileVM = new ProfileWorker();
            profileVM = profileVM.creation(user.Data, workerInfo.Data, ratings, projects);
            var wSkills = _workerSkillService.GetAll().Data.Where(d => d.UserId == HttpUtil.CurrentUser.UserId).ToList();
            var skillsWorker = new List<Skill>();
            foreach (var v in wSkills)
            {
                skillsWorker.Add(_skillservice.GetAll().Data.FirstOrDefault(d=>d.SkillId == v.SkillId));
            }
         
            
            profileVM.Skills = skillsWorker;

            return View(profileVM);
        }

        public ActionResult Edit()
        {
            var user = _userservice.GetByID(HttpUtil.CurrentUser.UserId);
            var workerInfo = _workerService.GetByID(HttpUtil.CurrentUser.UserId);
            var profileVM = new ProfileWorker();
            profileVM = profileVM.creation(user.Data, workerInfo.Data, new List<RatingWorker>(), new List<PostAProject>());
            return View(profileVM);
        }
       
        [HttpPost]
        public ActionResult Edit(ProfileWorker profile)
        {
            var user = _userservice.GetByID(HttpUtil.CurrentUser.UserId);
            var obj = profile.EditUserInfo(profile, user.Data);
            var u = _userservice.Save(obj);
            var ownerInfo = _workerService.GetByID(HttpUtil.CurrentUser.UserId);
            var obj1 = profile.EditWorkerInfo(profile, ownerInfo.Data);
            var owner = _workerService.Save(obj1);
            if (u.HasError || owner.HasError)
            {
                return View();
            }
            else
            {
                return Redirect("Profile");
            }
        }

    }
}