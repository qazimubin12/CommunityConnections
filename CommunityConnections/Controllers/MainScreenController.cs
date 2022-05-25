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
        public ActionResult PlaceAd(int AdID, int AdPage)
        {
            var Ad = AdsServices.Instance.GetAds(AdID);
            var AdsonPage = AdsServices.Instance.AdsonPage(AdPage);
            bool fullpagecheck = false;
            foreach (var item in AdsonPage)
            {
                if(item.AdSize == "Full Page" && item.AdStatus == "Placed")
                {
                    fullpagecheck = true;
                    break;
                }
                if(Ad.AdSize == "Full Page" && item.AdStatus == "Placed")
                {
                    fullpagecheck = true;
                    break;
                }
                else
                {
                    fullpagecheck = false;
                }
            }

          
            if (fullpagecheck == false)
            {
                Ad.PageNo = AdPage;
                Ad.AdStatus = "Placed";
                AdsServices.Instance.UpdateAds(Ad);
            }
            return RedirectToAction("Index", "MainScreen");
            

        }

        [HttpPost]
        public ActionResult UndoPlacing(int DataID)
        {
            var Ad = AdsServices.Instance.GetAds(DataID);

            Ad.AdStatus = "Not Placed";
            AdsServices.Instance.UpdateAds(Ad);

            return RedirectToAction("Index", "MainScreen");

        }


        [HttpGet]
        public ActionResult ViewAd(int ID)
        {
            AdsActionViewModel model = new AdsActionViewModel();
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
            var ad = AdsServices.Instance.GetAds(ID);
            model.ID = ad.ID;
            model.PageNo = ad.PageNo;
            model.Layout = ad.Layout;
            model.AdSize = ad.AdSize;
            model.Path = ad.Path;
            model.Name = ad.Name;
            model.AdStatus = ad.AdStatus;
            
            return View("ViewAd", model);


        }



        public void ViewAdInPC(int ID)
        {
            var AD = AdsServices.Instance.GetAds(ID);
            System.Diagnostics.Process.Start(AD.Path);
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


        [HttpGet]
        public ActionResult AdsView(int ID)
        {
            MainScreenViewModel model = new MainScreenViewModel();
            model.AdsonPage = AdsServices.Instance.AdsonPage(ID);
            return PartialView(model);
        }
        
    }
}