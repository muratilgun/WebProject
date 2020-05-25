using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebProject.Models;
using WebProject.Models.DataContext;
using WebProject.Models.Model;

namespace WebProject.Controllers
{

    public class AdminController : Controller
    {
        CorporateDBContext db = new CorporateDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            var sorgu = db.Categories.ToList();

            return View(sorgu);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {

            var login = db.Admins.Where(x => x.Email == admin.Email).SingleOrDefault();
            if (login != null)
            {
                if (login.Email == admin.Email && login.Password == admin.Password)
                {
                    Session["adminid"] = login.AdminId;
                    Session["email"] = login.Password;

                    return RedirectToAction("Index", "Admin");
                }


            }
            ViewBag.Uyari = "Kullanıcı adı yada şifre yanlış";
                return View(admin);
        }

        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["email"] = null;
            Session.Abandon();

            return RedirectToAction("Login", "Admin");
        }


    }

}