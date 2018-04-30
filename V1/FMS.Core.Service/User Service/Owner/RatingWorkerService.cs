using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;
using FMS.Infrastructure;
using Framework;

namespace FMS.Core.Service
{
   public class RatingWorkerService:IRatingWorkerService
    {
      FMSDbContext _context;

      public RatingWorkerService(FMSDbContext context)
        {
            _context = context;
        }

      public Result<RatingWorker> Save(RatingWorker userinfo)
      {
          var result = new Result<RatingWorker>();
          try
          {
              var objtosave = _context.ratingWorkers.FirstOrDefault(u => u.UserId == userinfo.UserId);
              if (objtosave == null)
              {
                  objtosave = new RatingWorker();
                  _context.ratingWorkers.Add(objtosave);
              }
              objtosave.CommunicationSkill = userinfo.CommunicationSkill;
              objtosave.OnTime = userinfo.OnTime;
              objtosave.OnBudget = userinfo.OnBudget;
              objtosave.Behaviour = userinfo.Behaviour;
              objtosave.Completeness = userinfo.Completeness;


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

      public bool IsValid(RatingWorker obj, Result<RatingWorker> result)
      {
          if (!ValidationHelper.IsStringValid(obj.CommunicationSkill.ToString()))
          {
              result.HasError = true;
              result.Message = "Invalid CommunicationSkill";
              return false;
          }


          if (!ValidationHelper.IsStringValid(obj.OnTime.ToString()))
          {
              result.HasError = true;
              result.Message = "Invalid OnTime";
              return false;
          }
          if (!ValidationHelper.IsStringValid(obj.OnBudget.ToString()))
          {
              result.HasError = true;
              result.Message = "Invalid OnBudget";
              return false;
          }
          if (!ValidationHelper.IsStringValid(obj.Behaviour.ToString()))
          {
              result.HasError = true;
              result.Message = "Invalid Behaviour";
              return false;
          }
          if (!ValidationHelper.IsStringValid(obj.Completeness.ToString()))
          {
              result.HasError = true;
              result.Message = "Invalid Completeness";
              return false;
          }

          return true;
      }

       public RatingWorker AverageRating(int UserId)
       {
           var result = new Result<List<RatingWorker>>() ;
           result = GetAll(UserId+"");
           var CommunicationSkill = 0;
           var Ontime = 0;
           var OnBudget = 0;
           var Behaviour = 0;
           var Completeness = 0;
  
           foreach (var p in result.Data)
           {
               CommunicationSkill = CommunicationSkill + p.CommunicationSkill;
               Ontime = Ontime + p.OnTime;
               OnBudget = OnBudget + p.OnBudget;
               Behaviour = Behaviour + p.Behaviour;
               Completeness = Completeness + p.Completeness;
           }
           CommunicationSkill = CommunicationSkill/result.Data.Count;
           Ontime = Ontime / result.Data.Count;
           OnBudget = OnBudget / result.Data.Count;
           Behaviour = Behaviour / result.Data.Count;
           Completeness = Completeness / result.Data.Count;
           var obj = new RatingWorker();
           obj.CommunicationSkill = CommunicationSkill;
           obj.OnTime = Ontime;
           obj.OnBudget = OnBudget;
           obj.Behaviour = Behaviour;
           obj.Completeness = Completeness;
           obj.UserId = UserId;
           return obj;
       }


       public Result<List<RatingWorker>> GetAll(string key = "")
      {
          var result = new Result<List<RatingWorker>>() { Data = new List<RatingWorker>() };

          try
          {
              IQueryable<RatingWorker> query = _context.ratingWorkers;

              if (ValidationHelper.IsIntValid(key))
              {
                  query = query.Where(q => q.UserId == Int32.Parse(key));
              }

              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.CommunicationSkill == Int32.Parse(key));

              }

              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.OnTime == Int32.Parse(key));

              }

              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.OnBudget == Int32.Parse(key));

              }


              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.Behaviour == Int32.Parse(key));

              }

              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.Completeness == Int32.Parse(key));

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

      public Result<RatingWorker> GetByID(int id)
      {
          var result = new Result<RatingWorker>();

          try
          {
              var obj = _context.ratingWorkers.FirstOrDefault(c => c.UserId == id);
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
              var objtodelete = _context.ratingWorkers.FirstOrDefault(c => c.UserId == id);
              if (objtodelete == null)
              {
                  result.HasError = true;
                  result.Message = "Invalid UserID";
                  return result;


              }

              _context.ratingWorkers.Remove(objtodelete);
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
