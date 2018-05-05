using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
   public class ResponseToaJob
    {

        [Key]
        public int ResponseId { get; set; }

        [Required]
        public int PostId { get; set; }
       
        [ForeignKey("PostId")]
        public PostAProject PostAProject;

        [Required]
        public int WUserId { get; set; }
       
        [ForeignKey("UserId")]
        public UserInfo UserInfo;


        [Required]
        public float FixedPrice { get; set; }

       public int Flag { get; set; }

       



    }
}
