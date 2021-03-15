using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MVCStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        private MVCDbStokEntities db = new MVCDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLKATEGORILER.Where(m=>m.DURUM == true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult Yenikategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yenikategori(TBLKATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("Yenikategori");
            }
            db.TBLKATEGORILER.Add(p1);
            p1.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            kategori.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir", ktgr);
        }

        public ActionResult Guncelle(TBLKATEGORILER p2)
        {
            var ktg = db.TBLKATEGORILER.Find(p2.KATEGORIID);
            ktg.KATEGORIAD = p2.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}