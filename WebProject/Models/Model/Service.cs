using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebProject.Models.Model
{
    [Table("Service")]
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        [Required, StringLength(150, ErrorMessage = "150 Karakter olabilir")]
        [DisplayName("Hizmet Başlık")]
        public string Title { get; set; }
        [DisplayName("Hizmet Açıklama")]
        public string Description { get; set; }
        [DisplayName("Hizmet Resim")]
        public string ImageURL { get; set; }
    }
}