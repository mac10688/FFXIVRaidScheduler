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
    using RaidScheduler.Domain.DomainModels.JobDomain;
    using RaidScheduler.Domain.DomainModels.RaidDomain;
    using RaidScheduler.Domain.DomainModels.PlayerDomain;
    using RaidScheduler.Domain.DomainModels.StaticPartyDomain;
    using RaidScheduler.Domain.DomainModels.UserDomain;
    using RaidScheduler.Domain.Repositories.Interfaces;
    using RaidScheduler.Domain.Queries.Interfaces;
    using RaidScheduler.Domain.Queries;

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

            kernel.Bind<IRepository<Player>>().To<PlayerRepository>();                      
            kernel.Bind<IRepository<StaticParty>>().To<StaticPartyRepository>();
            kernel.Bind<IRaidFactory>().To<RaidFactory>();
            kernel.Bind<IJobFactory>().To<JobFactory>();
            kernel.Bind<IPlayerSearch>().To<PlayerSearch>();

            kernel.Bind<IPartyService>().To<PartyCombinationService>();
            kernel.Bind<ISchedulingDomainService>().To<SchedulingDomain>();
            kernel.Bind<IJobCombination>().To<JobCombination>();

            kernel.Bind<IUserStore<User>>().To<UserStore<User>>();
            kernel.Bind<UserManager<User>>().ToSelf();
        }
    }
}
