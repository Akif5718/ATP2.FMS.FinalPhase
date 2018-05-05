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
    public interface IResponseToAJobService:IService<ResponseToaJob>
    {
        Result<bool> Delete(int id, int id2);
        Result<ResponseToaJob> GetByID(int id, int id2);
        Result<ResponseToaJob> Update(ResponseToaJob userinfo);
    }
}
