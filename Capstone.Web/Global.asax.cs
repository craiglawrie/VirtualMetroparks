using Capstone.Web.DAL;
using Ninject;
using Ninject.Web.Common.WebHost;
using Ninject.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using System.Web.Routing;

namespace Capstone.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            kernel.Bind<IParkDAL>().To<ParkSqlDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<ITrailDAL>().To<TrailSqlDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IPanoramicDAL>().To<PanoramicSqlDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<ILastSeenImagesDAL>().To<LastSeenImagesSqlDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<ILastSeenVideosDAL>().To<LastSeenVideosSqlDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IAdminNewHotspotDAL>().To<AdminNewHotspotSqlDAL>().WithConstructorArgument("connectionString", connectionString);

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            return kernel;
        }
    }
}
