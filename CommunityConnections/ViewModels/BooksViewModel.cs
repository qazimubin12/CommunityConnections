using CommunityConnections.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityConnections.ViewModels
{
    public class BooksListingViewModel
    {
        public string SearchTerm { get; set; }
        public List<Book> Books { get; set; }
    }

    public class BooksActionViewModel
    {
        public int ID { get; set; }
        public string BookName { get; set; }
        public int AdID  { get; set; }
    }
}