using CommunityConnections.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityConnections.ViewModels
{
    public class MainScreenViewModel
    {
        public List<Ads> Ads { get; set; }
        public Pages Pages { get; set; }
        public List<MyList> CompleteList { get; set; }
    }

    public class MyList
    {
        public Ads Ads { get; set; }
        public List<int> Pages{ get; set; }
    }
}