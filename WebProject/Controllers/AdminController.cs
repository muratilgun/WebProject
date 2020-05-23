using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;
using WebProject.Models.DataContext;

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
    }
}