﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ATP2.FMS.ViewModel;
using ATP2.FMS.Web.Framework;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;
using FMS.FrameWork;
using Ionic.Zip;
using Microsoft.Ajax.Utilities;

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
        private ISelectedWorkerService _selectedService;
        private IProjectSkillService _projectSkillService;
        private IRatingWorkerService _ratingWorkerService;

        public ProjectController(IPostAProjectService postservice, IProjectSectionService sectionservice, IProjectSkillService proskillservice, IskillService skillservice, IUserInfoService userservice, IResponseToAJobService responseservice, ISelectedWorkerService selectedWorkerService, IComentSectionService comentSectionService, ISavedFileService savedFileService, ISelectedWorkerService selectedService, IProjectSkillService projectSkillService, IRatingWorkerService ratingWorkerService)
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
            _selectedService = selectedService;
            _projectSkillService = projectSkillService;
            _ratingWorkerService = ratingWorkerService;
        }

        public ActionResult CreateProject()
        {
            var VM = new PostProjectModel();
            VM.SkillName = _skillservice.GetAll().Data.Select(v => v.SkillName).ToList();
            return View(VM);
        }

        [HttpPost]
        public ActionResult CreateProject(PostProjectModel PostProjectModel, string Selectskills)
        {
            try
            {
                string[] tokens = Selectskills.Split(',');
                
                PostProjectModel p=new PostProjectModel();

                var result = _postservice.Save(p.Insert(PostProjectModel));

                var last = _postservice.GetLastId();

                foreach (var x in PostProjectModel.SectionName)
                {
                    var projectsection = new ProjectSection();

                    projectsection.SectionName = x;
                    projectsection.PostId =last.Data.PostId ;
                    var result1 = _sectionservice.Save(projectsection);
                }

                //foreach (var skillid in PostProjectModel.SkillName)
                //{
                //    var projectskill = new ProjectSkills();
                //    projectskill.SkillName = skillid;
                //    projectskill.PostId = last.Data.PostId;
                //    var result2 = _proskillservice.Save(projectskill);
                //}


                PostProjectModel.PostId = last.Data.PostId;
                foreach (var str in tokens)
                {
                    var objToSave = new ProjectSkills();
                    objToSave.SkillName = str;
                    objToSave.PostId = PostProjectModel.PostId;
                    _projectSkillService.Save(objToSave);
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
            return RedirectToAction("ProjectDetails", "Owner", new { id = PostProjectModel.PostId });
        }

        //public ActionResult CreateProjectEdit(int id)
        //{
        //    PostEditModel post = new PostEditModel();

        //    var result = _postservice.GetByID(id);

        //    post.PostAProject = result.Data;

        //    var result2 = _proskillservice.GetAll().Data.Where(d => d.PostId == id).ToList();
        //    var result3 = _sectionservice.GetAll().Data.Where(d => d.PostId == id).ToList();
        //    post.ProjectSkills = result2;
        //    post.ProjectSections = result3;
        //    ViewBag.Categories = new MultiSelectList(result2, "ProjectSkillId", "SkillName");

        //    return View(post);
        //}
        public ActionResult CreateProjectEdit(int id)
        {
            var post = new PostProjectModel();

            var result = _postservice.GetByID(id).Data;

            var skills = _projectSkillService.GetAll().Data.Where(m => m.PostId == id).ToList();
            var AllSkills = _skillservice.GetAll().Data.ToList();
            post = post.creation(result, skills, AllSkills);

           

            return View(post);
        }

        [HttpPost]
        public ActionResult CreateProjectEdit(PostProjectModel PostProjectModel, string Selectskills)
        {
            var post = _postservice.GetByID(PostProjectModel.PostId);
            post.Data.Description = PostProjectModel.Description;
            post.Data.EndTime = PostProjectModel.EndTime;
            post.Data.StartTime = PostProjectModel.StartTime;
            post.Data.Members = PostProjectModel.Members;
            post.Data.Price = PostProjectModel.Price;
            post.Data.ProjectName = PostProjectModel.ProjectName;
            _postservice.Save(post.Data);
            var skills = _projectSkillService.GetAll().Data.Where(d => d.PostId == PostProjectModel.PostId).Select(m=>m.SkillName).ToList();
            string[] tokens = Selectskills.Split(',');
            String[] array = skills.ToArray();
            
            foreach (var t in tokens)
            {
                if (!array.Contains(t))
                {
                    var ongToSave = new ProjectSkills();
                    ongToSave.PostId = PostProjectModel.PostId;
                    ongToSave.SkillName = t;
                    _projectSkillService.Save(ongToSave);
                }
                
            }

            return RedirectToAction("ProjectDetails", "Owner", new {id = PostProjectModel.PostId});
        }
     
        public ActionResult RequestedMember(int id)
        {
            RequestedMemberModel requested = new RequestedMemberModel();
            //postid
            var result = _responseservice.GetAll(id+"");

            var result2 = _postservice.GetByID(id);
            var select = _selectedWorkerService.GetAll().Data.Where(d => d.PostId == id).ToList();


            requested.ProjectName = result2.Data.ProjectName;
            requested.Description = result2.Data.Description;
            requested.PostId = result2.Data.PostId;
            var m = result.Data.Where(p => p.Flag == 0).ToList();
            if (select.Count < result2.Data.Members)
            {
                foreach (var user in m)
                {
                    var result3 = _userservice.GetByID(user.WUserId);
                    requested.UserInfo.Add(result3.Data);


                }
            }
           
            List<ProfileWorker> totList = new List<ProfileWorker>();
            foreach (var v in requested.UserInfo)
            {
                var proWorkVM = new ProfileWorker();
                var obj = proWorkVM.creation(v, new WorkerInfo(),
                    _ratingWorkerService.GetAll().Data.Where(d => d.UserId == v.UserId).ToList(),
                    new List<PostAProject>());
                totList.Add(obj);
            }

            totList = totList.OrderByDescending(d => d.tot).ToList();
            var uInfo = new List<UserInfo>();
            foreach (var v in totList)
            {
                uInfo.Add(_userservice.GetByID(v.UserId).Data);
            }

            requested.UserInfo = uInfo;
            requested.totalAvg = totList;
            return View(requested);
        }

        [HttpPost]
        public ActionResult RequestedMember(SelectedWorker selected)
        {


            try
            {

                var result = _selectedService.Save(selected);
                //var result2 = _responseservice.Delete(selected.PostId,selected.UserId);
                var response = new ResponseToaJob();
                response.WUserId = selected.UserId;
                response.PostId = selected.PostId;
                var r = _responseservice.Update(response);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RequestedMember");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Profile", "Owner");
        }

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

      

        [HttpPost]
        public ActionResult SaveFile(HttpPostedFileBase file, string PostId, string ProjectSectionId)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(Server.MapPath("~/Files"), filename);
                    file.SaveAs(filepath);
                    var objToSave = new SavedFile();
                    objToSave.FileLink = filename;
                    objToSave.PostId = Int32.Parse(PostId);
                    objToSave.ProjectSectionId = Int32.Parse(ProjectSectionId);
                    objToSave.UserId = HttpUtil.CurrentUser.UserId;
                    var result = _savedFileService.Save(objToSave);
                    if (result.HasError)
                    {
                        return Content("Something Went wrong");
                    }
                }
                return RedirectToAction("WorkProgressOwner",new { id=PostId});
            }
            catch (Exception e)
            {
                return Content("Something went wrong");
            }
        }

        public ActionResult ZipDownload(int id)
        {
            var files = _savedFileService.GetAll().Data.Where(d => d.PostId == id).ToList();

            foreach (var v in files)
            {
                v.FileLink = "~/Files/" + v.FileLink;
            }
            SaveFiles(files);
            return null;
        }

        public void SaveFiles(List<SavedFile> files)
        {
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=myZipFile.zip");
            using (ZipFile zip = new ZipFile())
            {
                foreach (var v in files)
                {
                    zip.AddFile(Server.MapPath(v.FileLink), String.Empty);
                }
                zip.Save(Response.OutputStream);
            }
        }
        
    }
}