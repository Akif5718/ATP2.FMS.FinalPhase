using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
  public  class EducationalBackground
    {
        [Key]
        public int UserId{ get; set; }
        [ForeignKey("UserId")]
        public UserInfo UserInfo;
      
        public string School{ get; set; }
      
        public string Collage{ get; set; }
       
        public string UniversityPost { get; set; }
     
        public string UniversityUnder { get; set; }
       
        public string Others { get; set; }






    }


}
