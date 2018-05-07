using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Entities;
using FMS.FrameWork;
using FMS_Entities;

namespace FMS.Core.Service.Interfaces
{
    public interface ISelectedWorkerService:IService<SelectedWorker>
    {
        Result<SelectedWorker> Update(SelectedWorker userinfo);

        Result<bool> DeleteByuser(int id);
        Result<SelectedWorker> UpdateApprove(SelectedWorker userinfo, int app);
    }
}
