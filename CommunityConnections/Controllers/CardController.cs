using CommunityConnections.Services;
using CommunityConnections.Entities;
using CommunityConnections.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommunityConnections.Controllers
{
    public class CardController : Controller
    {
        // GET: Card
        public ActionResult Index(string SearchTerm)
        {
            CardListingViewModel model = new CardListingViewModel();
            model.Cards = CardServices.Instance.GetCards(SearchTerm);
            return View(model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            CardActionViewModel model = new CardActionViewModel();

            if (ID != 0)
            {
                var Card = CardServices.Instance.GetCard(ID);
                model.ID = Card.ID;
                model.Customer = Card.Customer;
                model.CardName = Card.CardName;
                model.CardNumber = Card.CardNumber;
                model.ExpirationDate = Card.ExpirationDate;
                model.SecurityCode = Card.SecurityCode;
                model.ZipCode = Card.ZipCode;
                model.Address = Card.Address;
            

                return PartialView("Action", model);

            }
            else
            {
                return View("Action", model);
            }
        }


        [HttpGet]
        public ActionResult View(int ID = 0)
        {
            CardActionViewModel model = new CardActionViewModel();


            var Card = CardServices.Instance.GetCard(ID);
            model.ID = Card.ID;
            model.Customer = Card.Customer;
            model.CardName = Card.CardName;
            model.CardNumber = Card.CardNumber;
            model.ExpirationDate = Card.ExpirationDate;
            model.SecurityCode = Card.SecurityCode;
            model.ZipCode = Card.ZipCode;
            model.Address = Card.Address;

            return View("View", model);


        }



        [HttpPost]
        public ActionResult Action(CardActionViewModel model)
        {

            if (model.ID != 0) //update record
            {
                var Card = CardServices.Instance.GetCard(model.ID);


                Card.ID = model.ID;
                Card.Customer = model.Customer;
                Card.CardName = model.CardName;
                Card.CardNumber = model.CardNumber;
                Card.ExpirationDate = model.ExpirationDate;
                Card.SecurityCode = model.SecurityCode;
                Card.ZipCode = model.ZipCode;
                Card.Address = model.Address;

                CardServices.Instance.UpdateCard(Card);

            }
            else
            {
                var Card = new Card();
                Card.Customer = model.Customer;
                Card.CardName = model.CardName;
                Card.CardNumber = model.CardNumber;
                Card.ExpirationDate = model.ExpirationDate;
                Card.SecurityCode = model.SecurityCode;
                Card.ZipCode = model.ZipCode;
                Card.Address = model.Address;
                CardServices.Instance.SaveCard(Card);
            }

            return RedirectToAction("Index", "Card");


        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            CardActionViewModel model = new CardActionViewModel();
            var Card = CardServices.Instance.GetCard(ID);
            model.ID = Card.ID;
            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CardActionViewModel model)
        {

            if (model.ID != 0) //we are trying to delete a record
            {
                var Card = CardServices.Instance.GetCard(model.ID);
                CardServices.Instance.DeleteCard(Card.ID);

            }
            return RedirectToAction("Index", "Card");

        }
    }
}