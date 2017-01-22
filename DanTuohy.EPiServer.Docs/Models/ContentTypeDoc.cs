using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanTuohy.EPiServer.Docs.Models
{
    public class ContentTypeDoc
    {
        public ContentTypeEnum Type { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Group { get; set; }

        public bool IsAvaliable { get; set; }

        public int SortOrder { get; set; }

        public IEnumerable<PropertyDoc> Properties { get; set; }
    }
}