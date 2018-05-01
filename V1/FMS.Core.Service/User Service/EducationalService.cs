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
  public class EducationalService:IEducationalService
    {
      FMSDbContext _context;

      public EducationalService(FMSDbContext context)
        {
            _context = context;
        }

        public Result<EducationalBackground> Save(EducationalBackground userinfo)
        {
            var result = new Result<EducationalBackground>();
            try
            {
                var objtosave = _context.educationalBackgrounds.FirstOrDefault(u => u.UserId == userinfo.UserId);
                if (objtosave == null)
                {
                    objtosave = new EducationalBackground();
                    _context.educationalBackgrounds.Add(objtosave);
                }
                objtosave.School = userinfo.School;
                objtosave.Collage = userinfo.Collage;
                objtosave.UniversityPost = userinfo.UniversityPost;
                objtosave.UniversityUnder = userinfo.UniversityUnder;
                objtosave.Others = userinfo.Others;


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

        public bool IsValid(EducationalBackground obj, Result<EducationalBackground> result)
        {
           


            return true;
        }

        public Result<List<EducationalBackground>> GetAll(string key = "")
        {
            var result = new Result<List<EducationalBackground>>() { Data = new List<EducationalBackground>() };

            try
            {
                IQueryable<EducationalBackground> query = _context.educationalBackgrounds;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.UserId == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.School.Contains(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Collage.Contains(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.UniversityPost.Contains(key));

                }


                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Others.Contains(key));

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



      public Result<EducationalBackground> GetByID(int id)
        {
            var result = new Result<EducationalBackground>();

            try
            {
                var obj = _context.educationalBackgrounds.FirstOrDefault(c => c.UserId == id);
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
                var objtodelete = _context.educationalBackgrounds.FirstOrDefault(c => c.UserId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                _context.educationalBackgrounds.Remove(objtodelete);
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
