﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnections.Entities
{
    public class Section:BaseEntity
    {
        public string SectionName { get; set; }
        public int NoOfPages { get; set; }
        public string After { get; set; }
    }
}