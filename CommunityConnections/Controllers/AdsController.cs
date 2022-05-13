using CommunityConnections.Entities;
using CommunityConnections.Services;
using CommunityConnections.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommunityConnections.Controllers
{
    public class AdsController : Controller
    {
        // GET: Ads
        public ActionResult Index(string SearchTerm)
        {
            AdsListingViewModel model = new AdsListingViewModel();
            model.Ads = AdsServices.Instance.GetAdss(SearchTerm);
            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int ID = 0)
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
            if (ID != 0)
            {
                var ad = AdsServices.Instance.GetAds(ID);
                model.ID = ad.ID;
                model.PageNo = ad.PageNo;
                model.Layout = ad.Layout;
                model.AdSize = ad.AdSize;
                model.Path = ad.Path;
                
                return View("Action", model);

            }
            else
            {
                return View("Action", model);
            }
        }



        [HttpPost]
        public ActionResult Action(AdsActionViewModel model)
        {
            
            if (model.ID != 0) //update record
            {
                var ad = AdsServices.Instance.GetAds(model.ID);

                ad.ID = model.ID;
                ad.Layout = model.Layout;
                ad.PageNo = model.PageNo;
                ad.AdSize = model.AdSize;
                ad.Path = model.Path;
              
                AdsServices.Instance.UpdateAds(ad);

            }
            else
            {
                var ad = new Ads();
                ad.Layout = model.Layout;
                ad.PageNo = model.PageNo;
                ad.AdSize = model.AdSize;
                ad.Path = model.Path;
                AdsServices.Instance.SaveAds(ad);
            }
           
                return RedirectToAction("Index", "Ads");

            
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AdsActionViewModel model = new AdsActionViewModel();
            var ad = AdsServices.Instance.GetAds(ID);
            model.ID = ad.ID;
            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(AdsActionViewModel model)
        {

            if (model.ID != 0) //we are trying to delete a record
            {
                var ad = AdsServices.Instance.GetAds(model.ID);
                AdsServices.Instance.DeleteAds(ad.ID);

            }
            return RedirectToAction("Index", "Ads");

        }
    }
}