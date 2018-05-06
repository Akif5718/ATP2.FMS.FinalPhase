using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;
using FMS.FrameWork;
using FMS.Infrastructure;

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
               var objtosave = _context.responseToaJobs.FirstOrDefault(u => u.WUserId == userinfo.WUserId && u.PostId==userinfo.PostId);
               if (objtosave == null)
               {
                   objtosave = new ResponseToaJob();
                   _context.responseToaJobs.Add(objtosave);
               }
               objtosave.WUserId = userinfo.WUserId;
               objtosave.PostId = userinfo.PostId;
               objtosave.FixedPrice = userinfo.FixedPrice;
               objtosave.Flag = 0;
              


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

       public Result<ResponseToaJob> Update(ResponseToaJob userinfo)
       {
           var result = new Result<ResponseToaJob>();
           try
           {
               var objtosave = _context.responseToaJobs.FirstOrDefault(u => u.WUserId == userinfo.WUserId && u.PostId == userinfo.PostId);
        
            
               objtosave.Flag =1;



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

       public Result<bool> Delete(int Id)
       {
           var result = new Result<bool>();

           try
           {
               var objtodelete = _context.responseToaJobs.FirstOrDefault(c => c.WUserId == Id);
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

       public bool IsValid(ResponseToaJob obj, Result<ResponseToaJob> result)
       {
           if (!ValidationHelper.IsStringValid(obj.WUserId.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid WUserId";
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
                   var a = Int32.Parse(key);
                   query = query.Where(q => q.PostId == a);
               }

               //if (ValidationHelper.IsStringValid(key))
               //{
               //    var a = Int32.Parse(key);
               //    query = query.Where(q => q.WUserId == a);

               //}

               //if (ValidationHelper.IsStringValid(key))
               //{
               //    var a = float.Parse(key);
               //    query = query.Where(q => q.FixedPrice == a);

               //}

              



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

       public Result<ResponseToaJob> GetByID(int id,int id2)
       {
           var result = new Result<ResponseToaJob>();

           try
           {
               var obj = _context.responseToaJobs.FirstOrDefault(c => c.PostId == id2 && c.WUserId==id);
               if (obj == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return null;


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

       public Result<bool> Delete(int id,int id2)
       {
           var result = new Result<bool>();

           try
           {
               var objtodelete = _context.responseToaJobs.FirstOrDefault(c => c.PostId == id && c.WUserId==id2);
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
