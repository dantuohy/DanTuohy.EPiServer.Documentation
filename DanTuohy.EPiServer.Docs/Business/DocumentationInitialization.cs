using System.Web.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Web.Routing;

namespace DanTuohy.EPiServer.Docs.Business
{
    [InitializableModule]
    [ModuleDependency(typeof(global::EPiServer.Web.InitializationModule))]
    public class DocumentationInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
         
            RouteTable.Routes.MapContentRoute(
                "Documentation",
                "Documentation",
                new { controller = "documentation", action = "index" });
        }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }
    }
}