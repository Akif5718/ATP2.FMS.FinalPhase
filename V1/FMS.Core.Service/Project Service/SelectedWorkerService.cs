using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;
using FMS.Infrastructure;
using Framework;


namespace FMS.Core.Service
{
   public class SelectedWorkerService:ISelectedWorkerService
    {
       FMSDbContext _context;

       public SelectedWorkerService(FMSDbContext context)
        {
            _context = context;
        }

       public Result<SelectedWorker> Save(SelectedWorker userinfo)
       {
           var result = new Result<SelectedWorker>();
           try
           {
               var objtosave = _context.selectedWorkers.FirstOrDefault(u => u.PostId == userinfo.PostId);
               if (objtosave == null)
               {
                   objtosave = new SelectedWorker();
                   _context.selectedWorkers.Add(objtosave);
               }
               objtosave.UserId = userinfo.UserId;
               objtosave.Price = userinfo.Price;
               objtosave.SubmissionDate = userinfo.SubmissionDate;



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

       public bool IsValid(SelectedWorker obj, Result<SelectedWorker> result)
       {
           if (!ValidationHelper.IsStringValid(obj.UserId.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid UserId";
               return false;
           }


           if (!ValidationHelper.IsStringValid(obj.Price.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid Price";
               return false;
           }
           if (!ValidationHelper.IsStringValid(obj.SubmissionDate.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid SubmissionDate";
               return false;
           }

           return true;
       }

       public Result<List<SelectedWorker>> GetAll(string key = "")
       {
           var result = new Result<List<SelectedWorker>>() { Data = new List<SelectedWorker>() };

           try
           {
               IQueryable<SelectedWorker> query = _context.selectedWorkers;

               if (ValidationHelper.IsIntValid(key))
               {
                   query = query.Where(q => q.PostId == Int32.Parse(key));
               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.UserId.Equals(Int32.Parse(key)));

               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.Price.Equals(Int32.Parse(key)));

               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.SubmissionDate.ToString().Contains(key));

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

       public Result<SelectedWorker> GetByID(int id)
       {
           var result = new Result<SelectedWorker>();

           try
           {
               var obj = _context.selectedWorkers.FirstOrDefault(c => c.PostId == id);
               if (obj == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid PostId";
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
               var objtodelete = _context.selectedWorkers.FirstOrDefault(c => c.PostId == id);
               if (objtodelete == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid PostId";
                   return result;


               }

               _context.selectedWorkers.Remove(objtodelete);
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
