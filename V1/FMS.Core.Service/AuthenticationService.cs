using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;
using FMS.FrameWork;
using FMS.Infrastructure;

namespace FMS.Core.Service
{
    public class AuthenticationService:IAuthenticationService
    {
        FMSDbContext _context;

        public AuthenticationService(FMSDbContext context)
        {
            _context = context;
        }

        public Result<UserInfo> Login(string email, string password)
        {
            var result = new Result<UserInfo>();
            try
            {
                var obj = _context.userInfos.FirstOrDefault(u => u.Email == email && u.Password == password);
                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Email or Pssword";
                    return result;
                }

                result.Data = obj;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
