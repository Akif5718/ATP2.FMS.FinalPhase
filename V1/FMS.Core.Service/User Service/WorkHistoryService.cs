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
    public class WorkHistoryService:IWorkHistoryService
    {
      FMSDbContext _context;

      public WorkHistoryService(FMSDbContext context)
        {
            _context = context;
        }

      public Result<WorkHistory> Save(WorkHistory userinfo)
      {
          var result = new Result<WorkHistory>();
          try
          {

              _context.workHistories.FirstOrDefault(u => u.UserId == userinfo.UserId);
              var objtosave = _context.workHistories.FirstOrDefault(u => u.UserId == userinfo.UserId);
              if (objtosave == null)
              {
                  objtosave = new WorkHistory();
                  _context.workHistories.Add(objtosave);
              }
              objtosave.CompanyName = userinfo.CompanyName;
              objtosave.Position = userinfo.Position;
              objtosave.Experience = userinfo.Experience;
              objtosave.UserId = userinfo.UserId;



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

      public bool IsValid(WorkHistory obj, Result<WorkHistory> result)
      {
          //if (!ValidationHelper.IsStringValid(obj.CompanyName))
          //{
          //    result.HasError = true;
          //    result.Message = "Invalid CompanyName";
          //    return false;
          //}
          //if (!ValidationHelper.IsStringValid(obj.Position))
          //{
          //    result.HasError = true;
          //    result.Message = "Invalid Position";
          //    return false;
          //}
          //if (!ValidationHelper.IsStringValid(obj.Experience))
          //{
          //    result.HasError = true;
          //    result.Message = "Invalid Experience";
          //    return false;
          //}


          return true;
      }

      public Result<List<WorkHistory>> GetAll(string key = "")
      {
          var result = new Result<List<WorkHistory>>() { Data = new List<WorkHistory>() };

          try
          {
              IQueryable<WorkHistory> query = _context.workHistories;

              if (ValidationHelper.IsIntValid(key))
              {
                  query = query.Where(q => q.UserId == Int32.Parse(key));
              }

              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.CompanyName.Contains(key));

              }

              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.Position.Contains(key));

              }

              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.Experience.Contains(key));

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

      public Result<WorkHistory> GetByID(int id)
      {
          var result = new Result<WorkHistory>();

          try
          {
              var obj = _context.workHistories.FirstOrDefault(c => c.UserId == id);
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
              var objtodelete = _context.workHistories.FirstOrDefault(c => c.UserId == id);
              if (objtodelete == null)
              {
                  result.HasError = true;
                  result.Message = "Invalid UserID";
                  return result;


              }

              _context.workHistories.Remove(objtodelete);
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
