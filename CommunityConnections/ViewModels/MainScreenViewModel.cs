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
        public List<Book> Books { get; set; }
        public List<string> StatusList { get; set; }
        public int SelectedPage { get; set; }
        public List<Ads> PlacedAds { get; set; }

        public List<Ads> NonPlacedAds { get; set; }
        public int NoOfPages { get; set; }

        public List<Section> Sections { get; set; }
        public List<MyList> CompleteList { get; set; }

        public Ads Ad { get; set; }
        public Ads NotPlacedAD { get; set; }
        public int Page { get; set; } 

        public List<string> Layouts { get; set; }


        public List<Ads> AdsonPage { get; set; }
        public string SearchTerm { get; set; }


        public int AdID { get; set; }
        public Customer Customer { get; set; }
        public List<MyAdViewList> MyAdsView { get; set; }
    }




    public class MyList
    {
        public Ads Ads { get; set; }
        public List<int> Pages{ get; set; }
    }

    public class MyAdViewList 
    {
        public Ads Ads { get; set; }
        public Customer Customer { get; set; }
    }
}