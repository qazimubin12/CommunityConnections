using CommunityConnections.Entities;
using CommunityConnections.Services;
using CommunityConnections.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
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
                model.Name = ad.Name;
                
                return PartialView("Action", model);

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
                        Ads.PageNo = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                        Ads.Layout = ((Excel.Range)range.Cells[row, 2]).Text;
                        Ads.AdSize = ((Excel.Range)range.Cells[row, 3]).Text;
                        Ads.Path = ((Excel.Range)range.Cells[row, 4]).Text;
                        Ads.Name = ((Excel.Range)range.Cells[row, 5]).Text;
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
                ad.Name = model.Name;
                AdsServices.Instance.UpdateAds(ad);

            }
            else
            {
                var ad = new Ads();
                ad.Layout = model.Layout;
                ad.PageNo = model.PageNo;
                ad.AdSize = model.AdSize;
                ad.Path = model.Path;
                ad.Name = model.Name;
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