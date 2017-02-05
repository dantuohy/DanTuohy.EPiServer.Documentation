using EPiServer.Data;
using Tuohy.Epi.Docs.Models.DynamicData;

namespace Tuohy.Epi.Docs.Interfaces
{
    public interface ICustomDocumentationService
    {
        Identity Updrt(CustomDocumetation data);

        CustomDocumetation GetCustomDocumentation(int contentTypeId);
    }
}