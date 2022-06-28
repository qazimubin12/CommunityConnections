using CommunityConnections.Services;
using CommunityConnections.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using CommunityConnections.Entities;
using System.Web;
using System.Web.Mvc;

namespace CommunityConnections.Controllers
{
    public class SectionController : Controller
    {
        // GET: Section
        public ActionResult Index(string SearchTerm)
        {
            SectionListingViewModel model = new SectionListingViewModel();
            model.Sections = SectionServices.Instance.GetSectionss(SearchTerm);
            return View(model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            SectionActionViewModel model = new SectionActionViewModel();
            model.Sections = SectionServices.Instance.GetSectionss();
            if (ID != 0)
            {
                var section = SectionServices.Instance.GetSections(ID);
                model.ID = section.ID;
                model.StartPage = section.StartPage;
                model.EndPage = section.EndPage;
                model.SectionName = section.SectionName;
                return PartialView("Action", model);

            }
            else
            {
                return View("Action", model);
            }
        }


        [HttpPost]
        public ActionResult Action(SectionActionViewModel model)
        {
            model.Sections = SectionServices.Instance.GetSectionss();
            if (model.ID != 0) //update record
            {
                var section = SectionServices.Instance.GetSections(model.ID);

                section.ID = model.ID;
                section.StartPage = model.StartPage;
                section.EndPage = model.EndPage;
                
                section.SectionName = model.SectionName;
          

                SectionServices.Instance.UpdateSections(section);

            }
            else
            {
                var section = new Section();
                section.StartPage = model.StartPage;
                section.EndPage = model.EndPage;
                section.SectionName = model.SectionName;

                SectionServices.Instance.SaveSections(section);
            }

            return RedirectToAction("Index", "Section");


        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            SectionActionViewModel model = new SectionActionViewModel();
            var Section = SectionServices.Instance.GetSections(ID);
            model.ID = Section.ID;
            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(SectionActionViewModel model)
        {

            if (model.ID != 0) //we are trying to delete a record
            {
                SectionServices.Instance.DeleteSections(model.ID);

            }
            return RedirectToAction("Index", "Sections");

        }
    }
}