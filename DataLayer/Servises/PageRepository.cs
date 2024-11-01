using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public class PageRepository : IPageRepository
    {

        private MyCmsContext db;

        public PageRepository(MyCmsContext context)
        {
            this.db = context;

        }
        public bool DelatePage(Page page)
        {
            try
            {
                db.Entry(page).State = System.Data.Entity.EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
            };
        }

        public bool DeletePage(int pageId)
        {
            try
            {
                var page = GetPageById(pageId);
                DelatePage(page);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Page> GetAllPage()
        {
            return db.Pages;
        }

        public Page GetPageById(int pageId)
        {
            return db.Pages.Find(pageId);
        }

        public bool InsertPage(Page page)
        {
            try
            {
                db.Pages.Add(page);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Page> LastNews(int take = 4)
        {
            return db.Pages.OrderByDescending(p => p.CreateDate).Take(take);
        }

        public IEnumerable<Page> PagesInSlider()
        {
            return db.Pages.Where(s => s.ShowInSlider == true);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Page> SearchPage(string parameter)
        {
            return db.Pages.Where(s => s.Title.Contains(parameter) || s.ShortDescription.Contains(parameter) || s.Text.Contains(parameter) || s.Tags.Contains(parameter)).Distinct();
        }

        public IEnumerable<Page> ShowPageByGroupId(int groupId)
        {
            return db.Pages.Where(g => g.GroupID == groupId);
        }

        public IEnumerable<Page> ToNews(int take = 4)
        {
            return db.Pages.OrderByDescending(p => p.Visit).Take(take);
        }

        public bool UpdatePage(Page page)
        {
            try
            {
                db.Entry(page).State = System.Data.Entity.EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
