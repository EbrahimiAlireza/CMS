using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class PageGroupRpository : IPageGroupRepository
    {
        private MyCmsContext db;

        public PageGroupRpository(MyCmsContext context)
        {
            this.db = context;

        }

        public bool DeleteGroup(int pageGroupId)
        {
            try
            {
                var group = GetGroupById(pageGroupId);
                DeleteGroup(group);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteGroup(PageGroup pageGroup)
        {
            try
            {
                db.Entry(pageGroup).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<PageGroup> GetAllGroups()
        {
            return db.PageGroups;
        }

        public PageGroup GetGroupById(int groupId)
        {
            return db.PageGroups.Find(groupId);
        }

        public bool InsertGroup(PageGroup pageGroup)
        {
            try
            {
                db.PageGroups.Add(pageGroup);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool UpadteGroup(PageGroup pageGroup)
        {
            try
            {
                db.Entry(pageGroup).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<ShowGroupsViewModels> GetAllGroupsForView()
        {
            return db.PageGroups.Select(g => new ShowGroupsViewModels() {
                GroupId = g.GroupID,
                GroupTitle = g.GroupTitle,
                PageCount = g.Pages.Count()

            });
        }
    }
}
