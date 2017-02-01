using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tuohy.Epi.Docs.Models.ViewModels
{
    public class DocumentationSettingsViewModel
    {
        [Display(Name = "Provide a URL to a logo")]
        public string Logo { get; set; }

        [Display(Name = "Primary Hex Colour")]
        public string PrimaryColour { get; set; }

        [Display(Name = "Secondary Hex Colour")]
        public string SecondaryColour { get; set; }

        [Display(Name = "Text Colour")]
        public string TextColour { get; set; }
    }
}