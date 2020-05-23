using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebProject.Models.DataContext;
using WebProject.Models.Model;

namespace WebProject.Controllers
{
    public class KimlikController : Controller
    {
        CorporateDBContext db = new CorporateDBContext();

        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.SiteKimliks.ToList());
        }

     

        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            var kimlik = db.SiteKimliks.Where(x => x.SiteKimlikId == id).SingleOrDefault();
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] // html kodlarını doğrulama

        public ActionResult Edit(int id, SiteKimlik siteKimlik, HttpPostedFileBase LogoURL)
        {
            if (ModelState.IsValid)
            {
                var sk = db.SiteKimliks.Where(x => x.SiteKimlikId == id).SingleOrDefault();
                if (LogoURL != null)
                {
                    //daha önce kaydettiğimiz foto varsa bunu sil komutu
                    if (System.IO.File.Exists(Server.MapPath(sk.LogoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(sk.LogoURL));

                    }
                    WebImage img = new WebImage(LogoURL.InputStream);
                    FileInfo imginfo = new FileInfo(LogoURL.FileName);
                    string logoname = LogoURL.FileName+ imginfo.Extension;
                    img.Resize(300,200) ;
                    img.Save("~/Uploads/Kimlik/"  + logoname);

                    sk.LogoURL = "/Uploads/Kimlik/" + logoname;
                }
                sk.Title = siteKimlik.Title;
                sk.Keywords = siteKimlik.Keywords;
                sk.Description = siteKimlik.Description;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteKimlik);
        }

    }
}
