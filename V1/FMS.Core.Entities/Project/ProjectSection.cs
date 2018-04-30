using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
   public class ProjectSection
    {

       [Key]
       public int ProjectSectionId { get; set; }

       [Required]
       public int PostId { get; set; }

       [ForeignKey("PostId")]
       public PostAProject PostAProject;

       [Required]
       public string SectionName { get; set; }

     
       public int Percentage { get; set; }

       public double Price { get; set; }
    }
}
