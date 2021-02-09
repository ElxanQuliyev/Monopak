using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.Areas.monopakadmin.ViewModels
{
    public class PageViewModel
    {
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string PageURL { get; set; }
        public string PageImageURL { get; set; }

        public List<string> PageCanonicalURLs { get; set; } // = new List<string>();

        public PageViewModel()
        {
            PageCanonicalURLs = new List<string>();
        }

    }
}