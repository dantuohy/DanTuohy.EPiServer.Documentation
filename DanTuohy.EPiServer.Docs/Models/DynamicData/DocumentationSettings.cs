using EPiServer.Data;
using EPiServer.Data.Dynamic;

namespace Tuohy.Epi.Docs.Models.DynamicData
{
    [EPiServerDataStore(AutomaticallyRemapStore = true)]
    public class DocumentationSettings : IDynamicData
    {
        public Identity Id { get; set; }
        public string LogoUrl { get; set; }
        public string PriamryColour { get; set; }
        public string SecondaryColour { get; set; }
        public string TextColour { get; set; }
        public bool IncludeJobs { get; set; }
        public bool AllowEditing { get; set; }

        public DocumentationSettings()
        {
            Id = Identity.NewIdentity();
        }
    }
}
