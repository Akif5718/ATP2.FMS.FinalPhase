using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
  public  class RatingWorker
    {
        [Key]
       
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public UserInfo UserInfo;

       

        [Required]
        public int CommunicationSkill { get; set; }

        [Required]
        public int OnTime { get; set; }

        [Required]
        public int OnBudget { get; set; }

        [Required]
        public int Behaviour { get; set; }

        [Required]
        public int Completeness { get; set; }
    }
}
