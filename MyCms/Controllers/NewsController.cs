using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCms.Controllers
{
    public class NewsController : Controller
    {
        MyCmsContext db = new MyCmsContext();
        IPageGroupRepository pageGroupRepository;
        IPageRepository pageRepository;
        IPageCommentRepository pageCommentRepository;

        public NewsController()
        {
            pageGroupRepository = new PageGroupRpository(db);
            pageRepository = new PageRepository(db);
            pageCommentRepository = new PageCommentRepository(db);
        }
        // GET: News
        public ActionResult ShowGroups()
        {
            return PartialView(pageGroupRepository.GetAllGroupsForView());
        }

        public ActionResult ShowGroupsInMenu()
        {
            return PartialView(pageGroupRepository.GetAllGroups());
        }

        public ActionResult TopNews()
        {
            return PartialView(pageRepository.ToNews());
        }
        [Route("Archive")]
        public ActionResult ArchiveNews(int pageId=1)
        {
            ViewBag.PageCount = pageRepository.GetAllPage().Count();
            ViewBag.pageId = pageId;
            int take = 4;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = pageRepository.GetAllPage().Count() / take;
            return View(pageRepository.GetAllPage().OrderByDescending(p=> p.CreateDate).Skip(skip).Take(take).ToList());
        }

        [Route("Group/{id}/{Title}")]
        public ActionResult ShowNewsGroupById(int id, string title)
        {
            ViewBag.Name = title;
            return View(pageRepository.ShowPageByGroupId(id));
        }

        [Route("News/{id}/{Title}")]
        public ActionResult ShowNews(int id,string title)
        {
            var news = pageRepository.GetPageById(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            else
            {
                news.Visit += 1;
                pageRepository.UpdatePage(news);
                pageRepository.Save();
                return View(news);
            }
        }

        [HttpPost]
        public ActionResult AddComment(int id,string name,string email,string comment)
        {
            PageComment Comment = new PageComment()
            {
                CreateDate = DateTime.Now,
                PageID=id,
                Comment=comment,
                Email=email,
                Name=name
            };
            pageCommentRepository.AddComment(Comment);
            return PartialView("ShowComments", pageCommentRepository.GetCommentByNewsId(id));
        }
        public ActionResult ShowComments(int id)
        {
            return PartialView(pageCommentRepository.GetCommentByNewsId(id));
        }
    }
}