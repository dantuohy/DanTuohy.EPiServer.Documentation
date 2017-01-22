using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.DataAbstraction;

namespace DanTuohy.EPiServer.Docs.Models
{
    public class PropertyDoc
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; }
        public bool IsRequired { get; set; }
        public bool IsSearchable { get; set; }
        public string Tab { get; set; }
        public int SortOrder { get; set; }
        public IDictionary<string, string> CustomAttributes { get; set; }

        public IDictionary<string, string> AllowedTypes { get; set; }
    }
}