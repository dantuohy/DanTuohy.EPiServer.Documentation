using System.IO;
using EPiServer.Core;
using EPiServer.Data;
using EPiServer.Data.Dynamic;
using EPiServer.DynamicContent;

namespace Tuohy.Epi.Docs.Models.DynamicData
{
    [EPiServerDataStore(AutomaticallyRemapStore = true)]
    public class CustomDocumetation : IDynamicData
    {
        public Identity Id { get; set; }
        public int ContentTypeId { get; set; }
        public string Markup { get; set; }

        public CustomDocumetation()
        {
            Id = Identity.NewIdentity();
        }
    }
}
