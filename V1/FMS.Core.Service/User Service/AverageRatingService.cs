using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Entities;
using FMS.Core.Entities.User;
using FMS.Core.Service.Interfaces.IUser_Service;
using FMS.FrameWork;
using FMS.Infrastructure;

namespace FMS.Core.Service.User_Service
{
   public class AverageRatingService:IAverageRatingService
    {
       FMSDbContext _context;

       public AverageRatingService(FMSDbContext context)
        {
            _context = context;
        }

       public Result<AverageRating> Save(AverageRating userinfo)
       {
           var result = new Result<AverageRating>();
           try
           {
               var objtosave = _context.AverageRatings.FirstOrDefault(u => u.UserId == userinfo.UserId);
               if (objtosave == null)
               {
                   objtosave = new AverageRating();
                   _context.AverageRatings.Add(objtosave);
               }
               objtosave.UserId = userinfo.UserId;
               objtosave.UserType = userinfo.UserType;
               objtosave.Average = userinfo.Average;
             


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

       public bool IsValid(AverageRating obj, Result<AverageRating> result)
       {

           return true;
       }

       public Result<List<AverageRating>> GetAll(string key = "")
       {
           var result = new Result<List<AverageRating>>() { Data = new List<AverageRating>() };

           try
           {
               IQueryable<AverageRating> query = _context.AverageRatings;

               if (ValidationHelper.IsIntValid(key))
               {
                   var m = Int32.Parse(key);
                   query = query.Where(q => q.UserId == m);
               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.UserType.Contains(key));

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

       public Result<AverageRating> GetByID(int id)
       {
           var result = new Result<AverageRating>();

           try
           {
               var obj = _context.AverageRatings.FirstOrDefault(c => c.UserId == id);
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
               var objtodelete = _context.AverageRatings.FirstOrDefault(c => c.UserId == id);
               if (objtodelete == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid UserID";
                   return result;


               }

               _context.AverageRatings.Remove(objtodelete);
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
