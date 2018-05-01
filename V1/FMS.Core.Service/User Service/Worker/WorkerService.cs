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
   public class WorkerService:IWorkerService
    {
       FMSDbContext _context;

       public WorkerService(FMSDbContext context)
        {
            _context = context;
        }

       public Result<WorkerInfo> Save(WorkerInfo userinfo)
       {
           var result = new Result<WorkerInfo>();
           try
           {
               var objtosave = _context.workerInfos.FirstOrDefault(u => u.UserId == userinfo.UserId);
               if (objtosave == null)
               {
                   objtosave = new WorkerInfo();
                   _context.workerInfos.Add(objtosave);
               }
               objtosave.EarnedMoney = userinfo.EarnedMoney;
               objtosave.RatePerHour = userinfo.RatePerHour;



               if (!IsValid(objtosave, result))
               {
                   return result;
               }
               _context.SaveChanges();
           }
           catch (Exception ex)
           {
               result.HasError = true;
               result.Message = ex.Message;
           }
           return result;
       }

       public bool IsValid(WorkerInfo obj, Result<WorkerInfo> result)
       {
           if (!ValidationHelper.IsStringValid(obj.EarnedMoney.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid EarnedMoney";
               return false;
           }
           if (!ValidationHelper.IsStringValid(obj.RatePerHour))
           {
               result.HasError = true;
               result.Message = "Invalid RatePerHour";
               return false;
           }


           return true;
       }

       public Result<List<WorkerInfo>> GetAll(string key = "")
       {
           var result = new Result<List<WorkerInfo>>() { Data = new List<WorkerInfo>() };

           try
           {
               IQueryable<WorkerInfo> query = _context.workerInfos;

               if (ValidationHelper.IsIntValid(key))
               {
                   query = query.Where(q => q.UserId == Int32.Parse(key));
               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.EarnedMoney.Equals(Int32.Parse(key)));

               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.RatePerHour.Contains(key));

               }


               result.Data = query.ToList();
           }
           catch (Exception e)
           {
               result.HasError = true;
               result.Message = e.Message;


           }
           return result;
       }

       public Result<WorkerInfo> GetByID(int id)
       {
           var result = new Result<WorkerInfo>();

           try
           {
               var obj = _context.workerInfos.FirstOrDefault(c => c.UserId == id);
               if (obj == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return result;


               }
               result.Data = obj;
           }
           catch (Exception e)
           {
               result.HasError = true;
               result.Message = e.Message;


           }
           return result;
       }

       public Result<bool> Delete(int id)
       {
           var result = new Result<bool>();

           try
           {
               var objtodelete = _context.workerInfos.FirstOrDefault(c => c.UserId == id);
               if (objtodelete == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return result;


               }

               _context.workerInfos.Remove(objtodelete);
               _context.SaveChanges();

           }
           catch (Exception e)
           {
               result.HasError = true;
               result.Message = e.Message;


           }
           return result;
       }
    }
}
