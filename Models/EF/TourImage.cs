using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DuLichV2.Models.EF
{
    [Table("TourImage")]
    public class TourImage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TourId { get; set; }
        public string Image { get; set; }
        public bool IsDefault { get; set; }
        [ForeignKey("TourId")]
        public virtual Tour Tour { get; set; }
    }
}