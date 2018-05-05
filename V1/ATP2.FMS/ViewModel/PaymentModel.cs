using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using FMS.Core.Entities;
using FMS.Infrastructure.Migrations;

namespace ATP2.FMS.ViewModel
{
    public class PaymentModel
    {
        public PostAProject PostAProject;
       
        public SelectedWorker Selectedworker ;

        public double Balance { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public List<UserInfo> UserInfos =new List<UserInfo>();
    }
}