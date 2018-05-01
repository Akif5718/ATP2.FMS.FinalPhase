using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FMS.Core.Entities;

namespace ATP2.FMS.ViewModel
{
    public class PostProjectModel
    {
        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string UFirstName { get; set; }

        [Required]
        public string ULastName { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Members { get; set; }

        [Required]
        public List<int> SkillId { get; set; }

        public int WUserId { get; set; }

        public int PostId { get; set; }

        [Required]
        public List<string> SectionName { get; set; }

        [Required]
        public List<string> SkillName = new List<string>();

        public PostAProject Insert(PostProjectModel postProjectModel)
        {
            var postAProject = new PostAProject();
            //postAProject.WUserId = CurrentUser.User.UserId;
            postAProject.WUserId = 1;
            postAProject.ProjectName = postProjectModel.ProjectName;
            postAProject.Description = postProjectModel.Description;
            postAProject.Price = postProjectModel.Price;
            postAProject.StartTime = postProjectModel.StartTime;
            postAProject.EndTime = postProjectModel.EndTime;
            postAProject.Members = postProjectModel.Members;

            return postAProject;
        }
    }
}