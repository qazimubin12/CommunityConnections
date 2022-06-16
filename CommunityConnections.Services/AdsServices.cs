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
    public class AdsServices
    {
        #region Singleton
        public static AdsServices Instance
        {
            get
            {
                if (instance == null) instance = new AdsServices();
                return instance;
            }
        }
        private static AdsServices instance { get; set; }
        private AdsServices()
        {
        }
        #endregion

            public Ads GetAds(int ID)
            {
                using (var context = new CCContext())
                {
                    return context.Ads.Find(ID);
                }
            }


        public void UpdateAdNewMethod(Ads ad, string Property, string value)
        {
            using (var context = new CCContext())
            {
                context.Entry(ad).Property(Property).CurrentValue = value;
                context.SaveChanges();
            }
        }

        public List<Ads> GetAdss(string SearchTerm = "")
        {
            List<Ads> Adss = null;
            using (var context = new CCContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Adss = context.Ads.Where(x=>x.Name == SearchTerm).ToList();
                }
                else
                {
                    Adss = context.Ads.ToList();
                }
            }
            return Adss;
        }




        public List<Ads> GetNotPlacedAdss(string SearchTerm = "")
        {
            List<Ads> Adss = null;
            using (var context = new CCContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Adss = context.Ads.Where(x => x.AdStatus == "Not Placed" && x.Name == SearchTerm).ToList();
                }
                else
                {
                    Adss = context.Ads.Where(x => x.AdStatus == "Not Placed").ToList();
                }
            }
            return Adss;
        }

        public List<Ads> GetPlacedAdss(string SearchTerm = "")
        {
            List<Ads> Adss = null;
            using (var context = new CCContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Adss = context.Ads.Where(x => x.AdStatus == "Placed" && x.Name == SearchTerm).OrderBy(x=>x.Sort).ToList();
                }
                else
                {
                    Adss = context.Ads.Where(x => x.AdStatus == "Placed" ).OrderBy(x=>x.Sort).ToList();
                }
            }
            return Adss;
        }


        public void SaveAds(Ads Ads)
        {
            using (var context = new CCContext())
            {
                context.Ads.Add(Ads);
                context.SaveChanges();
            }
        }

        public void UpdateAds(Ads Ads)
        {
            using (var context = new CCContext())
            {
                context.Entry(Ads).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public List<Ads> AdsonPage(int Page)
        {
            using(var context = new CCContext())
            {
                return context.Ads.Where(x => x.PageNo == Page && x.AdStatus == "Placed").ToList();
            }
        }

        public void DeleteAds(int ID)
        {
            using (var context = new CCContext())
            {

                var Ads = context.Ads.Find(ID);
                context.Ads.Remove(Ads);
                context.SaveChanges();
            }
        }
    }
}
