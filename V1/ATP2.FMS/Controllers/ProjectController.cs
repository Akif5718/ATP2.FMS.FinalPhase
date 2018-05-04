using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATP2.FMS.ViewModel;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;
using FMS.FrameWork;

namespace ATP2.FMS.Controllers
{
    public class ProjectController : Controller
    {
        private IPostAProjectService _postservice;
        private IProjectSectionService _sectionservice;
        private IProjectSkillService _proskillservice;
        private IskillService _skillservice;
        private IUserInfoService _userservice;
        private IResponseToAJobService _responseservice;
        private ISelectedWorkerService _selectedWorkerService;
        private IComentSectionService _comentSectionService;
        private ISavedFileService _savedFileService;

        public ProjectController(IPostAProjectService postservice, IProjectSectionService sectionservice, IProjectSkillService proskillservice, IskillService skillservice, IUserInfoService userservice, IResponseToAJobService responseservice, ISelectedWorkerService selectedWorkerService, IComentSectionService comentSectionService, ISavedFileService savedFileService)
        {
            _postservice = postservice;
            _sectionservice = sectionservice;
            _proskillservice = proskillservice;
            _skillservice = skillservice;
            _userservice = userservice;
            _responseservice = responseservice;
            _selectedWorkerService = selectedWorkerService;
            _comentSectionService = comentSectionService;
            _savedFileService = savedFileService;
        }

        public ActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProject(PostProjectModel PostProjectModel)
        {
            try
            {
                PostProjectModel p=new PostProjectModel();

                var result = _postservice.Save(p.Insert(PostProjectModel));

                foreach (var x in PostProjectModel.SectionName)
                {
                    var projectsection = new ProjectSection();
                    projectsection.SectionName = x;
                    var result1 = _sectionservice.Save(projectsection);
                }

                foreach (var skillid in PostProjectModel.SkillId)
                {
                    var projectskill = new ProjectSkills();
                    projectskill.SkillId = skillid;
                    var result2 = _proskillservice.Save(projectskill);
                }





                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("CreateProject");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("ProjectDetails", "Project");
        }

       

        public ActionResult RequestedMember()
        {
            RequestedMemberModel requested = new RequestedMemberModel();
            //postid
            var result = _responseservice.GetAll(1+"");

            var result2 = _postservice.GetByID(1);
            requested.ProjectName = result2.Data.ProjectName;
            requested.Description = result2.Data.Description;
            requested.PostId = result2.Data.PostId;
            foreach (var user in result.Data)
            {
                var result3 = _userservice.GetByID(user.WUserId);
                requested.UserInfo.Add(result3.Data);

            }
            return View(requested);
        }

        //[HttpPost]
        //public ActionResult RequestedMember(SelectedWorker selected)
        //{


        //    try
        //    {

        //        var result = selectedWorkerDao.Save(selected);
        //        var result2 = response.Delete1(selected.UserId);
        //        if (result.HasError)
        //        {
        //            ViewBag.Message = result.Message;
        //            return View("RequestedMember");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return RedirectToAction("OwnerProfile", "Owner");
        //}
        public ActionResult WorkProgressOwner(int id)
        {
            var post = _postservice.GetByID(id);
            var selectedWorkers = _selectedWorkerService.GetAll().Data.Where(d => d.PostId == id).ToList();
            List<UserInfo> workers = new List<UserInfo>();
            foreach (var v in selectedWorkers)
            {
                workers.Add(_userservice.GetByID(v.UserId).Data);
            }

            List<int> sectionIDs = new List<int>();
            var projectSections = _sectionservice.GetAll().Data.Where(d => d.PostId == id).ToList();
            foreach (var v in projectSections)
            {
                sectionIDs.Add(v.ProjectSectionId);
            }

            var commentsAll = _comentSectionService.GetAll().Data.ToList();
            var comments = new List<COMMENTSEC>();
            foreach (var v in commentsAll)
            {
                foreach (var x in sectionIDs)
                {
                    if (v.ProjectSectionId == x)
                    {
                        comments.Add(v);
                    }
                }
            }

            var files = _savedFileService.GetAll().Data.Where(d => d.PostId == id).ToList();
            var VM = new WorkerProgress();
            VM = VM.creation(post, workers, projectSections, comments, files);
            return View(VM);

        }
        [HttpPost]
        public ActionResult WorkProgressOwner(object obj)
        {
            return null;
        }
        
    }
}