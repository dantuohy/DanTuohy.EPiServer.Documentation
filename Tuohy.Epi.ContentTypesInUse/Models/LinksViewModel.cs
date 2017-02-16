using System.Collections.Generic;

namespace Tuohy.Epi.ContentTypesInUse.Models
{
    public class LinksViewModel
    {
        public string ContentTypeName { get; set; }
        public int ContentTypeId { get; set; }
        public IEnumerable<LinkViewModel> Links { get; set; }
        public int PageNumber { get; set; }
    }

    public class LinkViewModel
    {
      
        public string EditUrl { get; set; }

        public string LinkText { get; set; }

        public LinkViewModel( string editUrl, string linkText)
        {
         
            EditUrl = editUrl;
            LinkText = linkText;
        }
    }
}