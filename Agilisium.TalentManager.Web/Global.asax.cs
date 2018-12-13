using Agilisium.TalentManager.Model;
using Agilisium.TalentManager.Web.App_Start;
using Agilisium.TalentManager.Web.Helpers;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Agilisium.TalentManager.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer(new TalentManagerSeedData());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Run();

            Application[UIConstants.CONFIG_ENABLE_PAGINATION] = ConfigurationManager.AppSettings[UIConstants.CONFIG_ENABLE_PAGINATION];
            Application[UIConstants.CONFIG_RECORDS_PER_PAGE] = ConfigurationManager.AppSettings[UIConstants.CONFIG_RECORDS_PER_PAGE];
        }
    }
}
