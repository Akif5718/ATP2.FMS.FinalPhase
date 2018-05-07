using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;
using ATP2.FMS.ViewModel;
using ATP2.FMS.Web.Framework;
using FMS.Core.Entities;
using FMS.Core.Entities.User;
using FMS.Core.Service.Interfaces;
using FMS.Core.Service.Interfaces.IUser_Service;
using FMS.FrameWork;
using FMS.Infrastructure.Migrations;
using Ionic.Zip;
using Microsoft.Ajax.Utilities;
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
        private IAverageRatingService _averageRatingService;
        private IRatingWorkerService _ratingWorkerService;
        private ISelectedWorkerService _selectedWorkerService;
        private IPaymentService _paymentService;
        private IEducationalService _educationService;


        public OwnerController(IPostAProjectService postservice, IskillService skillservice, IProjectSkillService proskillservice, IResponseToAJobService responseservice, IUserInfoService userservice, IOwnerService ownerService, IRatingOwnerService ratingOwnerService, IAverageRatingService averageRatingService, ISelectedWorkerService selectedWorkerService, IRatingWorkerService ratingWorkerService, IPaymentService paymentService, IEducationalService educationService)
        {
            _postservice = postservice;
            _skillservice = skillservice;
            _proskillservice = proskillservice;
            _responseservice = responseservice;
            _userservice = userservice;
            _ownerService = ownerService;
            _ratingOwnerService = ratingOwnerService;
            _averageRatingService = averageRatingService;
            _selectedWorkerService = selectedWorkerService;
            _ratingWorkerService = ratingWorkerService;
            _paymentService = paymentService;
            _educationService = educationService;

        }

        public ActionResult ProjectList()
        {
            var result = _postservice.GetAll();
            var result3 = _postservice.GetAll();
            //var a = result.Where(d => d.Flag == 0);

            var result2 = _skillservice.GetAll();
            ProjectListModel projectListModel = new ProjectListModel();
            if (result != null)
            {
                foreach (var p in result.Data)
                {
                    var select = _selectedWorkerService.GetAll().Data.Where(d => d.PostId == p.PostId).ToList();
                    if (select.Count == p.Members)
                    {
                        result3.Data.Remove(p);
                    }
                }
            }

            result = result3;
            projectListModel.PostAProjects = result.Data.OrderByDescending(m=>m.PostId).ToList();
            projectListModel.Skills = result2.Data;

            return View(projectListModel);
        }

        [HttpPost]
        public ActionResult ProjectList(ProjectSkills skill)
        {
            ProjectListModel projectListModel = new ProjectListModel();

            var result = _proskillservice.GetAll().Data.Where(d=>d.SkillName.Contains(skill.SkillName)).ToList();
            foreach (var projectSkillse in result)
            {
                var result2 = _postservice.GetByID(projectSkillse.PostId);
                var select = _selectedWorkerService.GetAll().Data.Where(d => d.PostId == result2.Data.PostId).ToList();
                if (select.Count != result2.Data.Members)
                {
                    projectListModel.PostAProjects.Add(result2.Data);

                }

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

            var result2 = _proskillservice.GetAll().Data.Where(p=>p.PostId==id).ToList();
            foreach (var skillid in result2)
            {

                postProjectModel.SkillName.Add(skillid.SkillName);

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
                responseto.WUserId = HttpUtil.CurrentUser.UserId;
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

        public ActionResult Profile(int id=0)
        {
            var user = _userservice.GetByID(HttpUtil.CurrentUser.UserId);
            var ownerInfo = _ownerService.GetByID(HttpUtil.CurrentUser.UserId);
            var posedtProjects = _postservice.GetAll().Data.Where(d => d.WUserId == HttpUtil.CurrentUser.UserId).ToList();
            List<RatingOwner> ratings = _ratingOwnerService.GetAll().Data.Where(d => d.UserId == HttpUtil.CurrentUser.UserId).ToList();
            var profileVM = new Profile();
            profileVM = profileVM.creation(user.Data, ownerInfo.Data, ratings, posedtProjects);
            var avg = new AverageRating();
            avg.UserId = user.Data.UserId;
            avg.Average = profileVM.tot;
            avg.UserType = user.Data.UserType;
            _averageRatingService.Save(avg);
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
            if (id!=0)
            {
                return RedirectToAction("OtherViewPro2", "Owner", new { id = HttpUtil.CurrentUser.UserId });

            }
            return View(profileVM);
        }

        public ActionResult Edit()
        {
            var user = _userservice.GetByID(HttpUtil.CurrentUser.UserId);
            var ownerInfo = _ownerService.GetByID(HttpUtil.CurrentUser.UserId);
            var profileVM = new Profile();
            profileVM = profileVM.creation(user.Data,ownerInfo.Data,new List<RatingOwner>(), new List<PostAProject>());
            return View(profileVM);
        }
        [HttpPost]
        public ActionResult Edit(Profile profile)
        {
            var user = _userservice.GetByID(HttpUtil.CurrentUser.UserId);
            var obj = profile.EditUserInfo(profile, user.Data);
            var u = _userservice.Save(obj);
            var ownerInfo = _ownerService.GetByID(HttpUtil.CurrentUser.UserId);
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

       

        public ActionResult Deleteacount(int id)
        {
            var result = _postservice.GetAll().Data.Where(d => d.WUserId == id).ToList();
            foreach (var po in result)
            {
                _selectedWorkerService.Delete(po.PostId);
            }
            _postservice.DeletebyUser(id);
            _ownerService.Delete(id);

                _userservice.Delete(id);
            return RedirectToAction("Index", "Home");

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

        public ActionResult WorkerList(int id)
        {
            WorkerListModel model=new WorkerListModel();
            var result= _selectedWorkerService.GetAll().Data.Where(d => d.PostId == id).ToList();
            var m = result.Where(p => p.Flag == 0).ToList();
            
            foreach (var worker in m)
            {
                var result2 = _userservice.GetByID(worker.UserId);
               model.Name.Add(result2.Data.FristName+" "+result2.Data.LastName);
               model.UserId.Add(result2.Data.UserId);
            }
            model.PostId = id;
           
            return View(model);
        }


        public ActionResult WorkerRating(int id, int id2)
        {
            WorkerListModel model = new WorkerListModel();
            model.UId = id;
            model.PostId = id2;
            return View(model);
        }

        [HttpPost]
        public ActionResult WorkerRating(RatingWorker worker)
        {
            var result = _ratingWorkerService.Save(worker);
            SelectedWorker s=new SelectedWorker();
            s.UserId = worker.UserId;
            s.PostId = worker.PostId;
            _selectedWorkerService.Update(s);
            
            return RedirectToAction("WorkerList", "Owner",new{ id=worker.PostId});
        }

        public ActionResult Payments(int id)
        {
            var selW = _selectedWorkerService.GetAll().Data.Where(p => p.PostId == id).ToList();
            foreach (var v in selW)
            {
                _selectedWorkerService.UpdateApprove(v, 1);
            }
            
            PaymentModel pay=new PaymentModel();
            var result = _postservice.GetByID(id);
            pay.PostAProject = result.Data;
            var b = _userservice.GetByID(result.Data.WUserId);
            pay.Balance = b.Data.Balance;
            pay.PostId = id;
            var result2 = _selectedWorkerService.GetAll().Data.Where(d => d.PostId == id).ToList();
            var list = _selectedWorkerService.GetAll().Data.Where(d => d.PostId == id).ToList();
            var result4 = _paymentService.GetAll().Data.Where(d => d.PostId == id).ToList();
            foreach (var x in result2)
            {
                foreach (var pa in result4)
                {
                    if (x.UserId == pa.WUserId)
                    {
                        list.Remove(x);
                        break;
                    }
                }
            }
            result2 = list;
            //for (int i = 0; i < result2.Count;)
            //{
            //    for (int j = i; j < result4.Count; j++)
            //    {
            //        if (result2[i].UserId == result4[j].WUserId)
            //        {
            //            result2.Remove(result2[i]);
            //            i = 0;
            //            break;
            //        }
            //        else
            //        {
                       
            //        }
                   
            //    }
            //}
            foreach (var worker in result2)
            {
                var result3 = _userservice.GetByID(worker.UserId);
                pay.UserInfos.Add(result3.Data);

            }
            return View(pay);
        }

        [HttpPost]
        public ActionResult Payments(PaymentModel payment)
        {
            _userservice.Deposit(payment.Balance,payment.UserId);
            _userservice.Withdraw(payment.Balance,HttpUtil.CurrentUser.UserId);
            Payment pay=new Payment();
            pay.Balance = payment.Balance;
            pay.OUserId = HttpUtil.CurrentUser.UserId;
            pay.WUserId = payment.UserId;
            pay.PostId = payment.PostId;

            var result = _paymentService.Save(pay);
            return RedirectToAction("Payments", "Owner");

        }
        public ActionResult DepositeForm()
        {
            var user = _userservice.GetByID(HttpUtil.CurrentUser.UserId);
            return View(user.Data);
        }
        [HttpPost]
        public ActionResult DepositeForm(string balance)
        {
            _userservice.Deposit(double.Parse(balance), HttpUtil.CurrentUser.UserId);
            return RedirectToAction("Profile", "Owner");
        }

        public ActionResult OtherViewPro(int id)
        {

            var user = _userservice.GetByID(id);
            var ownerInfo = _ownerService.GetByID(id);
            var posedtProjects = _postservice.GetAll().Data.Where(d => d.WUserId == id).ToList();
            List<RatingOwner> ratings = _ratingOwnerService.GetAll().Data.Where(d => d.UserId == id).ToList();
            var profileVM = new Profile();
            profileVM = profileVM.creation(user.Data, ownerInfo.Data, ratings, posedtProjects);
            var avg = new AverageRating();
            avg.UserId = user.Data.UserId;
            avg.Average = profileVM.tot;
            avg.UserType = user.Data.UserType;
            _averageRatingService.Save(avg);
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

        public ActionResult OtherViewPro2(int id)
        {

            var user = _userservice.GetByID(id);
            var ownerInfo = _ownerService.GetByID(id);
            var posedtProjects = _postservice.GetAll().Data.Where(d => d.WUserId == id).ToList();
            List<RatingOwner> ratings = _ratingOwnerService.GetAll().Data.Where(d => d.UserId == id).ToList();
            var profileVM = new Profile();
            profileVM = profileVM.creation(user.Data, ownerInfo.Data, ratings, posedtProjects);
            var avg = new AverageRating();
            avg.UserId = user.Data.UserId;
            avg.Average = profileVM.tot;
            avg.UserType = user.Data.UserType;
            _averageRatingService.Save(avg);
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

        public ActionResult Propic(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                    Server.MapPath("~/DP"), pic);
                // file is uploaded
                file.SaveAs(path);
                var user = _userservice.GetByID(HttpUtil.CurrentUser.UserId);
                user.Data.ProPic = pic;
                _userservice.Save(user.Data);

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Profile", "Owner");
        }
    }
}