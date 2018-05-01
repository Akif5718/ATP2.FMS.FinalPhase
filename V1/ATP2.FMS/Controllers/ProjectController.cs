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
    public class ProjectController : Controller
    {
        private IPostAProjectService _postservice;
        private IProjectSectionService _sectionservice;
        private IProjectSkillService _skillservice;

        public ProjectController(IPostAProjectService postservice, IProjectSectionService sectionservice, IProjectSkillService skillservice)
        {
            _postservice = postservice;
            _sectionservice = sectionservice;
            _skillservice = skillservice;
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
                    projectskill.SkillID = skillid;
                    var result2 = _skillservice.Save(projectskill);
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
            return RedirectToAction("CreateProject", "Project");
        }
    }
}