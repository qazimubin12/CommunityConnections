using CommunityConnections.Database;
using CommunityConnections.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnections.Services
{
    public class CustomerServices
    {
        #region Singleton
        public static CustomerServices Instance
        {
            get
            {
                if (instance == null) instance = new CustomerServices();
                return instance;
            }
        }
        private static CustomerServices instance { get; set; }
        private CustomerServices()
        {
        }
        #endregion


        public Customer GetCustomer(int ID)
        {
            using (var context = new CCContext())
            {
                return context.Customers.Find(ID);
            }
        }


        public List<Customer> GetCustomers(string SearchTerm = "")
        {
            List<Customer> Customers = null;
            using (var context = new CCContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Customers = context.Customers.Where(x => x.FirstName != null && x.FirstName.ToLower()
                     .Contains(SearchTerm.ToLower())).ToList();
                }
                else
                {
                    Customers = context.Customers.ToList();
                }
            }
            return Customers;
        }


        public void SaveCustomer(Customer Customer)
        {
            using (var context = new CCContext())
            {
                context.Customers.Add(Customer);
                context.SaveChanges();
            }
        }

        public void UpdateCustomer(Customer Customer)
        {
            using (var context = new CCContext())
            {
                context.Entry(Customer).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


   

        public void DeleteCustomer(int ID)
        {
            using (var context = new CCContext())
            {

                var Customer = context.Customers.Find(ID);
                context.Customers.Remove(Customer);
                context.SaveChanges();
            }
        }
    }
}
