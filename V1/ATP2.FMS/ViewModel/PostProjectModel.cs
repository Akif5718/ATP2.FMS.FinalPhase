using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ATP2.FMS.Web.Framework;
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
        public int Flag { get; set; }
        public string Selectskills { get; set; }

        [Required]
        public List<string> SectionName { get; set; }

        [Required]
        public List<string> SkillName = new List<string>();

        public PostAProject Insert(PostProjectModel postProjectModel)
        {
            var postAProject = new PostAProject();
            postAProject.WUserId = HttpUtil.CurrentUser.UserId;
            postAProject.ProjectName = postProjectModel.ProjectName;
            postAProject.Description = postProjectModel.Description;
            postAProject.Price = postProjectModel.Price;
            postAProject.StartTime = postProjectModel.StartTime;
            postAProject.EndTime = postProjectModel.EndTime;
            postAProject.Members = postProjectModel.Members;

            return postAProject;
        }


        public PostProjectModel creation(PostAProject result, List<ProjectSkills> skills , List<Skill> Allskills)
        {
            var postModel = new PostProjectModel();
            postModel.PostId = result.PostId;
            postModel.Description = result.Description;
            postModel.EndTime = result.EndTime;
            postModel.StartTime = result.StartTime;
            postModel.Members = result.Members;
            postModel.Price = result.Price;
            postModel.ProjectName = result.ProjectName;
            var skillStr = skills.Select(m => m.SkillName).ToList();
            string str = "";
            int c = 0;
            foreach (var v in skillStr)
            {
                if(c!=0)
                    str = str + "," + v;
                else
                {
                    str = v;
                    c++;
                }
            }
            List<string> Skills = new List<string>();
            foreach (var v in Allskills)
            {
                if(!str.Contains(v.SkillName))
                    Skills.Add(v.SkillName);
            }
            postModel.SkillName = Skills;
            postModel.Selectskills = str;
            return postModel;
        }
    }
}