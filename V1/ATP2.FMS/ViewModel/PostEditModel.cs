using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FMS.Core.Entities;

namespace ATP2.FMS.ViewModel
{
    public class PostEditModel
    {
        public PostAProject PostAProject { get; set; }

        public List<ProjectSkills> ProjectSkills =new List<ProjectSkills>();

        public List<ProjectSection> ProjectSections = new List<ProjectSection>();

        public string SkillName { get; set; }

        public SelectList DropDownList { get; set; }

    }
}