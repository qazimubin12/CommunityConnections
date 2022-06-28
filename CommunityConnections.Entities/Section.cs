using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnections.Entities
{
    public class Section:BaseEntity
    {
        public string SectionName { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public bool InBetweenAny { get; set; }
        public string BeforeSection { get; set; }
    }
}
