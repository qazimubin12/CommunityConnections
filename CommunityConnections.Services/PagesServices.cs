using CommunityConnections.Database;
using CommunityConnections.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnections.Services
{
    public class PagesServices
    {
        #region Singleton
        public static PagesServices Instance
        {
            get
            {
                if (instance == null) instance = new PagesServices();
                return instance;
            }
        }
        private static PagesServices instance { get; set; }
        private PagesServices()
        {
        }
        #endregion

        public Pages GetPages()
        {
            using (var context = new CCContext())
            {
                return context.Pages.FirstOrDefault();
            }
        }
    }
}
