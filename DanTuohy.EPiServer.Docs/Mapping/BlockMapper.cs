
using DanTuohy.EPiServer.Docs.Interfaces;
using DanTuohy.EPiServer.Docs.Models;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;

namespace DanTuohy.EPiServer.Docs.Mapping
{
    [ServiceConfiguration(ServiceType = typeof(IMapToNew<ContentType, BlockDoc>))]
    public class BlockMapper : IMapToNew<ContentType, BlockDoc>
    {
        public BlockDoc Map(ContentType source)
        {
            var blockDoc = new BlockDoc
            {
                Type = ContentTypeEnum.Block,
                Name = source.LocalizedFullName,
                Description = source.LocalizedDescription,
                Group = source.LocalizedGroupName,
                SortOrder = source.SortOrder,
                IsAvaliable = source.IsAvailable
            };
            
            return blockDoc;
        }
    }
}