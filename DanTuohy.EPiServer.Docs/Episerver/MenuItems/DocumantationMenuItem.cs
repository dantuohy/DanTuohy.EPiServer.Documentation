using System.Collections.Generic;
using EPiServer.Security;
using EPiServer.Shell;
using EPiServer.Shell.Navigation;
using Tuohy.Epi.Docs.Controllers;

namespace Tuohy.Epi.Docs.Business
{
    [MenuProvider]
    public class DocumantationMenuItem : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            var documentationMenuItem = new UrlMenuItem("Documentation", "/global/cms/documentation", Paths.ToResource(typeof(DocumentationController), "documentation"))
            {
                IsAvailable = (request) => PrincipalInfo.HasEditAccess,
                Target = "_blank"
            };

            return new MenuItem[] { documentationMenuItem };
        }
    }
}