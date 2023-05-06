﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuLichV2.Models.EF
{
    [Table("News")]
    public class News : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(200)]
        public string Image { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [StringLength(4000)]
        [AllowHtml]
        public string Detail { get; set; }
        public string Alias { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}