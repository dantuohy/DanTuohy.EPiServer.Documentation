using System.Collections.Generic;
using EPiServer.Security;
using EPiServer.Shell.Navigation;

namespace DanTuohy.EPiServer.Docs.Business
{
    [MenuProvider]
    public class DocumantationMenuItem : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            var documentationMenuItem = new UrlMenuItem("Documentation", "/global/cms/documentation", "/documentation/index")
            {
                IsAvailable = (request) => PrincipalInfo.HasEditAccess,
                Target = "_blank"
            };

            return new MenuItem[] { documentationMenuItem };
        }
    }
}