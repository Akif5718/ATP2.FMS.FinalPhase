using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
  public  class OwnerInfo
    {

        [Key]
         public int OwnerInfoId { get; set; }
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserInfo UserInfo;

        [Required]
        public string  CompanyName{ get; set; }

        [Required]
        public string CompanyAddress { get; set; }

        [Required]
        public string CompanyCode { get; set; }

        [Required]
        public string Position { get; set; }



    }
}
