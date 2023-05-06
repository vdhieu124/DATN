using DuLichV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DuLichV2.Models.EF;

namespace DuLichV2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class BookTourController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/BookTour
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<BookTour> items = _dbContext.BookTours.OrderByDescending(x => x.Id);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult View(int id)
        {
            var item = _dbContext.BookTours.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = _dbContext.BookTours.Find(id);
            if (item != null)
            {
                _dbContext.BookTours.Attach(item);
                item.TypePayment = trangthai;
                _dbContext.Entry(item).Property(x => x.TypePayment).IsModified = true;
                _dbContext.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "Fail", Success = false });
        }
    }
}