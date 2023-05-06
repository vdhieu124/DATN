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
    [Authorize(Roles = "Admin")]
    public class ToursController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Tours
        public ActionResult Index(int? page)
        {
            IEnumerable<Tour> items = _dbContext.Tours.OrderByDescending(x => x.Id);
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
            ViewBag.TourCategory = new SelectList(_dbContext.TourCategories.ToList(), "Id", "Title");
            ViewBag.Place = new SelectList(_dbContext.Places.ToList(), "Id", "Name");
            ViewBag.Hotel = new SelectList(_dbContext.Hotels.ToList(), "Id", "Name");
            ViewBag.Vehicle = new SelectList(_dbContext.Vehicles.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Tour model,List<string> Images,List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.TourImages.Add(new TourImage
                            {
                                TourId = model.Id,
                                Image = Images[i],
                                IsDefault = true,
                            });
                        }
                        else
                        {
                            model.TourImages.Add(new TourImage
                            {
                                TourId = model.Id,
                                Image = Images[i],
                                IsDefault = false,
                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = DuLichV2.Models.Common.Filter.FilterChar(model.Name);
                _dbContext.Tours.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TourCategory = new SelectList(_dbContext.TourCategories.ToList(), "Id", "Title");
            ViewBag.Place = new SelectList(_dbContext.Places.ToList(), "Id", "Name");
            ViewBag.Hotel = new SelectList(_dbContext.Hotels.ToList(), "Id", "Name");
            ViewBag.Vehicle = new SelectList(_dbContext.Vehicles.ToList(), "Id", "Name");
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.TourCategory = new SelectList(_dbContext.TourCategories.ToList(), "Id", "Title");
            ViewBag.Place = new SelectList(_dbContext.Places.ToList(), "Id", "Name");
            ViewBag.Hotel = new SelectList(_dbContext.Hotels.ToList(), "Id", "Name");
            ViewBag.Vehicle = new SelectList(_dbContext.Vehicles.ToList(), "Id", "Name");
            var item = _dbContext.Tours.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tour model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = DuLichV2.Models.Common.Filter.FilterChar(model.Name);
                _dbContext.Tours.Attach(model);
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Tours.Find(id);
            if (item != null)
            {
                _dbContext.Tours.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = _dbContext.Tours.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, isHome= item.IsHome });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbContext.Tours.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
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
                        var obj = _dbContext.Tours.Find(Convert.ToInt32(item));
                        _dbContext.Tours.Remove(obj);
                        _dbContext.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}