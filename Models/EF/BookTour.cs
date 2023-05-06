using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DuLichV2.Models.EF
{
    [Table("BookTour")]
    public class BookTour : CommonAbstract
    {
        public BookTour()
        {
            this.BookTourDetails = new HashSet<BookTourDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(50)]
        public string Code { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(100)]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(100)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(11)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(200)]
        public string Address { get; set; }
        public decimal SubTotal { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public int Quantity { get; set; }
        public int TypePayment { get; set; }
        public virtual ICollection<BookTourDetail> BookTourDetails { get; set; }
    }
}