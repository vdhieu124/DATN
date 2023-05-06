using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DuLichV2.Models.EF
{
    [Table("Place")]
    public class Place : CommonAbstract
    {
        public Place()
        {
            this.Tours = new HashSet<Tour>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(30)]
        public string Name { get; set; }
        public string Alias { get; set; }
        public ICollection<Tour> Tours { get; set; }
    }
}