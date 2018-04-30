using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS_Entities
{
[NotMapped]
    public class Rewards
    {
        [Key]
        public string UserID { get; set; }
        [Required]
        public string ContestId { get; set; }
        [Required]
        public float Mark { get; set; }
        [Required]
        public string Certificate { get; set; }



    }
}
