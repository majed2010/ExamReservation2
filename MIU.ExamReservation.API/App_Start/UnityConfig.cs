using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using Microsoft.Practices.Unity;
using MIU.ExamReservation.Data;
using MIU.ExamReservation.Service;
using MIU.Data;
using MIU.Data.Common;
using MIU.Data.EntityFramework;
using MIU.Data.InMemory;
using MIU.Identity.Owin;
using MIU.Shared.Domain;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Unity.SelfHostWebApiOwin;
using MIULog;


namespace MIU.AdmissionBackEnd.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            string cacheServerIP = ConfigurationManager.AppSettings["CacheServerIP"];
            string CacheServerPort = ConfigurationManager.AppSettings["CacheServerPort"];
            if (string.IsNullOrEmpty(cacheServerIP))
            {
                throw new Exception("CacheServerIP is not configured");
            }

            container.RegisterType<IOptions<RedisCacheOptions>, RedisCacheOptions>();
            container.RegisterInstance<RedisCacheOptions>(new RedisCacheOptions() { Configuration = cacheServerIP });
            container.RegisterType<IDistributedCache, RedisCache>(new InjectionConstructor(container.Resolve(typeof(IOptions<RedisCacheOptions>))));
            //container.RegisterType<IUnitOfWorkFactory, EntityFrameworkUnitOfWorkFactory>(new InjectionConstructor(container.Resolve(typeof(AdmissionModel))));
            container.RegisterType<IUnitOfWork, EntityFrameworkUnitOfWork>();
            container.RegisterType<IDataManager, DataManager>();
            container.RegisterType<IApplicationUser, ApplicationUser>();
            //container.RegisterType<DbContext, AdmissionModel>();
            container.RegisterType<IUnitOfWorkFactory, InMemoryUnitOfWorkFactory>(new InjectionConstructor(
                    container.Resolve(typeof(IDistributedCache)),
                    container.Resolve(typeof(IOptions<RedisCacheOptions>))
                ));

            container.RegisterType<IDataManagerFactory, DataManagerFactory>(
                new InjectionConstructor(

                  //  container.Resolve(typeof(DbContext)),
                    container.Resolve(typeof(IDistributedCache)),
                    container.Resolve(typeof(IOptions<RedisCacheOptions>)),
                    container.Resolve(typeof(EntityFrameworkUnitOfWorkFactory)),
                    container.Resolve(typeof(InMemoryUnitOfWorkFactory)),
                    container.Resolve(typeof(IApplicationUser))

                    ));

            //container.RegisterType<IRegistrarAcceptanceService,RegistrarAcceptanceService > ();

            //container.RegisterType<IApplyService, ApplyService>();
            //container.RegisterType<IFacultyPercentageService, FacultyPercentageService>();
            //container.RegisterType<IEnglishService, EnglishService>();
            //container.RegisterType<IAptitudeService, AptitudeService>();
            //container.RegisterType<IExamService, ExamService>();
            //container.RegisterType<ICertificateService, CertificateService>();
            //container.RegisterType<IDocumentService, DocumentService>();
            //container.RegisterType<ICategoryService, CategoryService>();
            //container.RegisterType<IActionLogService, ActionLogService>();
            //container.RegisterType<LogService, LogService>();
            //container.RegisterType<IApplicantDocumentService, ApplicantDocumentService>();
            //container.RegisterType<IApplicationAdmissionFeesService, ApplicationAdmissionFeesService>();
            //container.RegisterType<IReportService, ReportService>();
            //container.RegisterType<IApplicationPercentageService, ApplicationPercentageService>();
            //container.RegisterType<IFileRequestService, FileRequestService>();
            //container.RegisterType<IFillingService, FillingService>();
            //container.RegisterType<ISchoolService, SchoolService>();
            //container.RegisterType<IForeignApplicantService, ForeignApplicantService>();
            //container.RegisterType<IEditApplicantAfterAcceptService, EditApplicantAfterAcceptService>();
            //container.RegisterType<ICountryService, CountryService>();
            //container.RegisterType<IWaitingService, WaitingService>();
            //container.RegisterType<IApplicantDocumentFiles_ViewService, ApplicantDocumentFiles_ViewService>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}