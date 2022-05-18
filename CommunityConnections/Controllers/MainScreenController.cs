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
            model.Ads = AdsServices.Instance.GetAdss();
            model.Pages = PagesServices.Instance.GetPages();
            
            return View(model);
        }
    }
}