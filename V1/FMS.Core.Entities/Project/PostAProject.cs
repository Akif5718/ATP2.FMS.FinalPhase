using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
    public class PostAProject
    {
     [Key]
     public int PostId { get; set; }
   
    [Required]
    public int WUserId { get; set; }
   
    [ForeignKey("WUserId")]
    public UserInfo UserInfo;
    
    [Required]
    public string ProjectName { get; set; }
   
    [Required]
    public double Price  { get; set; }
    
    [Required]
    public DateTime StartTime { get; set; }
  
     [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public string Description { get; set; }
    
    [Required]
    public int Members { get; set; }




    }

    
    
}
