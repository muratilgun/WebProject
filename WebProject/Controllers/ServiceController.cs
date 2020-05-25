using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebProject.Models.DataContext;
using WebProject.Models.Model;

namespace WebProject.Controllers
{
    public class ServiceController : Controller
    {
        private CorporateDBContext db = new CorporateDBContext();
        // GET: Service
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Service service, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                if (ImageURL != null)
                {

                    WebImage img = new WebImage(ImageURL.InputStream);
                    FileInfo imginfo = new FileInfo(ImageURL.FileName);
                    string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Service/" + logoname);

                    service.ImageURL = "/Uploads/Service/" + logoname;
                }
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenebilecek hizmet bulunamadı.";
            }
            var service = db.Services.Find(id);
            if (service== null)
            {
                return HttpNotFound();
            }
            return View(service);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Service service, HttpPostedFileBase ImageURL)
        {
           
            if (ModelState.IsValid)
            {
                var s = db.Services.Where(x => x.ServiceId == id).SingleOrDefault();
                if (ImageURL!=null)
                {
                    //daha önce kaydettiğimiz foto varsa bunu sil komutu
                    if (System.IO.File.Exists(Server.MapPath(s.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(s.ImageURL));

                    }
                    WebImage img = new WebImage(ImageURL.InputStream);
                    FileInfo imginfo = new FileInfo(ImageURL.FileName);
                    string servicename = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Uploads/Service/" + servicename);

                    s.ImageURL = "/Uploads/Service/" + servicename;
                }

                s.Title = service.Title;
                s.Description = service.Description;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if (id==null)
            {
                return HttpNotFound();

            }
            var s = db.Services.Find(id);
            if (s==null)
            {
                return HttpNotFound();
            }
            db.Services.Remove(s);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}