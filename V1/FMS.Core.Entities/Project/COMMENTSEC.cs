using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
    public class COMMENTSEC
    {
       [Key]
       public int CommunicationId { get; set; }

       [Required]
       public int UserId { get; set; }

       [ForeignKey("UserId")]
       public UserInfo UserInfo;

       [Required]
       public int ProjectSectionId { get; set; }

       [ForeignKey("ProjectSectionId")]
       public ProjectSection ProjectSection;

       [Required]
       public string Commt { get; set; }




    }
}
