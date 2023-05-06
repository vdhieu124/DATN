using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DuLichV2.Models.EF
{
    [Table("Vehicle")]
    public class Vehicle : CommonAbstract
    {
        public Vehicle()
        {
            this.Tours = new HashSet<Tour>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Detail { get; set; }
        public string Alias { get; set; }
        public ICollection<Tour> Tours { get; set; }
    }
}