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
   public class PostAProjectService:IPostAProjectService
    {
       FMSDbContext _context;

       public PostAProjectService(FMSDbContext context)
        {
            _context = context;
        }
       public Result<PostAProject> Save(PostAProject PostAProject)
       {
           var result = new Result<PostAProject>();
           try
           {
               var objtosave = _context.postAProjects.FirstOrDefault(u => u.PostId == PostAProject.PostId);
               if (objtosave == null)
               {
                   objtosave = new PostAProject();
                   _context.postAProjects.Add(objtosave);
               }
               objtosave.WUserId = PostAProject.WUserId;
               objtosave.ProjectName = PostAProject.ProjectName;
               objtosave.Price = PostAProject.Price;
               objtosave.StartTime = PostAProject.StartTime;
               objtosave.EndTime = PostAProject.EndTime;
               objtosave.Description = PostAProject.Description;
               objtosave.Members = PostAProject.Members;




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

       public bool IsValid(PostAProject obj, Result<PostAProject> result)
       {
           if (!ValidationHelper.IsStringValid(obj.WUserId.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid WUserId";
               return false;
           }
           if (!ValidationHelper.IsStringValid(obj.ProjectName))
           {
               result.HasError = true;
               result.Message = "Invalid ProjectName";
               return false;
           }

           if (!ValidationHelper.IsStringValid(obj.Price.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid Price";
               return false;
           }
           if (!ValidationHelper.IsStringValid(obj.StartTime.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid StartTime";
               return false;
           }
           if (!ValidationHelper.IsStringValid(obj.EndTime.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid EndTime";
               return false;
           } if (!ValidationHelper.IsStringValid(obj.Description))
           {
               result.HasError = true;
               result.Message = "Invalid Description";
               return false;
           }
           if (!ValidationHelper.IsStringValid(obj.Members.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid Members";
               return false;
           }


           return true;
       }

       public Result<List<PostAProject>> GetAll(string key = "")
       {
           var result = new Result<List<PostAProject>>() { Data = new List<PostAProject>() };

           try
           {
               IQueryable<PostAProject> query = _context.postAProjects;

               if (ValidationHelper.IsIntValid(key))
               {
                   var a = Int32.Parse(key);
                   query = query.Where(q => q.PostId == a);
               }

               if (ValidationHelper.IsStringValid(key))
               {
                   var a = Int32.Parse(key);
                   query = query.Where(q => q.WUserId == a);

               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.ProjectName.Contains(key));

               }

               if (ValidationHelper.IsStringValid(key))
               {
                   var a = float.Parse(key);
                   query = query.Where(q => q.Price == a);

               }


               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.StartTime.ToString().Contains(key));

               }
               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.EndTime.ToString().Contains(key));

               }
               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.Description.ToString().Contains(key));

               }

               if (ValidationHelper.IsStringValid(key))
               {
                   var a = Int32.Parse(key);
                   query = query.Where(q => q.Members == a);

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

      

       public Result<PostAProject> GetByID(int id)
       {
           var result = new Result<PostAProject>();

           try
           {
               var obj = _context.postAProjects.FirstOrDefault(c => c.PostId == id);
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

       public Result<PostAProject> GetLastId()
       {
           var result = new Result<PostAProject>();

           try
           {
               var obj = _context.postAProjects.OrderByDescending(m=>m.PostId).FirstOrDefault();
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

       public Result<bool> Delete(int id)
       {
           var result = new Result<bool>();

           try
           {
               var objtodelete = _context.postAProjects.FirstOrDefault(c => c.PostId == id);
               if (objtodelete == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid PostId";
                   return result;


               }

               _context.postAProjects.Remove(objtodelete);
               _context.SaveChanges();

           }
           catch (Exception e)
           {
               result.HasError = true;
               result.Message = e.Message;


           }
           return result;
       }

       private bool IsValidToSave(PostAProject obj, Result<PostAProject> result)
       {
           if (!ValidationHelper.IsIntValid(obj.PostId.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid UserID";
               return false;

           }
           if (_context.postAProjects.Any(u => u.PostId == obj.PostId))
           {

               result.HasError = true;
               result.Message = "Email PostId";
               return false;



           }
           return true;

       }

       //public int CountMember(int id)
       //{
           
       //}
    }
}
