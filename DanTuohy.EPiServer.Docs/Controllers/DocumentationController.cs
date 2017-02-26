using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EPiServer.Logging.Compatibility;
using EPiServer.Shell;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models.ViewModels;
using Tuohy.Epi.Docs.Models.DynamicData;
using EPiServer.DataAbstraction;
using Tuohy.Epi.Docs.Models;

namespace Tuohy.Epi.Docs.Controllers
{
    [Authorize(Roles = "Administrators, CmsAdmins, WebEditors")]
    public class DocumentationController : Controller
    {
        private static ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IContentTypeService _contentTypeService;
        private readonly IDocumentationSettingsService _settingsService;
        private readonly IScheduledJobService _jobService;
        private readonly ICustomDocumentationService _customDocumentationService;

        public DocumentationController(
            IContentTypeService contentTypeService,
            IScheduledJobService jobService,
            IDocumentationSettingsService settingsService,
            ICustomDocumentationService customDocumentationService)
        {
            _contentTypeService = contentTypeService;
            _jobService = jobService;
            _settingsService = settingsService;
            _customDocumentationService = customDocumentationService;
        }
        // GET: Documentation
        public ActionResult Index()
        {
            var editMode = Request.QueryString["edit"];
            var idString = Request.QueryString["ctid"];
            var name = Request.QueryString["name"];

            int id;
            ContentType contentType = null;
            ContentTypeDoc contentTypeDoc = null;
            if (!string.IsNullOrWhiteSpace(idString) && int.TryParse(idString, out id))
            {
                contentType = _contentTypeService.ListAll().FirstOrDefault(x => x.ID == id);
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                contentType = _contentTypeService.ListAll().FirstOrDefault(x => x.ModelType != null && x.ModelType.FullName != null && x.ModelType.FullName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            }

            if (contentType != null)
            {
                contentTypeDoc = _contentTypeService.GetContentTypeDoc(contentType);
            }

            var model = new ContentTypeViewModel(contentTypeDoc)
            {
                Pages = _contentTypeService.GetAllPages().OrderBy(x => x.LocalizedName).ToDictionary(x => x.ID, x => new NameGroupPair { ContentTypeName = x.LocalizedName, GroupName = x.GroupName }),
                Blocks = _contentTypeService.GetAllBlocks().OrderBy(x => x.LocalizedName).ToDictionary(x => x.ID, x => new NameGroupPair { ContentTypeName = x.LocalizedName, GroupName = x.GroupName }),
                Media = _contentTypeService.GetAllMedia().OrderBy(x => x.LocalizedName).ToDictionary(x => x.ID, x => new NameGroupPair { ContentTypeName = x.LocalizedName, GroupName = x.GroupName })
            };

            var customDocs = _customDocumentationService.GetCustomDocumentation(contentType == null ? "0" : contentType.ID.ToString());
            if (customDocs != null)
            {
                model.CustomDocumentation = customDocs.Markup;
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
                model.DisplayAsGroups = settings.DisplayAsGroups;

                if (settings.AllowEditing)
                {
                    if (User.IsInRole("Administrators") || User.IsInRole("CmsAdmins") || User.IsInRole("DocumentationEditors"))
                    {
                        model.CanEdit = true;
                    }

                    if (editMode != null && editMode == 1.ToString())
                    {
                        model.IsEditMode = true;
                    }
                }
            }

            return View(Paths.ToResource(GetType(), "Views/Documentation/index.cshtml"), model);
        }
        //todo confirm script tags are removed
        //todo validate no risk with SQL commands
        [ValidateInput(false)]
        public ActionResult Save(string contentTypeId, string markup)
        {
            if (!UserCanEdit(contentTypeId, markup))
            {
                return RedirectToAction("Index");
            }

            _customDocumentationService.Updrt(new CustomDocumentation
            {
                ContentTypeId = contentTypeId,
                Markup = markup
            });

            return RedirectToAction("Index", "Documentation", new { ctid = contentTypeId });
        }

        [ValidateInput(false)]
        public ActionResult SaveJob(string jobId, string markup)
        {
            if (!UserCanEdit(jobId, markup))
            {
                return RedirectToAction("Index");
            }

            _customDocumentationService.Updrt(new CustomDocumentation
            {
                ContentTypeId = jobId,
                Markup = markup
            });

            return RedirectToAction("Job", "Documentation", new { id = jobId });
        }


        public ActionResult Job()
        {
            var idString = Request.QueryString["id"];
            var editMode = Request.QueryString["edit"];

            JobDoc focusedJob = null;

            Guid id;
            if (!string.IsNullOrWhiteSpace(idString) && Guid.TryParse(idString, out id))
            {
                focusedJob = _jobService.GetJob(id);
            }

            var model = new JobViewModel(focusedJob)
            {
                Pages = _contentTypeService.GetAllPages().OrderBy(x => x.LocalizedName).ToDictionary(x => x.ID, x => new NameGroupPair { ContentTypeName = x.LocalizedName, GroupName = x.GroupName }),
                Blocks = _contentTypeService.GetAllBlocks().OrderBy(x => x.LocalizedName).ToDictionary(x => x.ID, x => new NameGroupPair { ContentTypeName = x.LocalizedName, GroupName = x.GroupName }),
                Media = _contentTypeService.GetAllMedia().OrderBy(x => x.LocalizedName).ToDictionary(x => x.ID, x => new NameGroupPair { ContentTypeName = x.LocalizedName, GroupName = x.GroupName }),
                Jobs = _jobService.ListAll()
            };

            if (focusedJob != null)
            {
                var customDocs = _customDocumentationService.GetCustomDocumentation(focusedJob.Id.ToString());

                if (customDocs != null)
                {
                    model.CustomDocumentation = customDocs.Markup;
                }
            }


            var settings = _settingsService.GetSettings();

            //todo refactor so only done once
            if (settings != null)
            {
                model.PageDesign = new PageDesign
                {
                    LogoUrl = settings.LogoUrl,
                    PriamryColour = settings.PriamryColour,
                    SecondaryColour = settings.SecondaryColour,
                    TextColour = settings.TextColour
                };

                model.DisplayAsGroups = settings.DisplayAsGroups;

                if (!settings.IncludeJobs)
                {
                    return RedirectToAction("Index");
                }

                if (settings.AllowEditing)
                {
                    if (User.IsInRole("Administrators") || User.IsInRole("CmsAdmins") || User.IsInRole("DocumentationEditors"))
                    {
                        model.CanEdit = true;
                    }

                    if (editMode != null && editMode == 1.ToString())
                    {
                        model.IsEditMode = true;
                    }
                }
            }
            return View(Paths.ToResource(GetType(), "Views/Documentation/job.cshtml"), model);
        }


        private bool UserCanEdit(string id, string markup)
        {
            var settings = _settingsService.GetSettings();

            return ((User.IsInRole("Administrators")
                      || User.IsInRole("CmsAdmins")
                      || User.IsInRole("DocumentationEditors"))
                     && (settings != null && !settings.AllowEditing)
                     && !string.IsNullOrWhiteSpace(id)
                     && !string.IsNullOrWhiteSpace(markup));
        }
    }
}