using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DuLichV2.Models.EF
{
    [Table("BookTourDetail")]
    public class BookTourDetail
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BookTourId { get; set; }
        public int TourId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public virtual BookTour BookTour { get; set; }
        public virtual Tour Tour { get; set; }
    }
}