using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStok.Models.Entity;

namespace MVCStok.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        private MVCDbStokEntities db = new MVCDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLSATISLAR.Where(m => m.DURUM == true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {

            List<SelectListItem> deger1 = (from i in db.TBLURUNLER.ToList()
                select new SelectListItem
                {
                    Text = i.URUNAD,
                    Value = i.URUNID.ToString()
                }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLMUSTERILER.ToList()
                select new SelectListItem
                {
                    Text = i.MUSTERIAD + " " +i.MUSTERISOYAD,
                    Value = i.MUSTERIID.ToString(),
                }).ToList();
            
            ViewBag.dgr2 = deger2;




            //List<SelectListItem> degerler_urun = (from i in db.TBLURUNLER.Where(i => i.DURUM == true).ToList()
            //    select new SelectListItem
            //    {

            //        Text = i.URUNAD,
            //        Value = i.URUNID.ToString(),

            //    }).ToList();
            //ViewBag.dgrurun = degerler_urun;
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(TBLSATISLAR p, TBLURUNLER p1)
        {
            

            var urun= db.TBLURUNLER.Where(x => x.URUNID== p.TBLURUNLER.URUNID).FirstOrDefault();

            p.TBLURUNLER= urun;
            var adsoyad = db.TBLMUSTERILER.Where(x => x.MUSTERIID== p.TBLMUSTERILER.MUSTERIID).FirstOrDefault();

            p.TBLMUSTERILER= adsoyad;


            db.TBLSATISLAR.Add(p);
            p.DURUM = true;
            urun.STOK -= p.ADET;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}