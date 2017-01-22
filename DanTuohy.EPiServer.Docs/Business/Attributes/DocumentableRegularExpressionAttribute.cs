using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DanTuohy.EPiServer.Docs.Interfaces;

namespace Alloy.Documentation.Business.Attributes
{
    public class DocumentableRegularExpressionAttribute : RegularExpressionAttribute, IDocumentable
    {
        public DocumentableRegularExpressionAttribute(string pattern, string name, string value) : base(pattern)
        {
            Name = name;
            Value = value;   
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}