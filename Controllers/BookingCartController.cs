using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuLichV2.Models;
using DuLichV2.Models.EF;
using DuLichV2.Models.Payments;

namespace DuLichV2.Controllers
{
    public class BookingCartController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: BookingCart
        public ActionResult Index()
        {
            BookingCart cart = (BookingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult VnpayReturn()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                string bookCode = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        //Thanh toan thanh cong
                        var itemBook = _dbContext.BookTours.FirstOrDefault(x => x.Code == bookCode);
                        if (itemBook != null)
                        {
                            _dbContext.BookTours.Attach(itemBook);
                            _dbContext.Entry(itemBook).State = System.Data.Entity.EntityState.Modified;
                            _dbContext.SaveChanges();
                        }
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }
            return View();
        }
        public ActionResult CheckOut()
        {
            BookingCart cart = (BookingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult Partial_CheckOut()
        {
            return PartialView(); 
        }
        public ActionResult CheckOutSuccess()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(BookTourViewModel req)
        {
            var code = new { Success = false, Code = -1, Url="" };
            if (ModelState.IsValid)
            {
                BookingCart cart = (BookingCart)Session["Cart"];
                if (cart != null)
                {
                    BookTour booktour = new BookTour();
                    booktour.CustomerName = req.CustomerName;
                    booktour.Phone = req.Phone;
                    booktour.Email = req.Email;
                    booktour.Address = req.Address;
                    cart.items.ForEach(x => booktour.BookTourDetails.Add(new BookTourDetail { 
                        TourId=x.TourId,
                        Price=x.Price,
                        Quantity=x.Quantity
                    }));
                    booktour.SubTotal = cart.items.Sum(x => (x.Price * x.Quantity));
                    booktour.TypePayment = req.TypePayment;
                    booktour.CreatedDate = DateTime.Now;
                    booktour.ModifiedDate = DateTime.Now;
                    booktour.CreatedBy = req.Phone;
                    Random rd = new Random();
                    booktour.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    _dbContext.BookTours.Add(booktour);
                    _dbContext.SaveChanges();

                    //Gửi mail khi đặt tour thành công
                    var strTour = "";
                    var thanhtien = decimal.Zero;
                    var TongTien = decimal.Zero;
                    foreach (var tour in cart.items)
                    {
                        strTour += "<tr>";
                        strTour+="<td>"+tour.TourName+"</td>";
                        strTour+="<td>"+tour.Quantity+"</td>";
                        strTour+="<td>"+DuLichV2.Common.Common.FormatNumber(tour.TotalPrice, 0)+"</td>";
                        strTour += "</tr>";
                        thanhtien += tour.Price * tour.Quantity;
                    }
                    TongTien = thanhtien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/sendCus.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}", booktour.Code);
                    contentCustomer = contentCustomer.Replace("{{Tour}}", strTour);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", booktour.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", booktour.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", booktour.Email);
                    contentCustomer = contentCustomer.Replace("{{DiaChi}}", booktour.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", DuLichV2.Common.Common.FormatNumber(thanhtien, 0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", DuLichV2.Common.Common.FormatNumber(TongTien, 0));
                    DuLichV2.Common.Common.SendMail("HIEU Travel", "Đơn đặt tour #" + booktour.Code, contentCustomer.ToString(), booktour.Email);

                    cart.ClearCart();
                    code = new { Success = true, Code = req.TypePayment, Url = "" };
                    if (req.TypePayment == 2)
                    {
                        var url = UrlPayment(req.TypePaymentVN, booktour.Code);
                        code = new { Success = true, Code = req.TypePayment, Url = url };
                    }
                }
            }
            return Json(code);
        }
        public ActionResult Partial_Item_Pay()
        {
            BookingCart cart = (BookingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                return PartialView(cart.items);
            }
            return PartialView();
        }
        public ActionResult Partial_Item_Cart()
        {
            BookingCart cart = (BookingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                return PartialView(cart.items);
            }
            return PartialView();
        }

        public ActionResult ShowCount()
        {
            BookingCart cart = (BookingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { count = cart.items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, count = 0 };
            var _dbContext = new ApplicationDbContext();
            var checkTour = _dbContext.Tours.FirstOrDefault(x => x.Id == id);
            if (checkTour != null)
            {
                BookingCart cart = (BookingCart)Session["Cart"];
                if (cart == null)
                {
                    cart = new BookingCart();
                }
                BookingCartItem item = new BookingCartItem
                {
                    TourId = checkTour.Id,
                    TourName = checkTour.Name,
                    TourImg = checkTour.Image,
                    CategoryName = checkTour.TourCategory.Title,
                    Alias=checkTour.Alias,
                    Quantity = quantity
                };
                item.Price = checkTour.Price;
                if (checkTour.PriceSale != checkTour.Price)
                {
                    item.Price = (decimal)checkTour.PriceSale;
                }
                item.TotalPrice = item.Price * item.Quantity;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm tour vào hàng đợi thành công!", code = 1, count = cart.items.Count };
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            BookingCart cart = (BookingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id,quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, count = 0 };
            BookingCart cart = (BookingCart)Session["Cart"];
            if(cart != null)
            {
                var checkTour = cart.items.FirstOrDefault(x => x.TourId == id);
                if (checkTour != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, count = cart.items.Count };
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            BookingCart cart = (BookingCart)Session["Cart"];
            if(cart != null)
            {
                cart.ClearCart();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        #region Thanh toán VN Pay
        public string UrlPayment(int TypePaymentVN, string bookCode)
        {
            var urlPayment = "";
            var book = _dbContext.BookTours.FirstOrDefault(x => x.Code == bookCode);

            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((long)book.SubTotal * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", book.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn đặt tour:" + book.Code);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", book.Code); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return urlPayment;
        }
        #endregion
    }
}