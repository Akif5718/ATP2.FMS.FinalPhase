using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Entities;

namespace FMS.Infrastructure
{
    public class FMSDbContext:DbContext
    {
        public FMSDbContext() : base("FMSDbContext")
        {
            
        }
       public DbSet<UserInfo> userInfos { get; set; }
       public DbSet<OwnerInfo> ownerInfos { get; set; }
       public DbSet<WorkerInfo> workerInfos { get; set; }
       public DbSet<RatingWorker> ratingWorkers { get; set; }
       public DbSet<RatingOwner> ratingOwners { get; set; }
       public DbSet<WorkerSkill> workerSkills { get; set; }
       public DbSet<EducationalBackground> educationalBackgrounds { get; set; }
       public DbSet<WorkHistory> workHistories { get; set; }
       public DbSet<Skill> skills { get; set; }
       public DbSet<SkillCategory> skillCategories { get; set; }
       public DbSet<PostAProject> postAProjects { get; set; }
       public DbSet<ProjectSection> projectSections { get; set; }
       public DbSet<ProjectSkills> projectSkillses { get; set; }
       public DbSet<ResponseToaJob> responseToaJobs { get; set; }
       public DbSet<SelectedWorker> selectedWorkers { get; set; }
       public DbSet<COMMENTSEC> commentsecs { get; set; }
       public DbSet<SavedFile> savedFiles { get; set; }
       public DbSet<Payment> payments { get; set; } 

    }
}
