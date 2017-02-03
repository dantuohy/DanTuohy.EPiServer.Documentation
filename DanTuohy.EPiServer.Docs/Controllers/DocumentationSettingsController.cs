using System.Web.Mvc;
using EPiServer.PlugIn;
using EPiServer.Shell;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models.DynamicData;
using Tuohy.Epi.Docs.Models.ViewModels;

namespace Tuohy.Epi.Docs.Controllers
{
    [GuiPlugIn(Area = PlugInArea.AdminMenu, DisplayName = "Documentation Settings", UrlFromModuleFolder = "/documentationsettings/index")]
    [Authorize(Roles = "CmsAdmins")]
    public class DocumentationSettingsController : Controller
    {
        private readonly IDocumentationSettingsService _settingsService;
        public DocumentationSettingsController(IDocumentationSettingsService settingsService)
        {
            _settingsService = settingsService;
        }
        public ActionResult Index()
        {
            var settings = _settingsService.GetSettings();

            return View(Paths.ToResource(GetType(), "Views/DocumentationSettings/index.cshtml"),
            settings == null ? new DocumentationSettingsViewModel() : new DocumentationSettingsViewModel
            {
                Logo = settings.LogoUrl,
                PrimaryColour = settings.PriamryColour,
                SecondaryColour = settings.SecondaryColour,
                TextColour = settings.TextColour,
                IncludeJobs = settings.IncludeJobs
            });
        }

        public ActionResult Save(DocumentationSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                _settingsService.UpdrtSettings(new DocumentationSettings
                {
                    LogoUrl = model.Logo,
                    PriamryColour = model.PrimaryColour,
                    SecondaryColour = model.SecondaryColour,
                    TextColour = model.TextColour,
                    IncludeJobs = model.IncludeJobs
                });
            }

            return RedirectToAction("Index");
        }
    }
}