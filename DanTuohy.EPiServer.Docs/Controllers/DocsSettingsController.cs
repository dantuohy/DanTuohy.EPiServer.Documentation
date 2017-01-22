using EPiServer.PlugIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DanTuohy.EPiServer.Docs.Controllers
{
    [GuiPlugIn(Area = PlugInArea.AdminMenu, DisplayName = "Site Docs Settings", UrlFromModuleFolder = "/DocsSettings/index")]
    public class DocsSettingsController : Controller
    {
    }
}