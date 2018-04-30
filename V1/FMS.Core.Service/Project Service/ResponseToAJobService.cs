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
   public class ResponseToAJobService:IResponseToAJobService
    {
       FMSDbContext _context;

       public ResponseToAJobService(FMSDbContext context)
        {
            _context = context;
        }

       public Result<ResponseToaJob> Save(ResponseToaJob userinfo)
       {
           var result = new Result<ResponseToaJob>();
           try
           {
               var objtosave = _context.responseToaJobs.FirstOrDefault(u => u.PostId == userinfo.PostId);
               if (objtosave == null)
               {
                   objtosave = new ResponseToaJob();
                   _context.responseToaJobs.Add(objtosave);
               }
               objtosave.WUserId = userinfo.WUserId;
               objtosave.FixedPrice = userinfo.FixedPrice;
               objtosave.SubmissionTime = userinfo.SubmissionTime;


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

       public bool IsValid(ResponseToaJob obj, Result<ResponseToaJob> result)
       {
           if (!ValidationHelper.IsStringValid(obj.WUserId.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid WUserId";
               return false;
           }


           if (!ValidationHelper.IsStringValid(obj.FixedPrice.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid FixedPrice";
               return false;
           }

           if (ValidationHelper.IsStringValid(obj.SubmissionTime))
           {
               result.HasError = true;
               result.Message = "Invalid SubmissionTime";
               return false;


           }

           return true;

       }

       public Result<List<ResponseToaJob>> GetAll(string key = "")
       {
           var result = new Result<List<ResponseToaJob>>() { Data = new List<ResponseToaJob>() };

           try
           {
               IQueryable<ResponseToaJob> query = _context.responseToaJobs;

               if (ValidationHelper.IsIntValid(key))
               {
                   query = query.Where(q => q.PostId == Int32.Parse(key));
               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.WUserId.Equals(Int32.Parse(key)));

               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.FixedPrice.Equals(Int32.Parse(key)));

               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.SubmissionTime.Contains(key));

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

       public Result<ResponseToaJob> GetByID(int id)
       {
           var result = new Result<ResponseToaJob>();

           try
           {
               var obj = _context.responseToaJobs.FirstOrDefault(c => c.PostId == id);
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
               var objtodelete = _context.responseToaJobs.FirstOrDefault(c => c.PostId == id);
               if (objtodelete == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return result;


               }

               _context.responseToaJobs.Remove(objtodelete);
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
