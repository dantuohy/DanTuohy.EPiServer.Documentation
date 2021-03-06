﻿using System.ComponentModel.DataAnnotations;

namespace Tuohy.Epi.Docs.Models.ViewModels
{
    public class DocumentationSettingsViewModel
    {
        [Display(Name = "Provide a URL to a logo?")]
        public string Logo { get; set; }

        [Display(Name = "Primary Hex Colour?")]
        public string PrimaryColour { get; set; }

        [Display(Name = "Secondary Hex Colour?")]
        public string SecondaryColour { get; set; }

        [Display(Name = "Text Colour?")]
        public string TextColour { get; set; }

        [Display(Name = "Include Scheduled Jobs?")]
        public bool IncludeJobs { get; set; }

        [Display(Name = "Allow Custom Editing?")]
        public bool AllowEditing { get; set; }

        [Display(Name = "Display Content Types Grouped?")]
        public bool DisplayAsGroups { get; set; }
    }
}