namespace Tuohy.Epi.Docs.Models.ViewModels
{
    public class JobViewModel : DocumentationPageViewModel
    {
        public JobDoc FocusedJobDoc { get; set; }

        public JobViewModel(JobDoc focusedJobDoc)
        {
            FocusedJobDoc = focusedJobDoc;
            if (focusedJobDoc != null)
            {
                FocusedContentTypeType = ContentTypeEnum.Job;
                FocusedContentTypeId = focusedJobDoc.Id.ToString();
            }
        }
    }
}