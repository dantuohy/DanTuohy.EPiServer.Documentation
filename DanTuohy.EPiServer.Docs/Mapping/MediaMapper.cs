using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models;

namespace Tuohy.Epi.Docs.Mapping
{
    [ServiceConfiguration(ServiceType = typeof(IMapToNew<ContentType, MediaDoc>))]
    public class MediaMapper : IMapToNew<ContentType, MediaDoc>
    {
        public MediaDoc Map(ContentType source)
        {
            var mediaDoc = new MediaDoc()
            {
                Type = ContentTypeEnum.Media,
                Name = source.LocalizedFullName,
                Description = source.LocalizedDescription,
                Group = source.LocalizedGroupName,
                SortOrder = source.SortOrder,
                IsAvaliable = source.IsAvailable
            };
            
            return mediaDoc;
        }
    }
}