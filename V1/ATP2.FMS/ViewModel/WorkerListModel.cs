using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATP2.FMS.ViewModel
{
    public class WorkerListModel
    {
       public List<string> Name = new List<string>(); 
       public List<int> UserId = new List<int>();

        public int PostId { get; set; }
        public int UId { get; set; }
        public int Flag { get; set; }

    }
}