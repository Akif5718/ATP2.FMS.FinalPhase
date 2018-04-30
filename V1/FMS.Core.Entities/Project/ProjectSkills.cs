using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Entities;

namespace FMS.Core.Entities
{
   public class ProjectSkills
    {
       [Key]
       public int PostID { get; set; }
      
       [ForeignKey("PostId")]
       public PostAProject PostAProject;

       public int SkillID { get; set; }

       [ForeignKey("SkillID")]
       public Skill Skill;
    }
}
