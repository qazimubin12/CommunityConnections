using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnections.Entities
{
    public class Book:BaseEntity
    {
        public string BookName { get; set; }
        public int AdID { get; set; }
    }
}
