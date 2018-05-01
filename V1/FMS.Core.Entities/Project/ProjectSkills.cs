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
       public int ProjectSkillId { get; set; }


       [Required]
       public int PostId { get; set; }
      
       [ForeignKey("PostId")]
       public PostAProject PostAProject;

       public int SkillId { get; set; }

       [ForeignKey("SkillId")]
       public Skill Skill;
    }
}
