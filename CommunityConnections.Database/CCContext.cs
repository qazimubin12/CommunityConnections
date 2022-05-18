using CommunityConnections.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnections.Database
{
    public class CCContext: DbContext,IDisposable
    {
        public CCContext() : base("CCConnectionStrings")
        {

        }

        public DbSet<Ads> Ads { get; set; }
        public DbSet<Pages> Pages { get; set; }
    }

}
