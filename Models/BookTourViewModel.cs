using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DuLichV2.Models
{
    public class BookTourViewModel
    {
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public string Address { get; set; }
        public int TypePayment { get; set; }
        public int TypePaymentVN { get; set; }
    }
}