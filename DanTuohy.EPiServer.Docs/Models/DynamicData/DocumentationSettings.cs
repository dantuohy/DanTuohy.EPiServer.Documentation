using System.IO;
using EPiServer.Core;
using EPiServer.Data;
using EPiServer.Data.Dynamic;
using EPiServer.DynamicContent;

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
    }
}
