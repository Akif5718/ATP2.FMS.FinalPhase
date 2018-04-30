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
   public class CategoryService:ICategoryService
    {
       FMSDbContext _context;

       public CategoryService(FMSDbContext context)
        {
            _context = context;
        }
        public Result<SkillCategory> Save(SkillCategory userinfo)
        {
            var result = new Result<SkillCategory>();
            try
            {

                _context.skillCategories.FirstOrDefault(u => u.CategoryId == userinfo.CategoryId);
                var objtosave = _context.skillCategories.FirstOrDefault(u => u.CategoryId == userinfo.CategoryId);
                if (objtosave == null)
                {
                    objtosave = new SkillCategory();
                    _context.skillCategories.Add(objtosave);
                }

                objtosave.CategoryId = userinfo.CategoryId;
                objtosave.CategoryName = userinfo.CategoryName;




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

        public bool IsValid(SkillCategory obj, Result<SkillCategory> result)
        {
            if (!ValidationHelper.IsStringValid(obj.CategoryId.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid CategoryId";
                return false;
            }

            if (!ValidationHelper.IsStringValid(obj.CategoryName))
            {
                result.HasError = true;
                result.Message = "Invalid CategoryName";
                return false;
            }


            return true;
        }

        public Result<List<SkillCategory>> GetAll(string key = "")
        {
            var result = new Result<List<SkillCategory>>() { Data = new List<SkillCategory>() };

            try
            {
                IQueryable<SkillCategory> query = _context.skillCategories;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.CategoryId == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.CategoryName.Contains(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.CategoryId == Int32.Parse(key));

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

        public Result<SkillCategory> GetByID(int id)
        {
            var result = new Result<SkillCategory>();

            try
            {
                var obj = _context.skillCategories.FirstOrDefault(c => c.CategoryId == id);
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
                var objtodelete = _context.skillCategories.FirstOrDefault(c => c.CategoryId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                _context.skillCategories.Remove(objtodelete);
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
