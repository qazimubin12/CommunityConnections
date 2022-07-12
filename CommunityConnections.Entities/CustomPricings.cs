using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnections.Entities
{
    public class CustomPricings:BaseEntity
    {
        public string Customer { get; set; }
        public string AdSize { get; set; }
        public float Price { get; set; }
        public string CustomNotes { get; set; }
    }
}
