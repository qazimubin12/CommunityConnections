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
        

        //public ActionResult Index(string SearchTerm)
        //{
        //    MainScreenViewModel model = new MainScreenViewModel();

        //    int Pages = 0;
        //    model.Sections = SectionServices.Instance.GetNotTrailingSections();
          
        //    model.NoOfPages = Pages;
        //    model.PlacedAds = AdsServices.Instance.GetPlacedAdss();
        //    if(Session["AdID"] != null)
        //    {
        //        int LastsavedAdID = int.Parse(Session["AdID"].ToString());
        //        model.NotPlacedAD = AdsServices.Instance.GetNotPlacedAd(LastsavedAdID);
        //    }
        //    else
        //    {
        //        model.NonPlacedAds = AdsServices.Instance.GetNotPlacedAdss(SearchTerm);
        //    }
        //    return View(model);
        //}




        [HttpGet]
        public ActionResult Index(int BookID = 0)
        {
         
            MainScreenViewModel model = new MainScreenViewModel();
            model.Books = BookServices.Instance.GetBookss();
            if(BookID != 0)
            {
                Session["Book"] = BookID;
            }
            else
            {
                if (Session["Book"] == null)
                {
                    foreach (var item in model.Books)
                    {
                        BookID = item.ID;
                        break;
                    }
                }
                else
                {
                    BookID = int.Parse(Session["Book"].ToString());
                }
            }
            var Book = BookServices.Instance.GetBooks(BookID);
            model.SelectedBook = Book.BookName;
            model.Sections = SectionServices.Instance.GetNotTrailingSectionsBookName(Book.BookName);
            model.PlacedAds = AdsServices.Instance.GetPlacedAdssViaBookName(Book.BookName);
            model.NonPlacedAds = AdsServices.Instance.GetNotPlacedAdsViaBookName(Book.BookName);
            
            return View(model);
        }



        [HttpPost]
        public ActionResult Index(string Book)
        {

            MainScreenViewModel model = new MainScreenViewModel();
            model.Books = BookServices.Instance.GetBookss();
            model.SelectedBook = Book;
            model.Sections = SectionServices.Instance.GetNotTrailingSectionsBookName(Book);
            model.PlacedAds = AdsServices.Instance.GetPlacedAdssViaBookName(Book);
            model.NonPlacedAds = AdsServices.Instance.GetNotPlacedAdsViaBookName(Book);
            return View(model);
        }


        [HttpPost]
        public ActionResult PlaceAd(int AdID, int AdPage,string Book)
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


            var AdsonPage = AdsServices.Instance.AdsonPage(AdPage,Book);
            var AdsOnPageTwo = AdsServices.Instance.AdsOnPageTwo(AdPage,Book);
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
                    var AdsonPageforSpreadAd = AdsServices.Instance.AdsonPage(AdPageTwo,Book);
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
            //if ()
            //{
                
            //    Ad.PageNo = AdPage;
            //    Ad.AdStatus = "Placed";
            //    string some =  DateTime.Now.ToString("ddMMyyyyhhmmss").ToString();
            //    Ad.Sort = some;
            //    AdsServices.Instance.UpdateAds(Ad);
            //    if(Ad.AdSize == "Full Spread")
            //    {
            //        AdPage++;
            //        Ad.PageTwo = AdPageTwo;
            //        AdsServices.Instance.UpdateAds(Ad);

            //    }

            //}
            #endregion


            #region CheckForAdsOnPage
            int Adscount = 0;
            int MaxAdsCanBePlaced = 8;
            foreach (var item in AdsonPage)
            {
                if(item.AdSize == "1/3 Page")
                {
                    Adscount += 2; //Exceptional Case for three 1/3 Ads
                }
                if(item.AdSize == "1/2 Page" || item.AdSize == "1/2 Page Vertical")
                {
                    Adscount += 4;
                }
                if(item.AdSize == "3/4 Page")
                {
                    Adscount += 6;
                }        
                if(item.AdSize == "1/4 Page" || item.AdSize == "1/4 Page Vertical")
                {
                    Adscount += 2;
                }
                if(item.AdSize == "1/8 Page")
                {
                    Adscount += 1;
                }
            }

            if (Ad.AdSize == "1/3 Page")
            {
                Adscount += 2; 
            }
            if (Ad.AdSize == "1/2 Page" || Ad.AdSize == "1/2 Page Vertical")
            {
                Adscount += 4;
            }
            if (Ad.AdSize == "3/4 Page")
            {
                Adscount += 6;
            }
            if (Ad.AdSize == "1/4 Page" || Ad.AdSize == "1/4 Page Vertical")
            {
                Adscount += 2;
            }
            if (Ad.AdSize == "1/8 Page")
            {
                Adscount += 1;
            }
            if (Adscount <= MaxAdsCanBePlaced && fullpagecheck == false && EvenPageNumberForSpreadAd == false)
            {

                Ad.PageNo = AdPage;
                Ad.AdStatus = "Placed";
                string some = DateTime.Now.ToString("ddMMyyyyhhmmss").ToString();
                Ad.Sort = some;
                AdsServices.Instance.UpdateAds(Ad);
                if (Ad.AdSize == "Full Spread")
                {
                    AdPage++;
                    Ad.PageTwo = AdPageTwo;
                    AdsServices.Instance.UpdateAds(Ad);

                }

            }



            #endregion

            Session["AdID"] = null;
            var book = BookServices.Instance.GetBook(Book);
            Session["Book"] = book.ID;
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
            string Book = Session["Book"].ToString();
            MainScreenViewModel model = new MainScreenViewModel();
            model.AdsonPage = AdsServices.Instance.AdsonPage(ID,Book);
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

        [HttpGet]
        public JsonResult CheckDeluxe(int ID)
        {
            var AD = AdsServices.Instance.GetAds(ID);
            bool deluxe = AD.Deluxe;
            return Json(deluxe, JsonRequestBehavior.AllowGet);
        }



    }
}