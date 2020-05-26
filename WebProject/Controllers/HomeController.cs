using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using WebProject.Models.DataContext;

namespace WebProject.Controllers
{

    public class HomeController : Controller
    {
        private CorporateDBContext db = new CorporateDBContext();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Service = db.Services.ToList().OrderByDescending(x => x.ServiceId);
            return View();
        }

        public ActionResult SliderPartial()
        {

            return View(db.Sliders.ToList().OrderByDescending(x => x.SliderId));
        }
        public ActionResult ServicePartial()
        {

            return View(db.Services.ToList().OrderByDescending(x => x.ServiceId));
        }

        public ActionResult About()
        {

            return View(db.Abouts.SingleOrDefault());
        }
        public ActionResult Service()
        {

            return View(db.Services.ToList().OrderByDescending(x => x.ServiceId));
        }
        
        [Route("/Home/Contact/")]
        public ActionResult Contact()
        {
            ViewBag.Kimlik = db.Contacts.SingleOrDefault();
            return View(db.Contacts.SingleOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(string adsoyad = null, string email = null, string konu = null, string mesaj = null)
        {

            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "emailsacma@gmail.com";
                WebMail.Password = "Sacma123.";
                WebMail.SmtpPort = 587;
                WebMail.Send("emailsacma@gmail.com", konu, email + "-" + mesaj);
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";
                Response.Redirect("/Home/Contact/");
                
            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu.Tekrar deneyiniz";
            }
            return View();
        }
        public ActionResult Blog()
        {
            return View(db.Blogs.Include("Category").ToList().OrderByDescending(x=> x.BlogId));
        }
        public ActionResult BlogDetail(int id)
        {
            var b = db.Blogs.Include("Category").Where(x => x.BlogId == id).SingleOrDefault();
            return View(b);
        }

        public ActionResult BlogCategoryPartial()
        {
            
            return PartialView(db.Categories.Include("Blogs").ToList().OrderBy(x=> x.CategoryName));
        }public ActionResult BlogPartial()
        {
            
            return PartialView(db.Blogs.ToList().OrderByDescending(x=> x.BlogId));
        }

        public ActionResult FooterPartial()
        {
            ViewBag.Service = db.Services.ToList().OrderByDescending(x => x.ServiceId);
            ViewBag.Contacts = db.Contacts.SingleOrDefault();
            ViewBag.Blog = db.Blogs.ToList().OrderByDescending(x => x.BlogId);
            return PartialView();
        }

    }
}