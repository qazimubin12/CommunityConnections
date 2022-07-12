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
    public class CardServices
    {
        #region Singleton
        public static CardServices Instance
        {
            get
            {
                if (instance == null) instance = new CardServices();
                return instance;
            }
        }
        private static CardServices instance { get; set; }
        private CardServices()
        {
        }
        #endregion


        public Card GetCard(int ID)
        {
            using (var context = new CCContext())
            {
                return context.Cards.Find(ID);
            }
        }

        

        public List<Card> GetCards(string SearchTerm = "")
        {
            List<Card> Cards = null;
            using (var context = new CCContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Cards = context.Cards.Where(x => x.Customer != null && x.Customer.ToLower()
                     .Contains(SearchTerm.ToLower())).ToList();
                }
                else
                {
                    Cards = context.Cards.ToList();
                }
            }
            return Cards;
        }

        public Card GetCardsViaName(string SearchTerm)
        {
            Card Card = null;
            using (var context = new CCContext())
            {
                
                    Card = context.Cards.Where(x => x.Customer == SearchTerm).FirstOrDefault();
               
            }
            return Card;
        }

        public List<Card> GetListOfCardsViaName(string SearchTerm)
        {
            var Cards = new List<Card>();
            using (var context = new CCContext())
            {

                 Cards = context.Cards.Where(x => x.Customer == SearchTerm).ToList();

            }
            return Cards;
        }





        public void SaveCard(Card Card)
        {
            using (var context = new CCContext())
            {
                context.Cards.Add(Card);
                context.SaveChanges();
            }
        }

        public void UpdateCard(Card Card)
        {
            using (var context = new CCContext())
            {
                context.Entry(Card).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


   

        public void DeleteCard(int ID)
        {
            using (var context = new CCContext())
            {

                var Card = context.Cards.Find(ID);
                context.Cards.Remove(Card);
                context.SaveChanges();
            }
        }
    }
}
