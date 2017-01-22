
using DanTuohy.EPiServer.Docs.Interfaces;
using DanTuohy.EPiServer.Docs.Models;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;

namespace DanTuohy.EPiServer.Docs.Mapping
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