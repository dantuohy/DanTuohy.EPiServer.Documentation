using System.ComponentModel.DataAnnotations;
using Tuohy.Epi.Docs.Interfaces;

namespace Tuohy.Epi.Docs.Business.Attributes
{
    /// <summary>
    /// Regualr expression attribute that extends <see cref="IDocumentable"></see>/>
    /// </summary>
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