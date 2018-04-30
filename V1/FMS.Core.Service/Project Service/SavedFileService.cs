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
  public  class SavedFileService:ISavedFileService
    {
       FMSDbContext _context;

       public SavedFileService(FMSDbContext context)
        {
            _context = context;
        }

      public Result<SavedFile> Save(SavedFile Entity)
      {
          var result = new Result<SavedFile>();
          try
          {
              var objtosave = _context.savedFiles.FirstOrDefault(u => u.SavedFileId == Entity.SavedFileId);
              if (objtosave == null)
              {
                  objtosave = new SavedFile();
                  _context.savedFiles.Add(objtosave);
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
              var objtodelete = _context.savedFiles.FirstOrDefault(c => c.SavedFileId == Id);
              if (objtodelete == null)
              {
                  result.HasError = true;
                  result.Message = "Invalid PostId";
                  return result;


              }

              _context.savedFiles.Remove(objtodelete);
              _context.SaveChanges();

          }
          catch (Exception e)
          {
              result.HasError = true;
              result.Message = e.Message;


          }
          return result;
      }

      public Result<List<SavedFile>> GetAll(string key = "")
      {
          var result = new Result<List<SavedFile>>() { Data = new List<SavedFile>() };

          try
          {
              IQueryable<SavedFile> query = _context.savedFiles;

              if (ValidationHelper.IsIntValid(key))
              {
                  query = query.Where(q => q.SavedFileId == Int32.Parse(key));
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

      public Result<SavedFile> GetByID(int Id)
      {
          var result = new Result<SavedFile>();

          try
          {
              var obj = _context.savedFiles.FirstOrDefault(c => c.SavedFileId == Id);
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

      public bool IsValid(SavedFile obj, Result<SavedFile> result)
      {
          if (!ValidationHelper.IsStringValid(obj.UserId.ToString()))
          {
              result.HasError = true;
              result.Message = "Invalid UserId";
              return false;
          }




          return true;
      }

      public Result<List<SavedFile>> DownloadZip(string key)
      {
          var result = new Result<List<SavedFile>>() { Data = new List<SavedFile>() };
          try
          {
              IQueryable<SavedFile> query = _context.savedFiles;

              if (ValidationHelper.IsIntValid(key))
              {
                  query = query.Where(q => q.PostId == Int32.Parse(key));
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
    }
}
