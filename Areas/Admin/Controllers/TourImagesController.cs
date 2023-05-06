using DuLichV2.Models;
using DuLichV2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuLichV2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TourImagesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/TourImages
        public ActionResult Index(int TourId)
        {
            ViewBag.TourId = TourId;
            var items = _dbContext.TourImages.Where(x => x.TourId == TourId).ToList();
            return View(items);
        }

        [HttpPost]
        public ActionResult AddImage(int TourId, string url)
        {
            _dbContext.TourImages.Add(new TourImage { 
                TourId=TourId,
                Image=url,
                IsDefault=false
            });
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.TourImages.Find(id);
            _dbContext.TourImages.Remove(item);
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }
    }
}