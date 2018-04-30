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
    public class ProjectSectionService:IProjectSectionService
    {
       FMSDbContext _context;

       public ProjectSectionService(FMSDbContext context)
        {
            _context = context;
        }

       public Result<ProjectSection> Save(ProjectSection userinfo)
       {
           var result = new Result<ProjectSection>();
           try
           {
               var objtosave = _context.projectSections.FirstOrDefault(u => u.ProjectSectionId == userinfo.ProjectSectionId);
               if (objtosave == null)
               {
                   objtosave = new ProjectSection();
                   _context.projectSections.Add(objtosave);
               }
               objtosave.PostId = userinfo.PostId;
               objtosave.SectionName = userinfo.SectionName;
               objtosave.Percentage = userinfo.Percentage;
               objtosave.Price = userinfo.Price;


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

       public bool IsValid(ProjectSection obj, Result<ProjectSection> result)
       {
           if (!ValidationHelper.IsStringValid(obj.PostId.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid PostId";
               return false;
           }


           if (!ValidationHelper.IsStringValid(obj.SectionName))
           {
               result.HasError = true;
               result.Message = "Invalid SectionName";
               return false;
           }
           if (!ValidationHelper.IsStringValid(obj.Percentage.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid Percentage";
               return false;
           }
           if (!ValidationHelper.IsStringValid(obj.Price.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid Price";
               return false;
           }

           return true;
       }

       public Result<List<ProjectSection>> GetAll(string key = "")
       {
           var result = new Result<List<ProjectSection>>() { Data = new List<ProjectSection>() };

           try
           {
               IQueryable<ProjectSection> query = _context.projectSections;

               if (ValidationHelper.IsIntValid(key))
               {
                   query = query.Where(q => q.ProjectSectionId == Int32.Parse(key));
               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.PostId.Equals(Int32.Parse(key)));

               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.SectionName.Contains(key));

               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.Percentage.Equals(Int32.Parse(key)));

               }


               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.Price.Equals(Int32.Parse(key)));

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

       public Result<ProjectSection> GetByID(int id)
       {
           var result = new Result<ProjectSection>();

           try
           {
               var obj = _context.projectSections.FirstOrDefault(c => c.ProjectSectionId == id);
               if (obj == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid ProjectSectionId";
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
               var objtodelete = _context.projectSections.FirstOrDefault(c => c.ProjectSectionId == id);
               if (objtodelete == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid ProjectSectionId";
                   return result;


               }

               _context.projectSections.Remove(objtodelete);
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
