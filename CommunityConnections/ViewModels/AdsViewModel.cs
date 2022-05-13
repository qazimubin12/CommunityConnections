using CommunityConnections.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityConnections.ViewModels
{
    public class AdsListingViewModel
    {
        public List<Ads> Ads { get; set; }
        public string SearchTerm { get; set; }
    }
    public class AdsActionViewModel
    {
        public int ID { get; set; }
        public int PageNo { get; set; }
        public string Layout { get; set; }
        public string AdSize { get; set; }
        public string Path { get; set; }
        public List<string> Layouts { get; set; }
    }
}