using DanTuohy.EPiServer.Docs.Interfaces;
using EPiServer.Shell;
using System.Linq;
using System.Web.Mvc;
using DanTuohy.EPiServer.Docs.Models.ViewModels;


namespace DanTuohy.EPiServer.Docs.Controllers
{
    [Authorize(Roles = "Administrators, WebAdmins, WebEditors")]
    public class DocumentationController : Controller
    {
        private readonly IContentTypeService _contentTypeService;
        public DocumentationController(IContentTypeService contentTypeService)
        {
            _contentTypeService = contentTypeService;
        }
        // GET: Documentation
        public ActionResult Index()
        {
            return View(Paths.ToResource(GetType(), "Views/Documentation/index.cshtml"), new DocumentationPageViewModel
            {
                Pages = _contentTypeService.GetAllPages().ToDictionary(x => x.ID, x => x.LocalizedFullName),
                Blocks = _contentTypeService.GetAllBlocks().ToDictionary(x => x.ID, x => x.LocalizedFullName),
                Media = _contentTypeService.GetAllMedia().ToDictionary(x => x.ID, x => x.LocalizedFullName)
            });
        }

        [HttpGet]
        public ActionResult GetContentType(int id)
        {
            var contentType = _contentTypeService.ListAll().FirstOrDefault(x => x.ID == id);
            var doc = _contentTypeService.GetContentTypeDoc(contentType);
            
            return Json(doc, JsonRequestBehavior.AllowGet);
        }
    }
}