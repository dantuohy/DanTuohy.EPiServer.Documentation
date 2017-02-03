using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EPiServer.Logging.Compatibility;
using EPiServer.Shell;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models.ViewModels;

namespace Tuohy.Epi.Docs.Controllers
{
    [Authorize(Roles = "Administrators, WebAdmins, WebEditors")]
    public class DocumentationController : Controller
    {
        private static ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IContentTypeService _contentTypeService;
        private readonly IDocumentationSettingsService _settingsService;
        private readonly IScheduledJobService _jobService;

        public DocumentationController(
            IContentTypeService contentTypeService,
            IDocumentationSettingsService settingsService,
            IScheduledJobService jobService)
        {
            _contentTypeService = contentTypeService;
            _settingsService = settingsService;
            _jobService = jobService;
        }
        // GET: Documentation
        public ActionResult Index()
        {
            var idString = Request.QueryString["id"];
            _log.Debug("Documentation: IDString=" + idString);
            var model = new ContentTypeViewModel()
            {
                Pages = _contentTypeService.GetAllPages().ToDictionary(x => x.ID, x => x.LocalizedFullName),
                Blocks = _contentTypeService.GetAllBlocks().ToDictionary(x => x.ID, x => x.LocalizedFullName),
                Media = _contentTypeService.GetAllMedia().ToDictionary(x => x.ID, x => x.LocalizedFullName)
            };

            int id;
            if (!string.IsNullOrWhiteSpace(idString) && int.TryParse(idString, out id))
            {
                var contentType = _contentTypeService.ListAll().FirstOrDefault(x => x.ID == id);
                if (contentType == null)
                {
                    _log.Error("Documentation: The content type with id: " + id + " does not exist");
                }
                else
                {
                    model.FocusedContentTypeDoc = _contentTypeService.GetContentTypeDoc(contentType);
                }
            }

            var settings = _settingsService.GetSettings();

            if (settings != null)
            {
                model.PageDesign = new PageDesign
                {
                    LogoUrl = settings.LogoUrl,
                    PriamryColour = settings.PriamryColour,
                    SecondaryColour = settings.SecondaryColour,
                    TextColour = settings.TextColour
                };
                if (settings.IncludeJobs)
                {
                    model.Jobs = _jobService.ListAll();
                }
            }

            return View(Paths.ToResource(GetType(), "Views/Documentation/index.cshtml"), model);
        }

        public ActionResult Job()
        {
            var idString = Request.QueryString["id"];
            _log.Debug("Documentation: IDString=" + idString);
            var model = new JobViewModel()
            {
                Pages = _contentTypeService.GetAllPages().ToDictionary(x => x.ID, x => x.LocalizedFullName),
                Blocks = _contentTypeService.GetAllBlocks().ToDictionary(x => x.ID, x => x.LocalizedFullName),
                Media = _contentTypeService.GetAllMedia().ToDictionary(x => x.ID, x => x.LocalizedFullName),
                Jobs = _jobService.ListAll()
            };

            var settings = _settingsService.GetSettings();

            if (settings != null)
            {
                model.PageDesign = new PageDesign
                {
                    LogoUrl = settings.LogoUrl,
                    PriamryColour = settings.PriamryColour,
                    SecondaryColour = settings.SecondaryColour,
                    TextColour = settings.TextColour
                };
                if (!settings.IncludeJobs)
                {
                    return RedirectToAction("Index");
                }
            }

            Guid id;
            if (!string.IsNullOrWhiteSpace(idString) && Guid.TryParse(idString, out id))
            {
                model.FocusedJobDoc = _jobService.GetJob(id);
            }

            return View(Paths.ToResource(GetType(), "Views/Documentation/job.cshtml"), model);
        }
    }
}