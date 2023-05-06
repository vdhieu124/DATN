using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuLichV2.Models.EF
{
    [Table("Hotel")]
    public class Hotel : CommonAbstract
    {
        public Hotel()
        {
            this.Tours = new HashSet<Tour>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(4000)]
        [AllowHtml]
        public string Detail { get; set; }
        [StringLength(200)]
        public string Image { get; set; }
        public string Alias { get; set; }
        public ICollection<Tour> Tours { get; set; }
    }
}