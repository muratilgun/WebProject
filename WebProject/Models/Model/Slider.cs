using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebProject.Models.Model
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        [DisplayName("Slider Başlık"), StringLength(30, ErrorMessage ="max 30 karakter olmalıdır.")]
        public string Title { get; set; }
        [DisplayName("Slider Açıklama"), StringLength(150, ErrorMessage = "max 150 karakter olmalıdır.")]
        public string Description { get; set; }
        [DisplayName("Slider Resim"), StringLength(250)]
        public string ImageURL { get; set; }
    }
}