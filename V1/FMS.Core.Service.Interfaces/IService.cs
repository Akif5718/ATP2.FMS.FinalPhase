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
   public interface IService<T>
   {
        Result<T> Save(T Entity);
        Result<bool> Delete(int Id);
        Result<List<T>> GetAll(String key="");
        Result<T> GetByID(int Id);
        bool IsValid(T obj, Result<T> result);
   }
}
