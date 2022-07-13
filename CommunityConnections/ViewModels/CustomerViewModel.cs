using CommunityConnections.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityConnections.ViewModels
{
    public class CustomerListingViewModel
    {
        public List<Customer> Customers { get; set; }
        public string SearchTerm { get; set; }
    }


    public class CustomerActionViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Compnay { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string AreaCode { get; set; }
        public string Phone { get; set; }
        public string OtherPhone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string BillingEmail { get; set; }
        public string PaymentMethod { get; set; }
        public float CustomerBalance { get; set; }
        public string PopupMessage { get; set; }
        public string Notes { get; set; }

        public List<Card> Cards { get; set; }
        public List<CustomPricings> CustomPricings { get; set; }
        public List<Ads> Ads { get; set; }

        /// <summary>
        /// //////////// Modal Added
        /// </summary>
        public List<Customer> Customers { get; set; }
        public int CPID { get; set; }/// <summary>
        /// name in the view
        /// </summary>
        public string Customer { get; set; }
        public string AdSize { get; set; }
        public float Price { get; set; }
        public string CustomNotes { get; set; }
        public List<string> AdSizes { get; set; }


        public int CardID { get; set; }/// <summary>
        /// name ad in the view
        /// </summary>
        public string CardCustomer { get; set; }/// <summary>
        /// name add in the view
        /// </summary>
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public string CardAddress { get; set; }/// <summary>
        /// name add in the view
        /// </summary>
        public string ZipCode { get; set; }



    }



    public class CustomerBreakDownViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }
    }

}