using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models.DataContext;
using WebProject.Models.Model;

namespace WebProject.Controllers
{
    public class AboutController : Controller
    {
        CorporateDBContext db = new CorporateDBContext();

        // GET: About
        public ActionResult Index()
        {
            var h = db.Abouts.ToList();
            return View(h);
        }
        public ActionResult Edit(int id)
        {
            var h = db.Abouts.Where(x => x.AboutId == id).FirstOrDefault();

            return View(h);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, About about)
        {
            if (ModelState.IsValid)
            {
                var hakkimizda = db.Abouts.Where(x => x.AboutId == id).SingleOrDefault();
                hakkimizda.Description = about.Description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(about);
        }
    }
}