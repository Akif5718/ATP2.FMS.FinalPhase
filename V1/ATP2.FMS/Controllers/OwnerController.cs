using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATP2.FMS.ViewModel;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;

namespace ATP2.FMS.Controllers
{
    public class OwnerController : Controller
    {
       
        private IPostAProjectService _postservice;
        private IskillService _skillservice;
        private IProjectSkillService _proskillservice;
        private IResponseToAJobService _responseservice;
        private IUserInfoService _userservice;


        public OwnerController(IPostAProjectService postservice, IskillService skillservice, IProjectSkillService proskillservice, IResponseToAJobService responseservice, IUserInfoService userservice)
        {
            _postservice = postservice;
            _skillservice = skillservice;
            _proskillservice = proskillservice;
            _responseservice = responseservice;
            _userservice = userservice;


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

            var result = _proskillservice.GetAll(skill.SkillId+"");
            foreach (var projectSkillse in result.Data)
            {
                var result2 = _postservice.GetByID(projectSkillse.PostId);
                projectListModel.PostAProjects.Add(result2.Data);

            }
            var result3 = _skillservice.GetAll();
            projectListModel.Skills = result3.Data;
            return View(projectListModel);
        }

        public ActionResult ProjectDetails()
        {
            //postid
            var result = _postservice.GetByID(1);
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
                var result3 = _skillservice.GetByID(skillid.SkillId);

                postProjectModel.SkillName.Add(result3.Data.SkillName);

            }

            var result4 = _userservice.GetByID(result.Data.WUserId);
            postProjectModel.UFirstName = result4.Data.FristName;
            postProjectModel.ULastName = result4.Data.LastName;


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
                responseto.WUserId = 9;
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
            return RedirectToAction("CreateProject", "Project");
        }
    }
}