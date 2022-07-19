using CommunityConnections.Entities;
using CommunityConnections.Services;
using CommunityConnections.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
namespace CommunityConnections.Controllers
{
    public class AdsController : Controller
    {
        // GET: Ads
        public ActionResult Index(string SearchTerm ="")
        {
            AdsListingViewModel model = new AdsListingViewModel();
            model.Ads = AdsServices.Instance.GetAdss(SearchTerm);
            model.Customers = CustomerServices.Instance.GetCustomers();
            #region AdSizesList
            var LayoutList = new List<string>();
            LayoutList.Add("Full Page W’ Bleed");
            LayoutList.Add("Full Page");
            LayoutList.Add("3/4 Page");
            LayoutList.Add("1/2 Page Vertical");
            LayoutList.Add("1/2 Page");
            LayoutList.Add("1/3 Page");
            LayoutList.Add("1/4 Page");
            LayoutList.Add("1/4 Page Vertical");
            LayoutList.Add("1/8 Page");
            LayoutList.Add("Full Spread");
            LayoutList.Add("3/4 Spread");
            model.Layouts = LayoutList;
            #endregion
            #region StatusList
            var StatusList = new List<string>();
            StatusList.Add("Running");
            StatusList.Add("Email");
            StatusList.Add("Call");
            model.StatusList = StatusList;
            #endregion




            return View(model);
        }

        [HttpGet]
        public ActionResult ActionPartial(int ID = 0)
        {
            AdsListingViewModel model = new AdsListingViewModel();
            model.Ads = AdsServices.Instance.GetAdss();
            model.Customers = CustomerServices.Instance.GetCustomers();
            var Ad = AdsServices.Instance.GetAds(ID);
            #region AdSizesList
            var LayoutList = new List<string>();
            LayoutList.Add("Full Page W’ Bleed");
            LayoutList.Add("Full Page");
            LayoutList.Add("3/4 Page");
            LayoutList.Add("1/2 Page Vertical");
            LayoutList.Add("1/2 Page");
            LayoutList.Add("1/3 Page");
            LayoutList.Add("1/4 Page");
            LayoutList.Add("1/4 Page Vertical");
            LayoutList.Add("1/8 Page");
            LayoutList.Add("Full Spread");
            LayoutList.Add("3/4 Spread");
            model.Layouts = LayoutList;
            #endregion
            #region StatusList
            var StatusList = new List<string>();
            StatusList.Add("Running");
            StatusList.Add("Email");
            StatusList.Add("Call");
            model.StatusList = StatusList;
            #endregion

            if (ID != 0)
            {
                model.ID = Ad.ID;
                model.PageNo = Ad.PageNo;
                model.AdSize = Ad.AdSize;
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +Request.ApplicationPath.TrimEnd('/') + "/";
                if (Ad.Path.Contains(baseUrl))
                {
                    model.Path = Ad.Path;
                }
                else
                {
                    model.Path = baseUrl + Ad.Path;
                }
                model.Name = Ad.Name;
                model.AdDescription = Ad.AdDescription;
                model.AdStatus = Ad.AdStatus;
                model.PageTwo = Ad.PageTwo;
                model.Status = Ad.Status;
                
                model.Discount = Ad.Discount;
                model.AdDate = Ad.AdDate;
                model.AddGraphics = Ad.AddGraphics;
                model.Book = Ad.Book;
                model.Customer = Ad.Customer;
                model.Repeat = Ad.Repeat;
                model.Status = Ad.Status;
                model.CustomSpecification = Ad.CustomSpecification;
                model.Total = Ad.Total;
            }
            return PartialView(model);
        }

        public ActionResult GetCustomers()
        {
            AdsListingViewModel model = new AdsListingViewModel();



            StringBuilder sb = new StringBuilder();
            model.Customers = CustomerServices.Instance.GetCustomers();
            int Count = 0;
            foreach (var item in model.Customers)
            {
                Count++;
                string name = item.FirstName + " " + item.MiddleName + " " + item.LastName;
               
                if (model.Customers.Count == Count)
                {
                    sb.Append(string.Format("\"{0}\":\"{1}\"", name, name));

                }
                else
                {
                    sb.Append(string.Format("\"{0}\":\"{1}\",", name, name));
                }


            }
   

            return Content("{" + sb.ToString() + "}");
        }



        public ActionResult GetAdsOnPage(int PageNo, string AdSize, string Path)
        {

            AdsListingViewModel model = new AdsListingViewModel();
            var AdsOnPage = AdsServices.Instance.AdsonPage(PageNo);
            List<Ads> TotalAdsOnPageOne = new List<Ads>();
            List<Ads> TotalAdsOnPageTwo = new List<Ads>();

            foreach (var item in AdsOnPage)
            {
                TotalAdsOnPageOne.Add(item);
            }
            model.PageOne = PageNo;
            PageNo++;
            model.Page_Two = PageNo;
            var AdsOnPagetwo = AdsServices.Instance.AdsonPage(PageNo);
            foreach (var item in AdsOnPagetwo)
            {
                TotalAdsOnPageTwo.Add(item);
            }

            if (Path != "")
            {
                var Ad = new Ads();
                if (Path.Contains("Content/paths/"))
                {
                    Ad.Path = Path;

                }
                else
                {
                    Ad.Path = "Content/paths/" + Path;
                }
                Ad.AdSize = AdSize;
                Ad.PageNo = PageNo;
                string some = DateTime.Now.ToString("ddMMyyyyhhmmss").ToString();
                Ad.Sort = some;
                TotalAdsOnPageOne.Add(Ad);
            } 
            model.AdsOnPageOne = TotalAdsOnPageOne;
            model.AdsOnPageTwo = TotalAdsOnPageTwo;
            model.Path = Path;
            return PartialView("MainScreenPartial",model);
        }





        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            AdsListingViewModel model = new AdsListingViewModel();
            var LayoutList = new List<string>();
            LayoutList.Add("Full Page W’ Bleed");
            LayoutList.Add("Full Page");
            LayoutList.Add("3/4 Page");
            LayoutList.Add("1/2 Page Vertical");
            LayoutList.Add("1/2 Page");
            LayoutList.Add("1/3 Page");
            LayoutList.Add("1/4 Page");
            LayoutList.Add("1/4 Page Vertical");
            LayoutList.Add("1/8 Page");
            LayoutList.Add("Full Spread");
            LayoutList.Add("3/4 Spread");

            var StatusList = new List<string>();
            StatusList.Add("Running");
            StatusList.Add("Email");
            StatusList.Add("Call");
            model.StatusList = StatusList;



            
            model.Customers = CustomerServices.Instance.GetCustomers();
            if (ID != 0)
            {
                var ad = AdsServices.Instance.GetAds(ID);
                model.ID = ad.ID;
                model.PageNo = ad.PageNo;
                model.AdSize = ad.AdSize;
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                model.Path = baseUrl + ad.Path;
                model.Name = ad.Name;
                model.AdStatus = ad.AdStatus;
                model.PageTwo = ad.PageTwo;
                model.Status = ad.Status;
                return View("Action", model);

            }
            else
            {
                return View("Action", model);
            }
        }





        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Please Select Excel File";
                    return View();
            }
            else
            {
                bool isPresent = false;
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/" + excelfile.FileName);
                    if(System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    excelfile.SaveAs(path);

                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;
                    List<Ads> list = new List<Ads>();
                    for (int row = 2; row < range.Rows.Count; row++)
                    {
                        var Ads = new Ads();
                        Ads.AdSize = ((Excel.Range)range.Cells[row, 3]).Text;
                        
                        Ads.PageNo = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                        Ads.Path = ((Excel.Range)range.Cells[row, 4]).Text;
                        Ads.Name = ((Excel.Range)range.Cells[row, 5]).Text;
                        Ads.AdStatus = "Not Placed";
                        var List = AdsServices.Instance.GetAdss();
                       
                        if(List.Count != 0) {
                            foreach (var item in List)
                            {
                                if (item.Name == Ads.Name && item.PageNo == Ads.PageNo)
                                {
                                    isPresent = true;
                                    break;
                                }
                                else
                                {
                                    isPresent = false;
                                }

                                


                            }
                            if (isPresent == false)
                            {
                                list.Add(Ads);
                                AdsServices.Instance.SaveAds(Ads);
                            }


                        }
                        else
                        {
                            list.Add(Ads);
                            AdsServices.Instance.SaveAds(Ads);
                        }

                    }
                    ViewBag.ListAds = list;
                    return View();
                }
                else
                {
                    ViewBag.Error = "Incorrect File";

                    return View();
                }
            }
        }






        [HttpPost]
        public ActionResult Action(AdsListingViewModel model, string Path)
        {

            if (model.ID != 0) //update record
            {
                var ad = AdsServices.Instance.GetAds(model.ID);

                ad.ID = model.ID;
                if (ad.Name.Contains("AD"))
                {
                    ad.Name =  model.Name;

                }
                else
                {
                    ad.Name = "AD" + DateTime.Now.ToString("hh:mm") + model.Name;

                }
                ad.PageNo = model.PageNo;
                ad.AdSize = model.AdSize;
                ad.Status = model.Status;
                if (model.Path.Contains("Content/paths/"))
                {
                    ad.Path = model.Path;
                }
                else
                {
                    ad.Path = "Content/paths/" + model.Path;
                }

                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                if (model.Path.Contains(baseUrl))
                {
                    ad.Path = model.Path;
                }
                else
                {
                    ad.Path = baseUrl + model.Path;
                }
                ad.AdStatus = "Not Placed";
                if (ad.AdSize == "Full Spread" || ad.AdSize == "3/4 Spread")
                {
                    ad.PageTwo = ad.PageNo + 1;
                }
                ad.AdDate = model.AdDate;
                ad.AddGraphics = model.AddGraphics;
                ad.Book = model.Book;
                ad.Repeat = model.Repeat;
                ad.Customer = model.Customer;
                ad.AdStatus = model.AdStatus;
                ad.CustomSpecification = model.CustomSpecification;
                if (model.Deluxe.ToString() == "on")
                {
                    ad.Deluxe = true;
                }
                else
                {
                    ad.Deluxe = false;
                }
                ad.Discount = model.Discount;
                ad.Total = model.Total;

                AdsServices.Instance.UpdateAds(ad);

            }
            else
            {
                var ad = new Ads();
                ad.PageNo = model.PageNo;
                ad.AdSize = model.AdSize;
                ad.Status = model.Status;
                ad.AdStatus = model.AdStatus;
                ad.Name = "AD" + DateTime.Now.ToString("hh:mm") + model.Path;

                if (model.Path.Contains("Content/paths/"))
                {
                    ad.Path = model.Path;
                }
                else
                {
                    ad.Path = "Content/paths/" + model.Path;
                }

                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                if (model.Path.Contains(baseUrl))
                {
                    ad.Path = model.Path;
                }
                else
                {
                    ad.Path = baseUrl + model.Path;
                }
                ad.AdStatus = "Not Placed";
                if (ad.AdSize == "Full Spread" || ad.AdSize == "3/4 Spread")
                {
                    ad.PageTwo = ad.PageNo + 1;
                }
                ad.AdDate = model.AdDate;
                ad.AddGraphics = model.AddGraphics;
                ad.Book = model.Book;
                ad.Customer = model.Customer;
                ad.AdDescription = model.AdDescription;
                ad.CustomSpecification = model.CustomSpecification;
                if (model.Deluxe.ToString() == "on")
                {
                    ad.Deluxe = true;
                }
                else
                {
                    ad.Deluxe = false;
                }
                ad.Discount = model.Discount;
                ad.Total = model.Total;
                ad.Repeat = model.Repeat;
                AdsServices.Instance.SaveAds(ad);
            }

            return RedirectToAction("Index", "Ads");


        }


        public ActionResult LoadAdSizes(int id)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                var LayoutList = new List<Tuple<string,string>>();
                Dictionary<string, string> mydict = new Dictionary<string, string>();
                mydict.Add("Full Page W’ Bleed", "Full Page W’ Bleed");
                mydict.Add("Full Page", "Full Page");
                mydict.Add("3/4 Page", "3/4 Page");
                mydict.Add("1/2 Page Vertical", "1/2 Page Vertical");
                mydict.Add("1/2 Page", "1/2 Page");
                mydict.Add("1/3 Page", "1/3 Page");
                mydict.Add("1/4 Page", "1/4 Page");
                mydict.Add("1/4 Page Vertical", "1/4 Page Vertical");
                mydict.Add("1/8 Page", "1/8 Page");
                mydict.Add("Full Spread", "Full Spread");
                mydict.Add("3/4 Spread", "3/4 Spread");
                
                foreach (var item in mydict)
                {
                    sb.Append(string.Format("{0}':'{1}'", item.Key, item.Value));
                }
                return Content("{" + sb.ToString() + "}");
            }
            catch (Exception ex)
            {

                throw;
            }

            //{ 'Full Page W’ Bleed':'Full Page W’ Bleed''Full Page':'Full Page''3/4 Page':'3/4 Page''1/2 Page Vertical':'1/2 Page Vertical''1/2 Page':'1/2 Page''1/3 Page':'1/3 Page''1/4 Page':'1/4 Page''1/4 Page Vertical':'1/4 Page Vertical''1/8 Page':'1/8 Page''Full Spread':'Full Spread''3/4 Spread':'3/4 Spread'}
        }

        [HttpPost]
        public ActionResult NewAction(int id, string propertyname,string value)
        {
            var status = false;
            var message = "";
    
            var Ad = AdsServices.Instance.GetAds(id);
            

            if (Ad != null)
            {
                

                if (propertyname == "Status")
                {
                    Ad.Status = value;
                }

                       
                if (propertyname == "AdStatus")
                {
                    Ad.AdStatus = value;
                }
                
                

                AdsServices.Instance.UpdateAds(Ad);
                status = true;
            }
            else
            {
                message = "error!";
            }


            var response = new { value = value, status = status, message = message };
            JObject o = JObject.FromObject(response);
            return Content(o.ToString());
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AdsListingViewModel model = new AdsListingViewModel();
            var ad = AdsServices.Instance.GetAds(ID);
            model.ID = ad.ID;
            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(AdsListingViewModel model)
        {

            if (model.ID != 0) //we are trying to delete a record
            {
                var ad = AdsServices.Instance.GetAds(model.ID);
                AdsServices.Instance.DeleteAds(ad.ID);

            }
            return RedirectToAction("Index", "Ads");

        }



        [HttpGet]
        public JsonResult GetLocationOfAd(string path)
        {
            string location = "Content/paths/" + path;
            return Json(location, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult GetPathOfAd(int ID)
        {
            var Ad = AdsServices.Instance.GetAds(ID);
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            string path = baseUrl + Ad.Path;
            return Json(path, JsonRequestBehavior.AllowGet);
        }



    }
}