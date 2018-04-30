using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
    public class Skill
    {
         
     [Key]
     public int SkillId { get; set; }
     
        [Required]
     public string SkillName{ get; set; }

     public int CategoryId { get; set; }
             
        [ForeignKey("CategoryId")]
     public SkillCategory SkillCategory;







    }
}
