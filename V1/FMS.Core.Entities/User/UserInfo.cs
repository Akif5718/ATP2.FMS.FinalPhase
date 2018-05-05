using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork;

namespace FMS.Core.Entities
{
    public class UserInfo
    {   
        [Key]
        public int  UserId { get; set; }
      
        [Required]
       
        public string FristName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
       
        public string Password { get; set; }
        
        [Required]
        public DateTime DateofBrith { get; set; }
        
        [Required]
        public DateTime JoinDate { get; set; }
       
        public string ProPic { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }

        [Required]
        public string UserType { get; set; }
      
        public double Balance { get; set; }

        

    }
}
