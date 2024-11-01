using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public interface IPageRepository
    {
        IEnumerable<Page> GetAllPage();
        Page GetPageById(int pageId);
        bool InsertPage(Page page);
        bool UpdatePage(Page page);
        bool DelatePage(Page page);
        bool DeletePage(int pageId);
        void Save();

        IEnumerable<Page> ToNews(int take = 4);
        IEnumerable<Page> PagesInSlider();
        IEnumerable<Page> LastNews(int take = 4);
        IEnumerable<Page> ShowPageByGroupId(int groupId);
        IEnumerable<Page> SearchPage(string parameter);
    }
}
