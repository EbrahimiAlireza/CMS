using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PageCommentRepository : IPageCommentRepository
    {
        MyCmsContext db = new MyCmsContext();
        public PageCommentRepository(MyCmsContext context)
        {
            this.db = context;
        }
        public bool AddComment(PageComment comment)
        {
           try
            {
                db.PageComments.Add(comment);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<PageComment> GetCommentByNewsId(int pageId)
        {
            return db.PageComments.Where(p => p.PageID == pageId);

        }
    }
}
