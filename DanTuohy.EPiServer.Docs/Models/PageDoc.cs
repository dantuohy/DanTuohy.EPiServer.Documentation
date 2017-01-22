using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanTuohy.EPiServer.Docs.Models
{
    public class PageDoc : ContentTypeDoc
    {
        public Dictionary<string, string> AvaliablePages { get; set; }

        public Dictionary<string, string> Access { get; set; }

    }
}