using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
   public class SkillCategory
    { 
     [Key]
     public int CategoryId { get; set; }
    
       [Required]
     public string CategoryName { get; set; }
   

    
    }
}
