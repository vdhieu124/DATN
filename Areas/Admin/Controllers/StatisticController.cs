using DuLichV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuLichV2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class StatisticController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Statistic
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStatistic(string fromDate, string toDate)
        {
            var query = from bt in _dbContext.BookTours
                        join btd in _dbContext.BookTourDetails on bt.Id equals btd.BookTourId
                        join t in _dbContext.Tours on btd.TourId equals t.Id
                        select new { 
                            CreatedDate = bt.CreatedDate,
                            Quantity = btd.Quantity,
                            Price = btd.Price,
                            OriginalPrice = t.OriginalPrice 
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreatedDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreatedDate < endDate);
            }

            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(x => new { 
                Date = x.Key.Value,
                TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                TotalSell = x.Sum(y => y.Quantity * y.Price)
            }).Select(x => new { 
                Date = x.Date,
                DoanhThu = x.TotalSell,
                LoiNhuan = x.TotalSell - x.TotalBuy
            });
            return Json(new { Data = result}, JsonRequestBehavior.AllowGet);
        }
    }
}