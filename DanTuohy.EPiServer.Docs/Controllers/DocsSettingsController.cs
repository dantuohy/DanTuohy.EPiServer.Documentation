using System.Web.Mvc;
using EPiServer.PlugIn;

namespace Tuohy.Epi.Docs.Controllers
{
    [GuiPlugIn(Area = PlugInArea.AdminMenu, DisplayName = "Site Docs Settings", UrlFromModuleFolder = "/DocsSettings/index")]
    public class DocsSettingsController : Controller
    {
    }
}