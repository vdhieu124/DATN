using DuLichV2.Models;
using DuLichV2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace DuLichV2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class VehiclesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Vehicles
        public ActionResult Index(int? page)
        {
            IEnumerable<Vehicle> items = _dbContext.Vehicles.OrderByDescending(x => x.Id);
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Vehicle model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = DuLichV2.Models.Common.Filter.FilterChar(model.Name);
                _dbContext.Vehicles.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = _dbContext.Vehicles.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vehicle model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Vehicles.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = DuLichV2.Models.Common.Filter.FilterChar(model.Name);
                _dbContext.Entry(model).Property(x => x.Name).IsModified = true;
                _dbContext.Entry(model).Property(x => x.Detail).IsModified = true;
                _dbContext.Entry(model).Property(x => x.Alias).IsModified = true;
                _dbContext.Entry(model).Property(x => x.ModifiedBy).IsModified = true;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Vehicles.Find(id);
            if (item != null)
            {
                _dbContext.Vehicles.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = _dbContext.Vehicles.Find(Convert.ToInt32(item));
                        _dbContext.Vehicles.Remove(obj);
                        _dbContext.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}