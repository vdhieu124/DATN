using DuLichV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuLichV2.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = _dbContext.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", items);
        }
        public ActionResult MenuTourCategory()
        {
            var items = _dbContext.TourCategories.ToList();
            return PartialView("_MenuTourCategory", items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = _dbContext.TourCategories.ToList();
            return PartialView("_MenuLeft", items);
        }
        public ActionResult MenuArrival()
        {
            var items = _dbContext.TourCategories.ToList();
            return PartialView("_MenuArrival", items);
        }
    }
}