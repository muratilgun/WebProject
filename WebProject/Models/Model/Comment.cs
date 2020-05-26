using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebProject.Models.Model
{
    [Table("Comment")]
    public class Comment
    {
        public int CommentId { get; set; }
        [Required,StringLength(50,ErrorMessage ="max 50 karakter olabilir.")]
        public string FullName { get; set; }
        public string Eposta { get; set; }
        [DisplayName("Yorumunuz")]
        public string Contents { get; set; }
        public bool Onay { get; set; }
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }

    }
}