using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            ViewBag.Contacts = db.Contacts.SingleOrDefault();
            ViewBag.Blog = db.Blogs.ToList().OrderByDescending(x => x.BlogId);
            return View();
        }

        public ActionResult SliderPartial()
        {

            return View(db.Sliders.ToList().OrderByDescending(x=> x.SliderId));
        }
        public ActionResult ServicePartial()
        {

            return View(db.Services.ToList().OrderByDescending(x => x.ServiceId));
        }

        public ActionResult About()
        {

            return View(db.Abouts.SingleOrDefault());
        }

    }
}