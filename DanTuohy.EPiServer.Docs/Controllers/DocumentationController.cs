using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EPiServer.Logging.Compatibility;
using EPiServer.Shell;
using Tuohy.Epi.Docs.Business.Attributes;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models.ViewModels;

namespace Tuohy.Epi.Docs.Controllers
{
   // [Authorize(Roles = "Administrators, WebAdmins, WebEditors")]
    public class DocumentationController : Controller
    {
        private static ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IContentTypeService _contentTypeService;
        public DocumentationController(IContentTypeService contentTypeService)
        {
            _contentTypeService = contentTypeService;
        }
        // GET: Documentation
        public ActionResult Index()
        {
            var idString = Request.QueryString["id"];
            _log.Debug("Documentation: IDString=" + idString);
            var model = new DocumentationPageViewModel
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

            return View(Paths.ToResource(GetType(), "Views/Documentation/index.cshtml"), model);
        }

        //[HttpGet]
        //public ActionResult ViewContentType(int id)
        //{
        //    // return Json("Success", JsonRequestBehavior.AllowGet);
        //    var contentType = _contentTypeService.ListAll().FirstOrDefault(x => x.ID == id);
        //    var doc = _contentTypeService.GetContentTypeDoc(contentType);

        //    return View(Paths.ToResource(GetType(), "Views/Documentation/ViewContentType.cshtml"), 

        //    );

        //    return Json(doc, JsonRequestBehavior.AllowGet);
        //}
    }
}