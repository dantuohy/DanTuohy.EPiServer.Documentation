using System.Collections.Generic;

namespace DanTuohy.EPiServer.Docs.Models.ViewModels
{
    public class DocumentationPageViewModel
    {
        public IDictionary<int, string> Pages { get; set; }
        public IDictionary<int, string> Blocks { get; set; }
        public IDictionary<int, string> Media { get; set; }
    }
}