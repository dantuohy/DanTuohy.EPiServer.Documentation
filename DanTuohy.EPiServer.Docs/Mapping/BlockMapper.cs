using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models;

namespace Tuohy.Epi.Docs.Mapping
{
    [ServiceConfiguration(ServiceType = typeof(IMapToNew<ContentType, BlockDoc>))]
    public class BlockMapper : IMapToNew<ContentType, BlockDoc>
    {
        public BlockDoc Map(ContentType source)
        {
            var blockDoc = new BlockDoc
            {
                Id = source.ID,
                Type = ContentTypeEnum.Block,
                Name = source.LocalizedName,
                Description = source.LocalizedDescription,
                Group = source.LocalizedGroupName,
                SortOrder = source.SortOrder,
                IsAvaliable = source.IsAvailable
            };
            
            return blockDoc;
        }
    }
}