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
  public  class OwnerService:IOwnerService
    {
      FMSDbContext _context;

      public OwnerService(FMSDbContext context)
        {
            _context = context;
        }
        public Result<OwnerInfo> Save(OwnerInfo userinfo)
        {
            var result = new Result<OwnerInfo>();
            try
            {
                var objtosave = _context.ownerInfos.FirstOrDefault(u => u.UserId == userinfo.UserId);
                if (objtosave == null)
                {
                    objtosave = new OwnerInfo();
                    _context.ownerInfos.Add(objtosave);
                }
                objtosave.CompanyName = userinfo.CompanyName;
                objtosave.CompanyAddress = userinfo.CompanyAddress;
                objtosave.CompanyCode = userinfo.CompanyCode;

                objtosave.Position = userinfo.Position;


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

        public bool IsValid(OwnerInfo obj, Result<OwnerInfo> result)
        {
            if (!ValidationHelper.IsStringValid(obj.CompanyName))
            {
                result.HasError = true;
                result.Message = "Invalid CompanyName";
                return false;
            }


            if (!ValidationHelper.IsStringValid(obj.CompanyAddress))
            {
                result.HasError = true;
                result.Message = "Invalid CompanyAddress";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.CompanyCode))
            {
                result.HasError = true;
                result.Message = "Invalid CompanyCode";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Position))
            {
                result.HasError = true;
                result.Message = "Invalid Position";
                return false;
            }

            return true;
        }

        public Result<List<OwnerInfo>> GetAll(string key = "")
        {
            var result = new Result<List<OwnerInfo>>() { Data = new List<OwnerInfo>() };

            try
            {
                IQueryable<OwnerInfo> query = _context.ownerInfos;

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
                    query = query.Where(q => q.CompanyAddress.Contains(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.CompanyCode.Contains(key));

                }


                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Position.Contains(key));

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

        public Result<OwnerInfo> GetByID(int id)
        {
            var result = new Result<OwnerInfo>();

            try
            {
                var obj = _context.ownerInfos.FirstOrDefault(c => c.UserId == id);
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
                var objtodelete = _context.ownerInfos.FirstOrDefault(c => c.UserId == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                _context.ownerInfos.Remove(objtodelete);
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
