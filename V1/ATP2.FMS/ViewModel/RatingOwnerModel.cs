using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATP2.FMS.ViewModel
{
    public class RatingOwnerModel
    {
        public int CommunicationSkill { get; set; }
        public int Onword { get; set; }
        public int Reliability { get; set; }
        public int Behaviour { get; set; }
        public int OwnerId { get; set; }
        public int PostId { get; set; }
    }
}