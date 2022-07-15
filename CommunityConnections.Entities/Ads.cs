using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnections.Entities
{
    public class Ads:BaseEntity
    {
        public string Name { get; set; }
        public DateTime AdDate { get; set; }
        public int PageNo { get; set; }
        public int PageTwo { get; set; }
        public string Layout { get; set; }
        public string AdSize { get; set; }
        public string Path { get; set; }
        public string AdStatus { get; set; }
        public string Sort { get; set; }

        public string Book { get; set; }
        public string Repeat { get; set; }
        public string Customer { get; set; }
        public int ChoosePage { get; set; }
        public string AddGraphics { get; set; }
        public string CustomSpecification { get; set; }
        public float Discount { get; set; }
        public float Total { get; set; }
        public string Status { get; set; }
        public bool Deluxe { get; set; }
    }
}
