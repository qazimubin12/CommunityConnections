using CommunityConnections.Services;
using CommunityConnections.ViewModels;
using CommunityConnections.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommunityConnections.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index(string SearchTerm = "")
        {
            BooksListingViewModel model = new BooksListingViewModel();
            model.Books = BookServices.Instance.GetBookss(SearchTerm);
            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            BooksActionViewModel model = new BooksActionViewModel();
            
            if (ID != 0)
            {
                var Book = BookServices.Instance.GetBooks(ID);
                model.ID = Book.ID;
                model.AdID = Book.AdID;
                model.BookName = Book.BookName;

                return PartialView("Action", model);

            }
            else
            {
                return View("Action", model);
            }
        }


        [HttpPost]
        public ActionResult Action(BooksActionViewModel model)
        {
            if (model.ID != 0) //update record
            {
                var Book = BookServices.Instance.GetBooks(model.ID);

                Book.ID = model.ID;
                Book.AdID = model.AdID;
                Book.BookName = model.BookName;
             
                BookServices.Instance.UpdateBooks(Book);

            }
            else
            {
                var Book = new Book();
                Book.AdID = model.AdID;
                Book.BookName = model.BookName;
              
                BookServices.Instance.SaveBooks(Book);
            }

            return RedirectToAction("Index", "Book");


        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            BooksActionViewModel model = new BooksActionViewModel();
            var Book = BookServices.Instance.GetBooks(ID);
            model.ID = Book.ID;
            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(BooksActionViewModel model)
        {

            if (model.ID != 0) //we are trying to delete a record
            {
                BookServices.Instance.DeleteBooks(model.ID);

            }
            return RedirectToAction("Index", "Book");

        }
    }
}