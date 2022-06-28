using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommunityConnections.Entities;

namespace CommunityConnections.ViewModels
{
    public class SectionListingViewModel
    {
        public List<Section> Sections { get; set; }
        public string SearchTerm { get; set; }

    }
    public class SectionActionViewModel
    {
        public int ID { get; set; }
        public List<Section> Sections { get; set; }
        public string SectionName { get; set; }
        public int NoOfPages { get; set; }
        public string After { get; set; }
    }
}