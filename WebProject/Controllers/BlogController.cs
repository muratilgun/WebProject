using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebProject.Models.DataContext;
using WebProject.Models.Model;

namespace WebProject.Controllers
{
    public class BlogController : Controller
    {
        private CorporateDBContext db = new CorporateDBContext();
        // GET: Blog
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            
            return View(db.Blogs.Include("Category").ToList().OrderByDescending(x=>x.BlogId));
        }
        public ActionResult Create()
        {
            //data taşıma işlemi
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken] //veri güvenliğini sağlar.
        public ActionResult Create(Blog blog, HttpPostedFileBase ImageURL)
        {
            if (ImageURL != null)
            {
                                
                WebImage img = new WebImage(ImageURL.InputStream);
                FileInfo imginfo = new FileInfo(ImageURL.FileName);
                string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(600, 400);
                img.Save("~/Uploads/Blog/" + blogimgname);

                blog.ImageURL = "/Uploads/Blog/" + blogimgname;
            }
            db.Blogs.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            if (id== null)
            {
                return HttpNotFound();
            }
            var b = db.Blogs.Where(x => x.BlogId == id).SingleOrDefault();
            if (b==null)//kaydın boş gelme ihtimaline karşı güvenlik
            {
                return HttpNotFound();

            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", b.CategoryId);
            return View(b);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Blog blog, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.Blogs.Where(x => x.BlogId == id).SingleOrDefault();
                if (ImageURL != null)
                {
                    //daha önce kaydettiğimiz foto varsa bunu sil komutu
                    if (System.IO.File.Exists(Server.MapPath(b.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(b.ImageURL));

                    }
                    WebImage img = new WebImage(ImageURL.InputStream);
                    FileInfo imginfo = new FileInfo(ImageURL.FileName);
                    string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Uploads/Blog/" + blogimgname);

                    b.ImageURL = "/Uploads/Blog/" + blogimgname;
                }
                b.Title = blog.Title;
                b.Contents = blog.Contents;
                b.CategoryId = blog.CategoryId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var b = db.Blogs.Find(id);
            if (b==null)//Biz kendimiz bul diyoruz Boş dönme şansı yok ???
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(b.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath(b.ImageURL));

            }
            db.Blogs.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }
    }
}