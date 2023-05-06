using DuLichV2.Models;
using DuLichV2.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuLichV2.Controllers
{
    public class ToursController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Tours
        public ActionResult Index(string searchText,int? id,int? page)
        {
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Tour> items = _dbContext.Tours.OrderBy(x => x.CreatedDate);
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x => x.Alias.Split('-').Contains(searchText) || x.Name.ToLower().Contains(searchText.ToLower()));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            
            if (id != null)
            {
                items = items.Where(x => x.TourCategoryId == id).OrderBy(x => x.CreatedDate);
            }
            return View(items);
        }
        public ActionResult TourCategory(string Alias, int id, string searchText)
        {
            IEnumerable<Tour> items = _dbContext.Tours.OrderBy(x => x.CreatedDate);
            if (id > 0)
            {
                items = items.Where(x => x.TourCategoryId == id).ToList();
                if (!string.IsNullOrEmpty(searchText))
                {
                    items = items.Where(x => x.Alias.Split('-').Contains(searchText) || x.Name.ToLower().Contains(searchText.ToLower()));
                }
            }
            var cate = _dbContext.TourCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult Detail(string Alias, int Id)
        {
            var item = _dbContext.Tours.Find(Id);
            if (item != null)
            {
                _dbContext.Tours.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                _dbContext.Entry(item).Property(x => x.ViewCount).IsModified = true;
                _dbContext.SaveChanges();
            }
            return View(item);
        }
        public ActionResult Partial_ItemsByTourId()
        {
            var items = _dbContext.Tours.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ItemsSale()
        {
            var items = _dbContext.Tours.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ItemsMaxView()
        {
            var item = _dbContext.Tours.OrderByDescending(x => x.ViewCount).First();
            return PartialView(item);
        }
    }
}