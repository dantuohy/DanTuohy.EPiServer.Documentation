namespace Tuohy.Epi.Docs.Models.ViewModels
{
    public class ContentTypeViewModel : DocumentationPageViewModel
    {
       
        public ContentTypeDoc FocusedContentTypeDoc { get; set; }

        public ContentTypeViewModel(ContentTypeDoc focusedContentTypeDoc)
        {
            FocusedContentTypeDoc = focusedContentTypeDoc;
            if (focusedContentTypeDoc != null)
            {
                FocusedContentTypeType = focusedContentTypeDoc.Type;
                FocusedContentTypeId = focusedContentTypeDoc.Id.ToString();
                FocusedContentTypeGroup = focusedContentTypeDoc.Group;
            }
        }

        public ContentTypeViewModel(ContentTypeDoc focusedContentTypeDoc, bool hideNavigation)
            : this(focusedContentTypeDoc)
        {
            HideNavigation = hideNavigation;

        }
    }
}