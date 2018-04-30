using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
   public class WorkerInfo
   {
       [Key]
      
       public int UserId { get; set; }
      
       [ForeignKey("UserId")]
       public UserInfo UserInfo;
      
        [Required]
        public float EarnedMoney { get; set; }
        [Required]
        public string RatePerHour { get; set; }



    }


}
