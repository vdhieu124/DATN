using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DuLichV2.Models.EF
{
    [Table("Contact")]
    public class Contact : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(100)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(4000)]
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}