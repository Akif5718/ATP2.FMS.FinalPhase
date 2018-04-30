using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
   public class Payment
    {
       [Key]
       public int Id { get; set; }

       [Required]
       public int OUserId { get; set; }
     
      
       [Required]
       public int WUserId { get; set; }
       
       [ForeignKey("WUserId,OUserId")]
       public UserInfo UserInfo;

       public double Balance { get; set; }
    }
}
