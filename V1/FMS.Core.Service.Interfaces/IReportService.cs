using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Entities;
using Framework;

namespace FMS.Core.Service.Interfaces
{
   public interface IReportService
   {
       Result<List<WorkReport>>GetAll(int PostId);
   }
}
