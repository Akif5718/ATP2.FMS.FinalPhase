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
   public class ComentSectionService:IComentSectionService
    {
       FMSDbContext _context;

       public ComentSectionService(FMSDbContext context)
        {
            _context = context;
        }

    
       public Result<COMMENTSEC> Save(COMMENTSEC Entity)
       {
           var result = new Result<COMMENTSEC>();
           try
           {
               var objtosave = _context.commentsecs.FirstOrDefault(u => u.CommunicationId == Entity.CommunicationId);
               if (objtosave == null)
               {
                   objtosave = new COMMENTSEC();
                   _context.commentsecs.Add(objtosave);
               }

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
               var objtodelete = _context.commentsecs.FirstOrDefault(c => c.CommunicationId == Id);
               if (objtodelete == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid PostId";
                   return result;


               }

               _context.commentsecs.Remove(objtodelete);
               _context.SaveChanges();

           }
           catch (Exception e)
           {
               result.HasError = true;
               result.Message = e.Message;


           }
           return result;
       }

       public Result<List<COMMENTSEC>> GetAll(string key = "")
       {
           var result = new Result<List<COMMENTSEC>>() { Data = new List<COMMENTSEC>() };

           try
           {
               IQueryable<COMMENTSEC> query = _context.commentsecs;

               if (ValidationHelper.IsIntValid(key))
               {
                   query = query.Where(q => q.CommunicationId == Int32.Parse(key));
               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.UserId.Equals(Int32.Parse(key)));

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

       public Result<COMMENTSEC> GetByID(int Id)
       {
           var result = new Result<COMMENTSEC>();

           try
           {
               var obj = _context.commentsecs.FirstOrDefault(c => c.CommunicationId == Id);
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

       public bool IsValid(COMMENTSEC obj, Result<COMMENTSEC> result)
       {
           if (!ValidationHelper.IsStringValid(obj.UserId.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid UserId";
               return false;
           }


           

           return true;
       }

     
    }
}
