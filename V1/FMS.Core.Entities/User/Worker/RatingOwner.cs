using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
  public  class RatingOwner
    {
        [Key]
      public int RatingOwnerId { get; set; }

        [Required]
        public int UserId { get; set; }
       
      [ForeignKey("UserId")]
        public UserInfo UserInfo;

      [Required]
      public int CommunicationSkill { get; set; }

      [Required]
      public int Reliability { get; set; }

      [Required]
      public int OnWord { get; set; }

      [Required]
      public int Behaviour { get; set; }

     

    }
}
