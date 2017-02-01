using System;
using System.Collections.Generic;

namespace Tuohy.Epi.Docs.Models.ViewModels
{
    public class DocumentationPageViewModel
    {
        public IDictionary<int, string> Pages { get; set; }
        public IDictionary<int, string> Blocks { get; set; }
        public IDictionary<int, string> Media { get; set; }
        public IDictionary<Guid, string> Jobs { get; set; }
        public PageDesign PageDesign { get; set; }
    }

    public class PageDesign
    {
        public string LogoUrl { get; set; }
        public string PriamryColour { get; set; }
        public string SecondaryColour { get; set; }
        public string TextColour { get; set; }
    }
}