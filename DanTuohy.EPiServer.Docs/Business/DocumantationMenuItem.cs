using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Security;
using EPiServer.Shell.Navigation;

namespace Alloy.Documentation.Business
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