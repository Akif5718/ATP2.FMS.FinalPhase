using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities.User
{
   public class AverageRating
    {
       [Key]
       public int  AverageRatingId { get; set; }

       [Required]
       public int UserId { get; set; }

       [ForeignKey("UserId")]
       public UserInfo UserInfo;

       [Required]
       public double Average { get; set; }

       [Required]
       public string UserType { get; set; }
    }
}
