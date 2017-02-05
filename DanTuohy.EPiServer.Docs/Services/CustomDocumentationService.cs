using System.Linq;
using EPiServer.Data;
using EPiServer.Data.Dynamic;
using EPiServer.ServiceLocation;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models.DynamicData;

namespace Tuohy.Epi.Docs.Services
{
    [ServiceConfiguration(ServiceType = typeof(ICustomDocumentationService))]
    public class CustomDocumentationService : ICustomDocumentationService
    {
        public Identity Updrt(CustomDocumetation data)
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(CustomDocumetation));
            var oldDocs = GetCustomDocumentation(data.ContentTypeId);
            if (oldDocs == null)
            {
                return store.Save(data);

            }
            data.Id = oldDocs.Id;

            return store.Save(data);
        }

        public CustomDocumetation GetCustomDocumentation(int contentTypeId)
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(CustomDocumetation));

            return store.Items<CustomDocumetation>().FirstOrDefault(x => x.ContentTypeId == contentTypeId);
        }
    }
}