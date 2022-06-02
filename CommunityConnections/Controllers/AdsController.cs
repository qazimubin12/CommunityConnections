using CommunityConnections.Entities;
using CommunityConnections.Services;
using CommunityConnections.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
            LayoutList.Add("1/4 Page Vertical");
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
                model.AdStatus = ad.AdStatus;
                model.PageTwo = ad.PageTwo;
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
                        Ads.AdSize = ((Excel.Range)range.Cells[row, 3]).Text;
                        
                        Ads.PageNo = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                        Ads.Layout = ((Excel.Range)range.Cells[row, 2]).Text;
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






        //[HttpPost]
        //public ActionResult Action(IEnumerable<HttpPostedFileBase> path,int ID, string AdSize,string Name, int PageNo,int PageTwo, string Layout)
        //{

        //    var supportedPicTypes = new[] { "jpg", "jpeg", "png" };
        //    var supportedPdfTypes = new[] { "pdf", "doc", "docx" };
        //    var PicFileSize = 200000000;
        //    var PathSize = 100000000000;
        //    if (ID != 0) //update record
        //    {
        //        var ad = AdsServices.Instance.GetAds(ID);

        //        ad.ID = ID;
        //        ad.Layout = Layout;
        //        ad.PageNo = PageNo;
        //        ad.AdSize = AdSize;
        //        ad.PageTwo = PageTwo;
        //        ad.Name = Name;
        //        ad.AdStatus = "Not Placed";

        //        if (path.Count() != 0)
        //        {
        //            if (path.ElementAt(0) != null)
        //            {
        //                if (path.ElementAt(0).ContentLength > (PathSize))
        //                {
        //                    ViewBag.Message = "File Size should be less than 10mb";
        //                }
        //                else if (!supportedPdfTypes.Contains(System.IO.Path.GetExtension(path.ElementAt(0).FileName).Substring(1)))
        //                {
        //                    ViewBag.Message = "File Extension is not valid";
        //                }
        //                else
        //                {
        //                    ViewBag.Message = "Record Already Exist!";

        //                    string Upth = Path.Combine(Server.MapPath("~/paths"), Path.GetFileName(path.ElementAt(0).FileName));
        //                    path.ElementAt(0).SaveAs(Upth);
        //                }
        //                string FilePath = path.ElementAt(0).FileName;
        //                ad.Path = "~/paths/" + path.ElementAt(0).FileName;
        //            }
        //        }
        //        else
        //        {
        //            ad.Path = ad.Path;
        //        }
        //        AdsServices.Instance.UpdateAds(ad);

        //    }
        //    else
        //    {
        //        var ad = new Ads();
        //        ad.Layout = Layout;
        //        ad.PageNo = PageNo;
        //        ad.AdSize = AdSize;
        //        ad.PageTwo = PageTwo;

        //        if (path.Count() != 0)
        //        {
        //            if (path.ElementAt(0) != null)
        //            {
        //                if (path.ElementAt(0).ContentLength > (PathSize))
        //                {
        //                    ViewBag.Message = "File Size should be less than 10mb";
        //                }
        //                else if (!supportedPdfTypes.Contains(System.IO.Path.GetExtension(path.ElementAt(0).FileName).Substring(1)))
        //                {
        //                    ViewBag.Message = "File Extension is not valid";
        //                }
        //                else
        //                {
        //                    ViewBag.Message = "Record Already Exist!";

        //                    string Upth = Path.Combine(Server.MapPath("~/paths"), Path.GetFileName(path.ElementAt(0).FileName));
        //                    path.ElementAt(0).SaveAs(Upth);
        //                }

        //                ad.Path = "~/paths/" + path.ElementAt(0).FileName;
        //            }
        //        }
        //        else
        //        {
        //            ad.Path = null;
        //        }
        //        ad.Name = Name;
        //        ad.AdStatus = "Not Placed";

        //        AdsServices.Instance.SaveAds(ad);
        //    }

        //        return RedirectToAction("Index", "Ads");


        //}

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
                ad.Path = "Content/paths/" + model.Path;
                ad.Name = model.Name;
                ad.AdStatus = model.AdStatus;
                ad.PageTwo = model.PageTwo;

                AdsServices.Instance.UpdateAds(ad);

            }
            else
            {
                var ad = new Ads();
                ad.Layout = model.Layout;
                ad.PageNo = model.PageNo;
                ad.AdSize = model.AdSize;
                ad.PageTwo = model.PageTwo;

                ad.Path = "Content/paths/" + model.Path;
                ad.Name = model.Name;
                ad.AdStatus = model.AdStatus;

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