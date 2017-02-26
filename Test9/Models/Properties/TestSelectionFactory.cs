using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test9.Models.Properties
{
    public class TestSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new List<SelectItem>
           {
               new SelectItem() { Value = "one", Text =  "one" },
               new SelectItem() { Value = "Two", Text =  "Two" }
           };
        }
    }
}