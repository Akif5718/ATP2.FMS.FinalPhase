﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Core.Entities
{
   public class WorkReport
    {
       [Key]
       [Required]
       public int PostId { get; set; }

       [ForeignKey("PostId")]
       public PostAProject  PostAProject;
        
       [Required]
       public int UserId { get; set; }

       [ForeignKey("UserId")]
       public UserInfo UserInfo;

        [Required]
       public int Percentage { get; set; }
    }
}
