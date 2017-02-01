using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Web.Routing;

namespace Tuohy.Epi.Docs.Business
{
    [InitializableModule]
    [ModuleDependency(typeof(global::EPiServer.Web.InitializationModule))]
    public class DocumentationInitialization : IInitializableModule
    {
        public static void Register(HttpConfiguration config)
        {
            // Attribute routing.
            //RouteTable.Routes.MapRoute(
            //    "DocumentationSettings",
            //    "documenationsettings",
            //    new { controller = "DocumenationSettings", action = "Index" });
        }

        public void Initialize(InitializationEngine context)
        {
           // GlobalConfiguration.Configure(Register);
    
        }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }
    }
}