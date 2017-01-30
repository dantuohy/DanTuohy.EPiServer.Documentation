using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models;

namespace Tuohy.Epi.Docs.Mapping
{
    [ServiceConfiguration(ServiceType = typeof(IMapToNew<ContentType, PageDoc>))]
    public class PageMapper : IMapToNew<ContentType, PageDoc>
    {
        public PageDoc Map(ContentType source)
        {
            var pageDoc = new PageDoc
            {
                Type = ContentTypeEnum.Page,
                Name = source.LocalizedFullName,
                Description = source.LocalizedDescription,
                Group = source.LocalizedGroupName,
                SortOrder = source.SortOrder,
                IsAvaliable = source.IsAvailable
            };
            
            return pageDoc;
        }
    }
}