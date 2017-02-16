using System.Web.Mvc;
using EPiServer.DataAbstraction;
using EPiServer.Shell;
using EPiServer.Shell.Gadgets;
using EPiServer.Shell.Web;
using Tuohy.Epi.ContentTypesInUse.Interfaces;
using Tuohy.Epi.ContentTypesInUse.Models;

namespace Tuohy.Epi.ContentTypesInUse.Controllers
{
    [Gadget(Title = "Content Types Currently In Use", Name = "Content Types Currently In Use", Description = "Show a list of all page and block types and how many of each are currently in use on the site. Will also show where in the site these are being used")]
    [CssResource("ClientResources/css/inUse.css")]
    public class InUseController : Controller
    {
        private const int PageSize = 10;
        private readonly IContentUseService _contentUseService;

        public InUseController(IContentUseService contentUseService)
        {
            _contentUseService = contentUseService;
        }

        public ActionResult Index()
        {
            return View(Paths.ToResource(GetType(), "Views/InUse/index.cshtml"), _contentUseService.GetContentTypesInUse());
        }

        [HttpPost]
        public ActionResult GetLinks(int id, int? page)
        {
            ContentType contentType;
            var links = _contentUseService.GetUsageUrlsForContentType(id, page ?? 0, PageSize, out contentType);

            if (links == null) return View(new LinksViewModel());

            var model = new LinksViewModel
            {
                ContentTypeId = contentType.ID,
                ContentTypeName = contentType.LocalizedName,
                Links = links,
                PageNumber = page ?? 0
            };
            return View(Paths.ToResource(GetType(), "Views/InUse/GetLinks.cshtml"), model);
        }
    }
}