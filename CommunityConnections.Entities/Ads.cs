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
        public int PageNo { get; set; }
        public string Layout { get; set; }
        public string AdSize { get; set; }
        public string Path { get; set; }
    }
}
