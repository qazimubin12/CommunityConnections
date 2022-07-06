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
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public bool Monday { get; set; }
        public string BeforeSection { get; set; }
        public string MoveForward { get; set; }
    }
}