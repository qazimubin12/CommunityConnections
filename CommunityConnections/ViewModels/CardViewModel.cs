using CommunityConnections.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityConnections.ViewModels
{
    public class CardListingViewModel
    {
        public string SearchTerm { get; set; }
        public List<Card> Cards { get; set; }
    }

    public class CardActionViewModel
    {
        public int ID { get; set; }
        public string Customer { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public List<Customer> Customers { get; set; }
    }
}