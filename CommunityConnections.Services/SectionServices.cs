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
    public class SectionServices
    {
        #region Singleton
        public static SectionServices Instance
        {
            get
            {
                if (instance == null) instance = new SectionServices();
                return instance;
            }
        }
        private static SectionServices instance { get; set; }
        private SectionServices()
        {
        }
        #endregion

        public Section GetSections(int ID)
        {
            using (var context = new CCContext())
            {
                return context.Sections.Find(ID);
            }
        }

        public Section GetSection(string Name)
        {
            using (var context = new CCContext())
            {
                return context.Sections.Where(x => x.SectionName == Name).FirstOrDefault();
            }
        }


        public List<Section> GetSectionss(string SearchTerm = "")
        {
            List<Section> Sectionss = null;
            using (var context = new CCContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Sectionss = context.Sections.Where(x => x.SectionName != null && x.SectionName.ToLower()
                    .Contains(SearchTerm.ToLower())).ToList();
                }

                else
                {
                    Sectionss = context.Sections.OrderBy(x=>x.SectionName).ToList();
                }
            }
            return Sectionss;
        }

        public List<Section> GetNotTrailingSections(string SearchTerm = "")
        {
            List<Section> Sectionss = null;
            using (var context = new CCContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Sectionss = context.Sections.Where(x => x.MoveForward == "No" && x.SectionName != null && x.SectionName.ToLower()
                    .Contains(SearchTerm.ToLower())).ToList();
                }

                else
                {
                    Sectionss = context.Sections.OrderBy(x => x.SectionName).ToList();
                }
            }
            return Sectionss;
        }

        public List<Section> GetNotTrailingSectionsBookName(string BookName)
        {
            List<Section> Sectionss = null;
            using (var context = new CCContext())
            {
                Sectionss = context.Sections.Where(x => x.MoveForward == "No" && x.Book == BookName).ToList();
               
            }
            return Sectionss;
        }


        public void SaveSections(Section Sections)
        {
            using (var context = new CCContext())
            {
                context.Sections.Add(Sections);
                context.SaveChanges();
            }
        }

        public void UpdateSections(Section Sections)
        {
            using (var context = new CCContext())
            {
                context.Entry(Sections).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


  
        public void DeleteSections(int ID)
        {
            using (var context = new CCContext())
            {

                var Sections = context.Sections.Find(ID);
                context.Sections.Remove(Sections);
                context.SaveChanges();
            }
        }
    }
}
