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


        public OwnerController(IPostAProjectService postservice, IskillService skillservice)
        {
            _postservice = postservice;
            _skillservice = skillservice;

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

        //[HttpPost]
        //public ActionResult ProjectList(ProjectSkills skill)
        //{
        //    ProjectListModel projectListModel = new ProjectListModel();

        //    var result = projectSkillDao.GetAllskill(skill.SkillID);
        //    foreach (var projectSkillse in result)
        //    {
        //        var result2 = postProjectDao.GetByID(projectSkillse.PostID);
        //        projectListModel.PostAProjects.Add(result2.Data);

        //    }
        //    var result3 = skillsDao.GetAll();
        //    projectListModel.Skills = result3;
        //    return View(projectListModel);
        //}
    }
}