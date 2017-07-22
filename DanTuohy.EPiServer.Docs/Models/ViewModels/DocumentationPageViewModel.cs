using System;
using System.Collections.Generic;
using System.Linq;

namespace Tuohy.Epi.Docs.Models.ViewModels
{
    public abstract class DocumentationPageViewModel
    {
        public IDictionary<int, NameGroupPair> Pages { get; set; }
        public IDictionary<int, NameGroupPair> Blocks { get; set; }
        public IDictionary<int, NameGroupPair> Media { get; set; }
        public IDictionary<Guid, string> Jobs { get; set; }
        public PageDesign PageDesign { get; set; }
        public ContentTypeEnum FocusedContentTypeType { get; set; }
        public string FocusedContentTypeId { get; set; }
        public string FocusedContentTypeGroup { get; set; }
        public bool CanEdit { get; set; }
        public bool IsEditMode { get; set; }
        public bool DisplayAsGroups { get; set; }
        public string CustomDocumentation { get; set; }
        public bool HideNavigation { get; set; }
        public IEnumerable<IGrouping<string, KeyValuePair<int, NameGroupPair>>> Grouped
        {
            get { return Pages.Concat(Blocks).OrderBy(x => x.Value.GroupName).GroupBy(x => x.Value.GroupName); }
        }
    }

    public class NameGroupPair
    {
        public string ContentTypeName { get; set; }
        public string GroupName { get; set; }

    }

    public class PageDesign
    {
        public string LogoUrl { get; set; }
        public string PriamryColour { get; set; }
        public string SecondaryColour { get; set; }
        public string TextColour { get; set; }
    }
}