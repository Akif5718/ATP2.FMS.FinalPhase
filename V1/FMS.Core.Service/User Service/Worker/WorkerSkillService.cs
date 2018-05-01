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
  public  class WorkerSkillService:IWorkerSkillService
    {
      FMSDbContext _context;

      public WorkerSkillService(FMSDbContext context)
        {
            _context = context;
        }
        public Result<WorkerSkill> Save(WorkerSkill Entity)
        {
            var result = new Result<WorkerSkill>();
            try
            {

                _context.workerSkills.FirstOrDefault(u => u.SkillId == Entity.SkillId);
                var objtosave = _context.workerSkills.FirstOrDefault(u => u.SkillId == Entity.SkillId);
                if (objtosave == null)
                {
                    objtosave = new WorkerSkill();
                    _context.workerSkills.Add(objtosave);
                }
                objtosave.SkillId = Entity.SkillId;




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
                var objtodelete = _context.workerSkills.FirstOrDefault(c => c.SkillId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                _context.workerSkills.Remove(objtodelete);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;


            }
            return result;
        }

        public Result<List<WorkerSkill>> GetAll(string key = "")
        {
             var result = new Result<List<WorkerSkill>>() { Data = new List<WorkerSkill>() };

            try
            {
                IQueryable<WorkerSkill> query = _context.workerSkills;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.SkillId == Int32.Parse(key));
                }

               

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.SkillId == Int32.Parse(key));

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

        public Result<WorkerSkill> GetByID(int Id)
        {
            var result = new Result<WorkerSkill>();

            try
            {
                var obj = _context.workerSkills.FirstOrDefault(c => c.SkillId == Id);
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

      public bool IsValid(WorkerSkill obj, Result<WorkerSkill> result)
      {
          throw new NotImplementedException();
      }
    }
}
