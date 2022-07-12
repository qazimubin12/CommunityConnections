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
    public class CPServices
    {
        #region Singleton
        public static CPServices Instance
        {
            get
            {
                if (instance == null) instance = new CPServices();
                return instance;
            }
        }
        private static CPServices instance { get; set; }
        private CPServices()
        {
        }
        #endregion


        public CustomPricings GetCustomPricings(int ID)
        {
            using (var context = new CCContext())
            {
                return context.CustomPricings.Find(ID);
            }
        }

        

        public List<CustomPricings> GetCustomPricings(string SearchTerm = "")
        {
            List<CustomPricings> CustomPricings = null;
            using (var context = new CCContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    CustomPricings = context.CustomPricings.Where(x => x.Customer != null && x.Customer.ToLower()
                     .Contains(SearchTerm.ToLower())).ToList();
                }
                else
                {
                    CustomPricings = context.CustomPricings.ToList();
                }
            }
            return CustomPricings;
        }

        public List<CustomPricings> GetCustomPricingsViaName(string SearchTerm)
        {
            List<CustomPricings> CustomPricings = new List<CustomPricings>();
            using (var context = new CCContext())
            {
                
                    CustomPricings = context.CustomPricings.Where(x => x.Customer == SearchTerm).ToList();
               
            }
            return CustomPricings;
        }

        public List<CustomPricings> GetListOfCustomPricingsViaName(string SearchTerm)
        {
            var CustomPricings = new List<CustomPricings>();
            using (var context = new CCContext())
            {

                 CustomPricings = context.CustomPricings.Where(x => x.Customer == SearchTerm).ToList();

            }
            return CustomPricings;
        }





        public void SaveCustomPricings(CustomPricings CustomPricings)
        {
            using (var context = new CCContext())
            {
                context.CustomPricings.Add(CustomPricings);
                context.SaveChanges();
            }
        }

        public void UpdateCustomPricings(CustomPricings CustomPricings)
        {
            using (var context = new CCContext())
            {
                context.Entry(CustomPricings).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


   

        public void DeleteCustomPricings(int ID)
        {
            using (var context = new CCContext())
            {

                var CustomPricings = context.CustomPricings.Find(ID);
                context.CustomPricings.Remove(CustomPricings);
                context.SaveChanges();
            }
        }
    }
}
