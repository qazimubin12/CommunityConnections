using CommunityConnections.Entities;
using CommunityConnections.Services;
using CommunityConnections.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommunityConnections.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer




        public ActionResult Index(string SearchTerm)
        {
            CustomerListingViewModel model = new CustomerListingViewModel();
            model.Customers = CustomerServices.Instance.GetCustomers(SearchTerm);
            return View(model);
        }



        #region CustomerActionRegion
        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            CustomerActionViewModel model = new CustomerActionViewModel();
            if (ID != 0)
            {
                var customer = CustomerServices.Instance.GetCustomer(ID);
                model.Cards = CardServices.Instance.GetListOfCardsViaName(customer.FullName);
                model.CustomPricings = CPServices.Instance.GetCustomPricingsViaName(customer.FullName);
                model.Ads = AdsServices.Instance.GetAdssViaName(customer.FullName);

                model.ID = customer.ID;
                model.FirstName = customer.FirstName;
                model.MiddleName = customer.MiddleName;
                model.LastName = customer.LastName;
                model.Notes = customer.Notes;
                model.Phone = customer.Phone;
                model.OtherPhone = customer.OtherPhone;
                model.Email = customer.Email;
                model.Compnay = customer.Compnay;
                model.BillingEmail = customer.BillingEmail;
                model.Address = customer.Address;
                model.AreaCode = customer.AreaCode;
                model.City = customer.City;
                model.Country = customer.Country;
                model.CustomerBalance = customer.CustomerBalance;
                model.Fax = customer.Fax;
                model.PaymentMethod = customer.PaymentMethod;
                model.PopupMessage = customer.PopupMessage;
                model.State = customer.State;
                model.Title = customer.Title;




                return PartialView("Action", model);

            }
            else
            {
                model.Cards = null;
                model.CustomPricings = null;
                model.Ads = null;
                return PartialView("Action", model);
            }
        }


        [HttpGet]
        public ActionResult View(int ID = 0)
        {
            CustomerActionViewModel model = new CustomerActionViewModel();


            var customer = CustomerServices.Instance.GetCustomer(ID);
            model.ID = customer.ID;
            model.FirstName = customer.FirstName;
            model.MiddleName = customer.MiddleName;
            model.LastName = customer.LastName;
            model.Notes = customer.Notes;
            model.Phone = customer.Phone;
            model.OtherPhone = customer.OtherPhone;
            model.Email = customer.Email;
            model.Compnay = customer.Compnay;
            model.BillingEmail = customer.BillingEmail;
            model.Address = customer.Address;
            model.AreaCode = customer.AreaCode;
            model.City = customer.City;
            model.Country = customer.Country;
            model.CustomerBalance = customer.CustomerBalance;
            model.Fax = customer.Fax;
            model.PaymentMethod = customer.PaymentMethod;
            model.PopupMessage = customer.PopupMessage;
            model.State = customer.State;
            model.Title = customer.Title;

            return View("View", model);


        }
        [HttpPost]
        public ActionResult Action(CustomerActionViewModel model)
        {

            if (model.ID != 0) //update record
            {
                var customer = CustomerServices.Instance.GetCustomer(model.ID);


                customer.ID = model.ID;
                customer.FirstName = model.FirstName;
                customer.MiddleName = model.MiddleName;
                customer.LastName = model.LastName;
                customer.FullName = customer.FirstName + " " + customer.MiddleName + " " + customer.LastName;
                customer.Notes = model.Notes;
                customer.Phone = model.Phone;
                customer.OtherPhone = model.OtherPhone;
                customer.Email = model.Email;
                customer.Compnay = model.Compnay;
                customer.BillingEmail = model.BillingEmail;
                customer.Address = model.Address;
                customer.AreaCode = model.AreaCode;
                customer.City = model.City;
                customer.Country = model.Country;
                customer.CustomerBalance = model.CustomerBalance;
                customer.Fax = model.Fax;
                customer.PaymentMethod = model.PaymentMethod;
                customer.PopupMessage = model.PopupMessage;
                customer.State = model.State;
                customer.Title = model.Title;

                var Cards = CardServices.Instance.GetListOfCardsViaName(customer.FullName);
                foreach (var item in Cards)
                {
                    item.Customer = customer.FullName;
                    CardServices.Instance.UpdateCard(item);

                }
                var CP = CPServices.Instance.GetListOfCustomPricingsViaName(customer.FullName);
                foreach (var item in CP)
                {
                    item.Customer = customer.FullName;
                    CPServices.Instance.UpdateCustomPricings(item);
                }
                CustomerServices.Instance.UpdateCustomer(customer);

            }
            else
            {
                var customer = new Customer();
                customer.FirstName = model.FirstName;
                customer.MiddleName = model.MiddleName;
                customer.LastName = model.LastName;
                customer.FullName = customer.FirstName + " " + customer.MiddleName + " " + customer.LastName;
                customer.Notes = model.Notes;
                customer.Phone = model.Phone;
                customer.OtherPhone = model.OtherPhone;
                customer.Email = model.Email;
                customer.Compnay = model.Compnay;
                customer.BillingEmail = model.BillingEmail;
                customer.Address = model.Address;
                customer.AreaCode = model.AreaCode;
                customer.City = model.City;
                customer.Country = model.Country;
                customer.CustomerBalance = model.CustomerBalance;
                customer.Fax = model.Fax;
                customer.PaymentMethod = model.PaymentMethod;
                customer.PopupMessage = model.PopupMessage;
                customer.State = model.State;
                customer.Title = model.Title;
                CustomerServices.Instance.SaveCustomer(customer);
            }

            return RedirectToAction("Index", "Customer");


        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            CustomerActionViewModel model = new CustomerActionViewModel();
            var customer = CustomerServices.Instance.GetCustomer(ID);
            model.ID = customer.ID;
            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CustomerActionViewModel model)
        {

            if (model.ID != 0) //we are trying to delete a record
            {
                var customer = CustomerServices.Instance.GetCustomer(model.ID);
                CustomerServices.Instance.DeleteCustomer(customer.ID);

            }
            return RedirectToAction("Index", "Customer");

        }



        #endregion


        #region CardActionRegion


        [HttpGet]
        public ActionResult CardAction(int ID = 0)
        {
            CustomerActionViewModel model = new CustomerActionViewModel();
            model.Customers = CustomerServices.Instance.GetCustomers();
            if (ID != 0)
            {
                var Card = CardServices.Instance.GetCard(ID);
                model.CardID = Card.ID;
                model.CardCustomer = Card.Customer;
                model.CardName = Card.CardName;
                model.CardNumber = Card.CardNumber;
                model.ExpirationDate = Card.ExpirationDate;
                model.SecurityCode = Card.SecurityCode;
                model.ZipCode = Card.ZipCode;
                model.CardAddress = Card.Address;


                return PartialView("CardAction", model);

            }
            else
            {
                return PartialView("CardAction", model);
            }
        }


        [HttpPost]
        public ActionResult CardAction(CustomerActionViewModel model)
        {

            if (model.CardID != 0) //update record
            {
                var Card = CardServices.Instance.GetCard(model.CardID);


                Card.ID = model.CardID;
                Card.Customer = model.CardCustomer;
                Card.CardName = model.CardName;
                Card.CardNumber = model.CardNumber;
                Card.ExpirationDate = model.ExpirationDate;
                Card.SecurityCode = model.SecurityCode;
                Card.ZipCode = model.ZipCode;
                Card.Address = model.CardAddress;

                CardServices.Instance.UpdateCard(Card);

            }
            else
            {
                var Card = new Card();
                Card.Customer = model.CardCustomer;
                Card.CardName = model.CardName;
                Card.CardNumber = model.CardNumber;
                Card.ExpirationDate = model.ExpirationDate;
                Card.SecurityCode = model.SecurityCode;
                Card.ZipCode = model.ZipCode;
                Card.Address = model.CardAddress;
                CardServices.Instance.SaveCard(Card);
            }

            return RedirectToAction("Index", "Customer");


        }





        [HttpGet]
        public async Task<ActionResult> CardDelete(int ID)
        {
            CustomerActionViewModel model = new CustomerActionViewModel();
            var Card = CardServices.Instance.GetCard(ID);
            model.CardID = Card.ID;
            return PartialView("_CardDelete", model);
        }

        [HttpPost]
        public ActionResult CardDelete(CustomerActionViewModel model)
        {
            JsonResult json = new JsonResult();


            if (model.CardID != 0) //we are trying to delete a record
            {
                var Card = CardServices.Instance.GetCard(model.CardID);
                CardServices.Instance.DeleteCard(Card.ID);
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid Role." };
            }

            return RedirectToAction("Index", "Customer");
        }



        #endregion



        #region CPActionRegion


        [HttpGet]
        public ActionResult CPAction(int ID = 0)
        {
            CustomerActionViewModel model = new CustomerActionViewModel();
            model.Customers = CustomerServices.Instance.GetCustomers();
            var AdSizes = new List<string>();
            AdSizes.Add("Full Page W’ Bleed");
            AdSizes.Add("Full Page");
            AdSizes.Add("3/4 Page");
            AdSizes.Add("1/2 Page Vertical");
            AdSizes.Add("1/2 Page");
            AdSizes.Add("1/3 Page");
            AdSizes.Add("1/4 Page");
            AdSizes.Add("1/4 Page Vertical");
            AdSizes.Add("1/8 Page");
            AdSizes.Add("Full Spread");
            AdSizes.Add("3/4 Spread");
            model.AdSizes = AdSizes;
            if (ID != 0)
            {
                var CP = CPServices.Instance.GetCustomPricings(ID);
                model.CPID = CP.ID;
                model.Customer = CP.Customer;
                model.AdSize = CP.AdSize;
                model.CustomNotes = CP.CustomNotes;
                model.Price = CP.Price;


                return PartialView("CPAction", model);

            }
            else
            {
                return PartialView("CPAction", model);
            }
        }


        [HttpPost]
        public ActionResult CPAction(CustomerActionViewModel model)
        {

            if (model.ID != 0) //update record
            {
                var CP = CPServices.Instance.GetCustomPricings(model.ID);


                CP.ID = model.CPID;
                CP.Customer = model.Customer;
                CP.AdSize = model.AdSize;
                CP.CustomNotes = model.CustomNotes;
                CP.Price = model.Price;
                CPServices.Instance.UpdateCustomPricings(CP);

            }
            else
            {
                var CP = new CustomPricings();

                CP.Customer = model.Customer;
                CP.AdSize = model.AdSize;
                CP.CustomNotes = model.CustomNotes;
                CP.Price = model.Price;
                CPServices.Instance.SaveCustomPricings(CP);
            }

            return RedirectToAction("Index", "Customer");


        }





        [HttpGet]
        public async Task<ActionResult> CPDelete(int ID)
        {
            CustomerActionViewModel model = new CustomerActionViewModel();
            var CP = CPServices.Instance.GetCustomPricings(ID);
            model.CPID = CP.ID;
            return PartialView("_CPDelete", model);
        }

        [HttpPost]
        public ActionResult CPDelete(CustomerActionViewModel model)
        {
            JsonResult json = new JsonResult();


            if (model.CPID != 0) //we are trying to delete a record
            {
                var CP = CPServices.Instance.GetCustomPricings(model.CPID);
                CPServices.Instance.DeleteCustomPricings(CP.ID);
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid Role." };
            }

            return RedirectToAction("Index", "Customer");
        }



        #endregion






    }
}