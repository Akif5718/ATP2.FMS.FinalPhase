using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ATP2.FMS.ViewModel;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;
using FMS.FrameWork;
using Ionic.Zip;
using Newtonsoft.Json;

namespace ATP2.FMS.Controllers
{
    public class OwnerController : Controller
    {
       
        private IPostAProjectService _postservice;
        private IskillService _skillservice;
        private IProjectSkillService _proskillservice;
        private IResponseToAJobService _responseservice;
        private IUserInfoService _userservice;
        private IOwnerService _ownerService;
        private IRatingOwnerService _ratingOwnerService;


        public OwnerController(IPostAProjectService postservice, IskillService skillservice, IProjectSkillService proskillservice, IResponseToAJobService responseservice, IUserInfoService userservice, IOwnerService ownerService, IRatingOwnerService ratingOwnerService)
        {
            _postservice = postservice;
            _skillservice = skillservice;
            _proskillservice = proskillservice;
            _responseservice = responseservice;
            _userservice = userservice;
            _ownerService = ownerService;
            _ratingOwnerService = ratingOwnerService;

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
        public ActionResult Profile()
        {
            var user = _userservice.GetByID(1);
            var ownerInfo = _ownerService.GetByID(1);
            var posedtProjects = _postservice.GetAll().Data.Where(d=> d.WUserId==1).ToList();
            List<RatingOwner> ratings = _ratingOwnerService.GetAll().Data.Where(d => d.UserId == 1).ToList();
            var profileVM = new Profile();
            profileVM = profileVM.creation(user.Data, ownerInfo.Data, ratings, posedtProjects);
            //string s1 = " ~/Files/1SignIn.PNG";
            //string s2 = " ~/Files/2SignUp.PNG";
            //string s3 = " ~/Files/3AfterSignUp.PNG";
            //var file1 = new SavedFile();
            //file1.FileLink = s1;
            //var file2 = new SavedFile();
            //file2.FileLink = s2;
            //var file3 = new SavedFile();
            //file3.FileLink = s3;
            //var files = new List<SavedFile>();
            //files.Add(file1);
            //files.Add(file2);
            //files.Add(file3);
            //SaveFiles(files);
            
            return View(profileVM);
        }

        public ActionResult Edit()
        {
            var user = _userservice.GetByID(1);
            var ownerInfo = _ownerService.GetByID(1);
            var profileVM = new Profile();
            profileVM = profileVM.creation(user.Data,ownerInfo.Data,new List<RatingOwner>(), new List<PostAProject>());
            return View(profileVM);
        }
        [HttpPost]
        public ActionResult Edit(Profile profile)
        {
            var user = _userservice.GetByID(1);
            var obj = profile.EditUserInfo(profile, user.Data);
            var u = _userservice.Save(obj);
            var ownerInfo = _ownerService.GetByID(1);
            var obj1 = profile.EditOwnerInfo(profile, ownerInfo.Data);
            var owner = _ownerService.Save(obj1);
            if (u.HasError || owner.HasError)
            {
                return View();
            }
            else
            {
                return Redirect("Profile");
            }
        }

        public void SaveFiles(List<SavedFile> files)
        {
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition","attachment; filename=myZipFile.zip");
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