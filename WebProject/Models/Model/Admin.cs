using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace WebProject.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required,StringLength(50,ErrorMessage ="En fazla 50 karakter olmalıdır.")]
        public string Email { get; set; }
        [Required, StringLength(50, ErrorMessage = "En fazla 50 karakter olmalıdır.")]
        public string Password { get; set; }
        public string Role { get; set; }


    }
}