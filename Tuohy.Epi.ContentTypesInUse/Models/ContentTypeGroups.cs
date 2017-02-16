using System.Collections.Generic;

namespace Tuohy.Epi.ContentTypesInUse.Models
{
    public class ContentTypeGroups
    {
        public IEnumerable<ContentTypeCountsViewModel> Blocks { get; set; }
        public IEnumerable<ContentTypeCountsViewModel> Pages { get; set; }
    }
}