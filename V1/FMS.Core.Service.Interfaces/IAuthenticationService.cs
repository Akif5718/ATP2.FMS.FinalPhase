using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Entities;
using FMS.FrameWork;

namespace FMS.Core.Service.Interfaces
{
   public interface IAuthenticationService
   {
       Result<UserInfo> Login(string email, string password);
   }
}
