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
    public class BookServices
    {
        #region Singleton
        public static BookServices Instance
        {
            get
            {
                if (instance == null) instance = new BookServices();
                return instance;
            }
        }
        private static BookServices instance { get; set; }
        private BookServices()
        {
        }
        #endregion

        public Book GetBooks(int ID)
        {
            using (var context = new CCContext())
            {
                return context.Books.Find(ID);
            }
        }

        public Book GetBook(string Name)
        {
            using (var context = new CCContext())
            {
                return context.Books.Where(x => x.BookName == Name).FirstOrDefault();
            }
        }


        public Book GetBook(int AdID)
        {
            using (var context = new CCContext())
            {
                return context.Books.Where(x => x.AdID == AdID).FirstOrDefault();
            }
        }


        public List<Book> GetBookss(string SearchTerm = "")
        {
            List<Book> Bookss = null;
            using (var context = new CCContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Bookss = context.Books.Where(x => x.BookName != null && x.BookName.ToLower()
                    .Contains(SearchTerm.ToLower())).ToList();
                }

                else
                {
                    Bookss = context.Books.OrderBy(x => x.BookName).ToList();
                }
            }
            return Bookss;
        }

      

        public void SaveBooks(Book Books)
        {
            using (var context = new CCContext())
            {
                context.Books.Add(Books);
                context.SaveChanges();
            }
        }

        public void UpdateBooks(Book Books)
        {
            using (var context = new CCContext())
            {
                context.Entry(Books).State = EntityState.Modified;
                context.SaveChanges();
            }
        }



        public void DeleteBooks(int ID)
        {
            using (var context = new CCContext())
            {

                var Books = context.Books.Find(ID);
                context.Books.Remove(Books);
                context.SaveChanges();
            }
        }
    }
}
