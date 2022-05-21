using CommunityConnections.Services;
using CommunityConnections.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommunityConnections.Controllers
{
    public class MainScreenController : Controller
    {
        // GET: MainScreen
        public ActionResult Index(MainScreenViewModel model)
        {
            model.Ads = AdsServices.Instance.GetNotPlacedAdss();
            model.Pages = PagesServices.Instance.GetPages();
            model.PlacedAds = AdsServices.Instance.GetPlacedAdss();
            model.NonPlacedAds = AdsServices.Instance.GetNotPlacedAdss();
            return View(model);
        }


        [HttpPost]
        public ActionResult PlaceAd(string AdSize, int AdID, int AdPage)
        {
            var Ad = AdsServices.Instance.GetAds(AdID);
            Ad.AdSize = AdSize;
            Ad.PageNo = AdPage;
            Ad.AdStatus = "Placed";
            AdsServices.Instance.UpdateAds(Ad);

            return RedirectToAction("Index", "MainScreen");
            

        }


        [HttpGet]
        public ActionResult SelectLayout(int DataID, int DroppedPage)
        {
            MainScreenViewModel model = new MainScreenViewModel();
            var LayoutList = new List<string>();
            LayoutList.Add("Full Page W’ Bleed");
            LayoutList.Add("Full Page");
            LayoutList.Add("3/4 Page");
            LayoutList.Add("1/2 Page Vertical");
            LayoutList.Add("1/2 Page");
            LayoutList.Add("1/3 Page");
            LayoutList.Add("1/4 Page");
            LayoutList.Add("1/8 Page");
            LayoutList.Add("Full Spread");
            LayoutList.Add("3/4 Spread");
            model.Layouts = LayoutList;
            model.Ad = AdsServices.Instance.GetAds(DataID);
            model.Page = DroppedPage;
            return PartialView(model);
        }
    }
}