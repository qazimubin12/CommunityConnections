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
        

        public ActionResult Index(string SearchTerm)
        {
            MainScreenViewModel model = new MainScreenViewModel();

            int Pages = 0;
            model.Sections = SectionServices.Instance.GetNotTrailingSections();
          
            model.NoOfPages = Pages;
            model.PlacedAds = AdsServices.Instance.GetPlacedAdss();
            if(Session["AdID"] != null)
            {
                int LastsavedAdID = int.Parse(Session["AdID"].ToString());
                model.NotPlacedAD = AdsServices.Instance.GetNotPlacedAd(LastsavedAdID);
            }
            else
            {
                model.NonPlacedAds = AdsServices.Instance.GetNotPlacedAdss(SearchTerm);
            }
            return View(model);
        }

        public ActionResult Index2(string SearchTerm)
        {
            MainScreenViewModel model = new MainScreenViewModel();

            int Pages = 0;
            model.Sections = SectionServices.Instance.GetNotTrailingSections();

            model.NoOfPages = Pages;
            model.PlacedAds = AdsServices.Instance.GetPlacedAdss();
            model.NonPlacedAds = AdsServices.Instance.GetNotPlacedAdss(SearchTerm);
            return View(model);
        }


        [HttpPost]
        public ActionResult PlaceAd(int AdID, int AdPage)
        {
            var Ad = AdsServices.Instance.GetAds(AdID);


            //When Spread Ad Get to Even Number Page
            if (Ad.AdSize == "3/4 Spread" || Ad.AdSize == "Full Spread")
            {
                if(AdPage % 2 == 0)
                {
                    AdPage--;
                }
            }


            var AdsonPage = AdsServices.Instance.AdsonPage(AdPage);
            var AdsOnPageTwo = AdsServices.Instance.AdsOnPageTwo(AdPage);
            bool fullpagecheck = false;
            bool EvenPageNumberForSpreadAd = false;
            int AdPageTwo = 0;



            #region CheckForFullSpreadAndBleedADs

            foreach (var item in AdsOnPageTwo)
            {

                if (item.AdStatus == "Placed")
                {
                    if (item.AdSize == "Full Spread" || item.AdSize == "Full Page" || item.AdSize == "Full Page W’ Bleed")
                    {
                        fullpagecheck = true;
                        ViewBag.Invalid = "Invalid Position";

                        break;
                    }

                }

                else
                {
                    fullpagecheck = false;
                }
            } //when any ad is present on page


            foreach (var item in AdsonPage)
            {
              
                if(item.AdStatus == "Placed" )
                {
                    if (Ad.AdSize == "Full Spread" || Ad.AdSize == "Full Page" || Ad.AdSize == "Full Page W’ Bleed")
                    {
                        fullpagecheck = true;
                        break;
                    }
                    
                }
                
                else
                {
                    fullpagecheck = false;
                }
            } //when any ad is present on page


            if(Ad.AdSize == "Full Page" || Ad.AdSize == "Full Page W’ Bleed" || Ad.AdSize == "Full Spread")
            {
                if(AdsonPage.Count > 0)
                {
                    fullpagecheck = true;
                    ViewBag.Invalid = "Invalid Position";

                }
                else
                {
                    fullpagecheck = false;
                }
            }
          
            if(Ad.AdSize == "3/4 Spread" || Ad.AdSize == "Full Spread")
            {

                if (AdPage % 2 == 0)
                {
                    EvenPageNumberForSpreadAd = true;
                    ViewBag.Invalid = "Invalid Position";

                }
                else
                {
                    EvenPageNumberForSpreadAd = false;
                }

                if(EvenPageNumberForSpreadAd == false)
                {
                     AdPageTwo = AdPage;
                    AdPageTwo++;
                    var AdsonPageforSpreadAd = AdsServices.Instance.AdsonPage(AdPageTwo);
                    foreach (var item in AdsonPageforSpreadAd)
                    {

                        if (item.AdStatus == "Placed")
                        {
                            if (Ad.AdSize == "Full Spread" || Ad.AdSize == "Full Page" || Ad.AdSize == "Full Page W’ Bleed")
                            {
                                fullpagecheck = true;
                                ViewBag.Invalid = "Invalid Position";
                                break;
                            }

                        }

                        else
                        {
                            fullpagecheck = false;
                        }
                    } //when any ad is present on page after the page is present


                }
            }
            if (fullpagecheck == false && EvenPageNumberForSpreadAd == false)
            {
                
                Ad.PageNo = AdPage;
                Ad.AdStatus = "Placed";
                string some =  DateTime.Now.ToString("ddMMyyyyhhmmss").ToString();
                Ad.Sort = some;
                AdsServices.Instance.UpdateAds(Ad);
                if(Ad.AdSize == "Full Spread")
                {
                    AdPage++;
                    Ad.PageTwo = AdPageTwo;
                    AdsServices.Instance.UpdateAds(Ad);

                }

            }
            #endregion


            #region CheckForAdsOnPage
            foreach (var item in AdsonPage)
            {
               
                

            }


            #endregion

            Session["AdID"] = null;
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
            model.SelectedPage = ID;
            var StatusList = new List<string>();
            StatusList.Add("Running");
            StatusList.Add("Email");
            StatusList.Add("Call");
            model.StatusList = StatusList;

            var List = new List<MyAdViewList>();
            foreach (var item in model.AdsonPage)
            {
               
                var Customer = CustomerServices.Instance.GetCustomersViaName(item.Customer);
                List.Add(new MyAdViewList { Ads = item, Customer = Customer });
            }
            model.MyAdsView = List;


            return PartialView(model);
        }



        [HttpPost]
        public ActionResult UpdateStatus(int PageID,int ID, string Status)
        {

            var Ad = AdsServices.Instance.GetAds(ID);
            Ad.Status = Status;
            AdsServices.Instance.UpdateAds(Ad);


            return RedirectToAction("Index", "MainScreen");
        }



        [HttpGet]
        public JsonResult GetAdStatus(int ID)
        {
            var AD = AdsServices.Instance.GetAds(ID);
            string status = AD.Status;
            return Json(status,JsonRequestBehavior.AllowGet);
        }



    }
}