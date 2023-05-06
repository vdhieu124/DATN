using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuLichV2.Models.EF
{
    [Table("Tour")]
    public class Tour : CommonAbstract
    {
        public Tour()
        {
            this.TourImages = new HashSet<TourImage>();
            this.BookTourDetails = new HashSet<BookTourDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(500)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(100)]
        public string TourCode { get; set; }
        public string Alias { get; set; }
        [AllowHtml]
        [StringLength(4000)]
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public string Detail { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public string Time { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public string DepartureTime { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public decimal OriginalPrice { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public int Quantity { get; set; }
        public int ViewCount { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsHot { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [ForeignKey("TourCategory")]
        public int TourCategoryId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public virtual TourCategory TourCategory { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Place Place { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<TourImage> TourImages { get; set; }
        public virtual ICollection<BookTourDetail> BookTourDetails { get; set; }

    }
}