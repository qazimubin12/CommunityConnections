using CommunityConnections.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityConnections.ViewModels
{
    public class CPActionViewModel
    {
        public List<Customer> Customers { get; set; }
        public int ID { get; set; }
        public string Customer { get; set; }
        public string AdSize { get; set; }
        public float Price { get; set; }
        public string CustomNotes { get; set; }
        public List<string> AdSizes { get; set; }
    }

    public class CPListingViewModel
    {
        public List<CustomPricings> CustomPricings { get; set; }
        public string SearchTerm { get; set; }
    }
}