using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStok.Models.Entity;

namespace MVCStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        private MVCDbStokEntities db = new MVCDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLMUSTERILER.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult Yenimusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yenimusteri(TBLMUSTERILER p1)
        {
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}