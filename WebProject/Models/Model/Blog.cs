using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebProject.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string ImageURL { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}