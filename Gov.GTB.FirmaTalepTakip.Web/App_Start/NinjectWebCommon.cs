[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Gov.GTB.FirmaTalepTakip.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Gov.GTB.FirmaTalepTakip.Web.App_Start.NinjectWebCommon), "Stop")]


namespace Gov.GTB.FirmaTalepTakip.Web.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    using Gov.GTB.FirmaTalepTakip.Repository.DataContext;
    using Gov.GTB.FirmaTalepTakip.Repository.Interface;
    using Gov.GTB.FirmaTalepTakip.Repository.Repository;
    using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Abstract;
    using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            // Install our Ninject-based IDependencyResolver into the Web API config
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            kernel.Bind<FirmaDbContext>().To<FirmaDbContext>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IFirmaRepository>().To<FirmaRepository>();
            kernel.Bind<IGumrukKodRepository>().To<GumrukKodRepository>();
            kernel.Bind<ITalepDetayFirmaRepository>().To<TalepDetayFirmaRepository>();
            kernel.Bind<IRefTalepCevapRepository>().To<RefTalepCevapRepository>();
            kernel.Bind<IRefTalepKonuRepository>().To<RefTalepKonuRepository>();
            kernel.Bind<ICevapRepository>().To<CevapRepository>();
        }
    }
}