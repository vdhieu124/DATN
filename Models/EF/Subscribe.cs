﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DuLichV2.Models.EF
{
    [Table("Subscribe")]
    public class Subscribe
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(200)]
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}