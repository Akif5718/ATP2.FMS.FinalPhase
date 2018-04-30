﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ATP2.FMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IUnityContainer container = new IUnityContainer();
            container.RegisterType<IOwnerService, OwnerService>();
            container.RegisterType<IRatingWorkerService, RatingWorkerService>();
            container.RegisterType<IRatingOwnerService, RatingOwnerService>();
            container.RegisterType<IWorkerService, WorkerService>();
            container.RegisterType<IWorkerSkillService, WorkerSkillService>();
            container.RegisterType<IEducationalService, EducationalService>();
            container.RegisterType<IUserInfoService, UserInfoService>();
            container.RegisterType<IWorkHistoryService, WorkHistoryService>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IReportService, ReportService>();
            container.RegisterType<IService, Service>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IskillService, skillService>();
            container.RegisterType<IComentSectionService, ComentSectionService>();
            container.RegisterType<IPostAProjectService, PostAProjectService>();
            container.RegisterType<IProjectSectionService, ProjectSectionService>();
            container.RegisterType<IProjectSkillService, ProjectSkillService>();
            container.RegisterType<IResponseToAJobService, ResponseToAJobService>();
            container.RegisterType<ISavedFileService, SavedFileService>();
            container.RegisterType<ISelectedWorkerService, SelectedWorkerService>();
            container.RegisterType<IPaymentService, PaymentService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}