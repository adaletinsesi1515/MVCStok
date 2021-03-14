using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStok.Models.Entity;

namespace MVCStok.Controllers
{
    public class UrunController : Controller
    {
        private MVCDbStokEntities db = new MVCDbStokEntities();
        // GET: Urun
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.Where(m=>m.DURUM == true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult Yeniurun()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.Where(i => i.DURUM == true).ToList()
                select new SelectListItem
                {

                    Text = i.KATEGORIAD,
                    Value = i.KATEGORIID.ToString(),

                }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult Yeniurun(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;

            db.TBLURUNLER.Add(p1);
            p1.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            urun.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.Where(i=>i.DURUM==true).ToList()
                select new SelectListItem
                {
                    
                    Text = i.KATEGORIAD,
                    Value = i.KATEGORIID.ToString(),

                }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urunler = db.TBLURUNLER.Find(p.URUNID);
            urunler.URUNAD = p.URUNAD;
            urunler.MARKA = p.MARKA;
            urunler.FIYAT = p.FIYAT;
            urunler.STOK = p.STOK;
            urunler.DURUM = true;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urunler.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}