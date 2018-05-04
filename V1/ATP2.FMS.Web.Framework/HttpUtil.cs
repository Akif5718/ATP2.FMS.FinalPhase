using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FMS.Core.Entities;
using Newtonsoft.Json;

namespace ATP2.FMS.Web.Framework
{
    public static class HttpUtil
    {
        public static UserInfo CurrentUser
        {
            get
            {
                try
                {
                    var user = JsonConvert.DeserializeObject<UserInfo>(HttpContext.Current.User.Identity.Name);
                    return user;
                }
                catch (Exception e)
                {
                    
                    return null;
                }
            }
        }
    }
}
