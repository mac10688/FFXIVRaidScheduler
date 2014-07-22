[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RaidScheduler.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(RaidScheduler.App_Start.NinjectWebCommon), "Stop")]

namespace RaidScheduler.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using RaidScheduler.Domain.Services;
    using RaidScheduler.Domain.DomainModels;
    using RaidScheduler.Domain.Repositories;
    using Microsoft.AspNet.Identity;
    using RaidScheduler.Domain.Data;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<RaidSchedulerContext, DbContext, IdentityDbContext<User>>().To<RaidSchedulerContext>().InRequestScope().WithConstructorArgument("connectionString", System.Configuration.ConfigurationManager.ConnectionStrings["RaidSchedulerContext"].ToString());

            kernel.Bind<IRepository<Job>>().To<JobRepository>();
            kernel.Bind<IRepository<Player>>().To<PlayerRepository>();
            kernel.Bind<IRepository<PlayerDayAndTimeAvailable>>().To<PlayerDayAndTimeAvailableRepository>();
            kernel.Bind<IRepository<PotentialJob>>().To<PlayerPotentialJobRepository>();
            kernel.Bind<IRepository<Raid>>().To<RaidRepository>();
            kernel.Bind<IRepository<RaidCriteria>>().To<RaidCriteriaRepository>();
            kernel.Bind<IRepository<RaidRequested>>().To<RaidRequestedRepository>();
            kernel.Bind<IRepository<StaticPartyDayAndTimeSchedule>>().To<StaticPartyDayAndTimeScheduleRepository>();
            kernel.Bind<IRepository<StaticMember>>().To<StaticPartyMemberRepository>();
            kernel.Bind<IRepository<StaticParty>>().To<StaticPartyRepository>();

            kernel.Bind<IRaidService>().To<RaidService>();
            kernel.Bind<IPartyService>().To<PartyCombinationService>();
            kernel.Bind<ISchedulingService>().To<SchedulingDomain>();

            kernel.Bind<IUserStore<User>>().To<UserStore<User>>();
            kernel.Bind<UserManager<User>>().ToSelf();
        }        
    }
}
