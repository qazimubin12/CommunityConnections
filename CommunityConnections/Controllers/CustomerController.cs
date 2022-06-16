using CommunityConnections.Entities;
using CommunityConnections.Services;
using CommunityConnections.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            CustomerActionViewModel model = new CustomerActionViewModel();
 
            if (ID != 0)
            {
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
                model.CustomPricing = customer.CustomPricing;
                model.Fax = customer.Fax;
                model.PaymentMethod = customer.PaymentMethod;
                model.PopupMessage = customer.PopupMessage;
                model.State = customer.State;
                model.Title = customer.Title;

                return View("Action", model);

            }
            else
            {
                return View("Action", model);
            }
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
                customer.CustomPricing = model.CustomPricing;
                customer.Fax = model.Fax;
                customer.PaymentMethod = model.PaymentMethod;
                customer.PopupMessage = model.PopupMessage;
                customer.State = model.State;
                customer.Title = model.Title;

                CustomerServices.Instance.UpdateCustomer(customer);

            }
            else
            {
                var customer = new Customer();
                customer.FirstName = model.FirstName;
                customer.MiddleName = model.MiddleName;
                customer.LastName = model.LastName;
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
                customer.CustomPricing = model.CustomPricing;
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


    }
}