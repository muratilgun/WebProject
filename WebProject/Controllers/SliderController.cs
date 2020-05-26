using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebProject.Models.DataContext;
using WebProject.Models.Model;

namespace WebProject.Controllers
{
    public class SliderController : Controller
    {
        private CorporateDBContext db = new CorporateDBContext();

        // GET: Slider
        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }

        // GET: Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SliderId,Title,Description,ImageURL")] Slider slider, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                if (ImageURL != null)
                {

                    WebImage img = new WebImage(ImageURL.InputStream);
                    FileInfo imginfo = new FileInfo(ImageURL.FileName);
                    string sliderimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(1024, 360);
                    img.Save("~/Uploads/Slider/" + sliderimgname);

                    slider.ImageURL = "/Uploads/Slider/" + sliderimgname;
                }
                db.Sliders.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SliderId,Title,Description,ImageURL")] Slider slider, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                var s = db.Sliders.Where(x => x.SliderId == slider.SliderId).SingleOrDefault();
                if (ImageURL != null)
                {
                    //daha önce kaydettiğimiz foto varsa bunu sil komutu
                    if (System.IO.File.Exists(Server.MapPath(s.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(s.ImageURL));

                    }
                    WebImage img = new WebImage(ImageURL.InputStream);
                    FileInfo imginfo = new FileInfo(ImageURL.FileName);
                    string sliderimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(1024,360);
                    img.Save("~/Uploads/Slider/" + sliderimgname);

                    s.ImageURL = "/Uploads/Slider/" + sliderimgname;
                }
                s.Title = slider.Title;
                s.Description = slider.Description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Sliders.Find(id);
            if (System.IO.File.Exists(Server.MapPath(slider.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath(slider.ImageURL));

            }
            db.Sliders.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
