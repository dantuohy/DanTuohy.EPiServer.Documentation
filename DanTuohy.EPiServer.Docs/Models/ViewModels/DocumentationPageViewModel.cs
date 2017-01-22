using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanTuohy.EPiServer.Docs.ViewModels
{
    public class DocumentationPageViewModel
    {
        public IDictionary<int, string> Pages { get; set; }
        public IDictionary<int, string> Blocks { get; set; }
        public IDictionary<int, string> Media { get; set; }
    }
}