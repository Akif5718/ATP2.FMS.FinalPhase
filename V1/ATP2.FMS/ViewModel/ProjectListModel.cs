using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMS.Core.Entities;

namespace ATP2.FMS.ViewModel
{
    public class ProjectListModel
    {
        public List<PostAProject> PostAProjects = new List<PostAProject>();

        public List<Skill> Skills = new List<Skill>();

        public int SkillId { get; set; }

        public string SkillName { get; set; }
    }
}