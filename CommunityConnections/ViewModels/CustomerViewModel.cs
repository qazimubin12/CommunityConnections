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
        public string Notes { get; set; }
        public string PopupMessage { get; set; }
        public string CustomPricing { get; set; }
    }



    public class CustomerBreakDownViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }
    }

}