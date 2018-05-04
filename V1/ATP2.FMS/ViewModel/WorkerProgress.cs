using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMS.Core.Entities;
using FMS.FrameWork;

namespace ATP2.FMS.ViewModel
{
    public class WorkerProgress
    {
        public int PostID { get; set; }
        public List<UserInfo> workers { get; set; }
        public string Description { get; set; }
        public string PostName { get; set; }
        public List<ProjectSection> ProjectSections { get; set; }
        public List<COMMENTSEC> Comments { get; set; }
        public List<SavedFile> SavedFiles { get; set; }

        public WorkerProgress creation(Result<PostAProject> post, List<UserInfo> userInfos, List<ProjectSection> projectSections, List<COMMENTSEC> comments, List<SavedFile> files)
        {
            var objToSend = new WorkerProgress();
            objToSend.PostID = post.Data.PostId;
            objToSend.Description = post.Data.Description;
            objToSend.PostName = post.Data.ProjectName;
            objToSend.workers = userInfos;
            objToSend.ProjectSections = projectSections;
            objToSend.Comments = comments;
            objToSend.SavedFiles = files;
            return objToSend;

        }
    }
}