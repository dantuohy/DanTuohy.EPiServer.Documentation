using EPiServer.Data;
using EPiServer.Data.Dynamic;

namespace Tuohy.Epi.Docs.Models.DynamicData
{
    [EPiServerDataStore(AutomaticallyRemapStore = true)]
    public class CustomDocumentation : IDynamicData
    {
        public Identity Id { get; set; }
        public string ContentTypeId { get; set; }
        public string Markup { get; set; }

        public CustomDocumentation()
        {
            Id = Identity.NewIdentity();
        }
    }
}
