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
    public class RatingOwnerService:IRatingOwnerService
    {
      FMSDbContext _context;

      public RatingOwnerService(FMSDbContext context)
        {
            _context = context;
        }

      public Result<RatingOwner> Save(RatingOwner userinfo)
      {
          var result = new Result<RatingOwner>();
          try
          {
              var objtosave = _context.ratingOwners.FirstOrDefault(u => u.UserId == userinfo.UserId);
              if (objtosave == null)
              {
                  objtosave = new RatingOwner();
                  _context.ratingOwners.Add(objtosave);
              }
              objtosave.CommunicationSkill = userinfo.CommunicationSkill;
              objtosave.Reliability = userinfo.Reliability;
              objtosave.OnWord = userinfo.OnWord;
              objtosave.Behaviour = userinfo.Behaviour;


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

      public bool IsValid(RatingOwner obj, Result<RatingOwner> result)
      {
          if (!ValidationHelper.IsStringValid(obj.CommunicationSkill.ToString()))
          {
              result.HasError = true;
              result.Message = "Invalid CommunicationSkill";
              return false;
          }


          if (!ValidationHelper.IsStringValid(obj.Reliability.ToString()))
          {
              result.HasError = true;
              result.Message = "Invalid Reliability";
              return false;
          }
          if (!ValidationHelper.IsStringValid(obj.OnWord.ToString()))
          {
              result.HasError = true;
              result.Message = "Invalid OnWord";
              return false;
          }
          if (!ValidationHelper.IsStringValid(obj.Behaviour.ToString()))
          {
              result.HasError = true;
              result.Message = "Invalid Behaviour";
              return false;
          }

          return true;
      }

      public Result<List<RatingOwner>> GetAll(string key = "")
      {
          var result = new Result<List<RatingOwner>>() { Data = new List<RatingOwner>() };

          try
          {
              IQueryable<RatingOwner> query = _context.ratingOwners;

              if (ValidationHelper.IsIntValid(key))
              {
                  query = query.Where(q => q.UserId == Int32.Parse(key));
              }

              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.CommunicationSkill.Equals(Int32.Parse(key)));

              }

              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.Reliability.Equals(Int32.Parse(key)));

              }

              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.OnWord.Equals(Int32.Parse(key)));

              }


              if (ValidationHelper.IsStringValid(key))
              {
                  query = query.Where(q => q.Behaviour.Equals(Int32.Parse(key)));

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

      public Result<RatingOwner> GetByID(int id)
      {
          var result = new Result<RatingOwner>();

          try
          {
              var obj = _context.ratingOwners.FirstOrDefault(c => c.UserId == id);
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
              var objtodelete = _context.ratingOwners.FirstOrDefault(c => c.UserId == id);
              if (objtodelete == null)
              {
                  result.HasError = true;
                  result.Message = "Invalid UserID";
                  return result;


              }

              _context.ratingOwners.Remove(objtodelete);
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
