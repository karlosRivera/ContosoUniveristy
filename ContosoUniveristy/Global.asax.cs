using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Contoso.Web.Models;
using System.Web.Security;
using Contoso.Web.IoC;
using Contoso.Data;
using Contoso.Domain;
using Contoso.Data.Infrastructure;
using Contoso.Service;
using Contoso.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.Practices.ServiceLocation;


using Contoso.Web.Log;

namespace ContosoUniveristy
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            Database.SetInitializer<SchoolContext>(new SchoolInitializer());
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            IUnityContainer container = GetUnityContainer();
            var serviceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private IUnityContainer GetUnityContainer()
        {
            //Create UnityContainer          
            IUnityContainer container = new UnityContainer()
                //.RegisterType<IControllerActivator, CustomControllerActivator>() // No nned to a controller activator
            .RegisterType<IFormsAuthenticationService, FormsAuthenticationService>()
            .RegisterType<IMembershipService, AccountMembershipService>()
            .RegisterInstance<MembershipProvider>(Membership.Provider)
            .RegisterType<IDatabaseFactory, DatabaseFactory>(new HttpContextLifetimeManager<IDatabaseFactory>())
            .RegisterType<IUnitOfWork, UnitOfWork>(new HttpContextLifetimeManager<IUnitOfWork>())
            .RegisterType<IStudentRepository, StudentRepository>(new HttpContextLifetimeManager<IStudentRepository>())
            .RegisterType<IRepositoryService<Student>, StudentService>(new HttpContextLifetimeManager<IRepositoryService<Student>>())
            .RegisterType<ICourseRepository, CourseRepository>(new HttpContextLifetimeManager<ICourseRepository>())
            .RegisterType<IRepositoryServiceGet<Course>, CourseService>(new HttpContextLifetimeManager<IRepositoryServiceGet<Course>>())
            .RegisterType<IDepartmentRepository, DepartmentRepository>(new HttpContextLifetimeManager<IDepartmentRepository>())
            .RegisterType<IRepositoryService<Department>, DepartmentService>(new HttpContextLifetimeManager<IRepositoryService<Student>>())
            .RegisterType<IInstructorRepository, InstructorRepository>(new HttpContextLifetimeManager<IInstructorRepository>())
            .RegisterType<IRepositoryService<Instructor>, InstructorService>(new HttpContextLifetimeManager<IRepositoryService<Instructor>>())
            .RegisterType<IEnrollmentRepository, EnrollmentRepository>(new HttpContextLifetimeManager<IEnrollmentRepository>())
            .RegisterType<IRepositoryService<Enrollment>, EnrollmentService>(new HttpContextLifetimeManager<IRepositoryService<Enrollment>>())
            .RegisterType<IUserRepository, UserRepository>(new HttpContextLifetimeManager<IUserRepository>())
            .RegisterType<IRoleRepository, RoleRepository>(new HttpContextLifetimeManager<IRoleRepository>())
            .RegisterType<ILoggingService, LoggingService>(new HttpContextLifetimeManager<ILoggingService>())
            .RegisterType<ISecurityService, SecurityService>(new HttpContextLifetimeManager<ISecurityService>());
            return container;
        }
    }
}