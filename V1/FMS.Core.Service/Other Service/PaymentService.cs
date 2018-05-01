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
   public class PaymentService:IPaymentService
    {
       FMSDbContext _context;

       public PaymentService(FMSDbContext context)
        {
            _context = context;
        }
        public Result<Payment> Save(Payment Payment)
        {
            var result = new Result<Payment>();
            try
            {
                var objtosave = _context.payments.FirstOrDefault(u => u.Id == Payment.Id);
                if (objtosave == null)
                {
                    objtosave = new Payment();
                    _context.payments.Add(objtosave);
                }

                objtosave.OUserId = Payment.OUserId;
                objtosave.WUserId = Payment.WUserId;
                objtosave.Balance = Payment.Balance;



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

        public bool IsValid(Payment obj, Result<Payment> result)
        {

            if (!ValidationHelper.IsStringValid(obj.OUserId.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid OUserId";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.WUserId.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid WUserId";
                return false;
            }
            if (!ValidationHelper.IsStringValid(obj.Balance.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid Balance";
                return false;
            }


            return true;
        }

    
       public Result<List<Payment>> GetAll(string key = "")
        {
            var result = new Result<List<Payment>>() { Data = new List<Payment>() };

            try
            {
                IQueryable<Payment> query = _context.payments;

                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.Id == Int32.Parse(key));
                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.OUserId == Int32.Parse(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.WUserId == Int32.Parse(key));

                }

                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.Balance.Equals(Int32.Parse(key)));

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

        public Result<Payment> GetByID(int id)
        {
            var result = new Result<Payment>();

            try
            {
                var obj = _context.payments.FirstOrDefault(c => c.Id == id);
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
                var objtodelete = _context.payments.FirstOrDefault(c => c.Id == id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid UserID";
                    return result;


                }

                _context.payments.Remove(objtodelete);
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
