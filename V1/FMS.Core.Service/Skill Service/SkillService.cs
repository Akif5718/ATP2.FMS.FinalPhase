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
  public  class SkillService:IskillService
    {
      FMSDbContext _context;

      public SkillService(FMSDbContext context)
        {
            _context = context;
        }

        public Result<Skill> Save(Skill userinfo)
        {
            var result = new Result<Skill>();
            try
            {

                _context.skills.FirstOrDefault(u => u.SkillId == userinfo.SkillId);
                var objtosave = _context.skills.FirstOrDefault(u => u.SkillId == userinfo.SkillId);
                if (objtosave == null)
                {
                    objtosave = new Skill();
                    _context.skills.Add(objtosave);
                }
                objtosave.SkillName = userinfo.SkillName;
                objtosave.CategoryId = userinfo.CategoryId;




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

        public bool IsValid(Skill obj, Result<Skill> result)
        {
            if (!ValidationHelper.IsStringValid(obj.SkillName))
            {
                result.HasError = true;
                result.Message = "Invalid SkillName";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.CategoryId.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid CategoryId";
                return false;
            }


            return true;
        }

        public Result<List<Skill>> GetAll(string key = "")
        {
            var result = new Result<List<Skill>>() { Data = new List<Skill>() };

            try
            {
                IQueryable<Skill> query = _context.skills;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.SkillId == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.SkillName.Contains(key));

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

        public Result<Skill> GetByID(int id)
        {
            var result = new Result<Skill>();

            try
            {
                var obj = _context.skills.FirstOrDefault(c => c.SkillId == id);
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
                var objtodelete = _context.skills.FirstOrDefault(c => c.SkillId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                _context.skills.Remove(objtodelete);
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
