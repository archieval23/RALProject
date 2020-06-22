using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using RALProject.ApplicationService.ServiceContract;
using RALProject.ApplicationService.Services;
using RALProject.Infrastructure.EntityFramework.Common;
using RALProject.Infrastructure.EntityFramework.RAL;
using AutoMapper;
using RALProject.Domain.Contracts;
using RALProject.Infrastructure.Repository;
using RALProject.Common.EmailHelper;

namespace RALProject.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IRALUnitOfWork, RALUnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<ICommonUnitOfWork, CommonUnitOfWork>(new HierarchicalLifetimeManager());
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    

            // Service Registry
            container.RegisterType<IRALServices, RALServices>();
            container.RegisterType<IReportServices, ReportServices>();
            container.RegisterType<IEmailManager, EmailManager>();

            // Repository Registry
            container.RegisterType<IBusinessUnitRepository, BusinessUnitRepository>();
            container.RegisterType<ILoginRepository, LoginRepository>();
            container.RegisterType<IPORepository, PORepository>();
            container.RegisterType<IReportRepository, ReportRepository>();
            container.RegisterType<IStoreRepository, StoreRepository>();
            container.RegisterType<IVendorRepository, VendorRepository>();

            // AutoMapper Registry
            container.RegisterInstance<IMapper>(AutoMapperConfig().CreateMapper());

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }

        public static MapperConfiguration AutoMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebMappingProfile());
                cfg.AddProfile(new ServiceMappingProfile());
                cfg.AddProfile(new DomainMappingProfile());
            });
        }
    }
}
