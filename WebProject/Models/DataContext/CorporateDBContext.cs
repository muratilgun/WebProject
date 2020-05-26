using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebProject.Models.Model;

namespace WebProject.Models.DataContext
{
    public class CorporateDBContext : DbContext
    {
        public CorporateDBContext() : base("KurumsalDB")
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SiteKimlik> SiteKimliks{ get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Comment> Comments { get; set; }

        
    }
}