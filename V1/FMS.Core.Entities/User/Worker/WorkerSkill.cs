using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
   public class WorkerSkill
    {
       [Key]
       [Required]
       public int UserId { get; set; }

       [ForeignKey("UserId")]
       public UserInfo UserInfo;

       [Required]
       public int SkillId { get; set; }

       [ForeignKey("SkillId")]
       public Skill Skill;
    }
}
