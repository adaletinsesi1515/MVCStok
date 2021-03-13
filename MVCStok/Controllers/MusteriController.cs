﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MVCStok.Models.Entity;

namespace MVCStok.Controllers
{
    public class MusteriController : Controller
    {
        

        // GET: Musteri
        private MVCDbStokEntities db = new MVCDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLMUSTERILER.Where(m=>m.DURUM == true).ToList();
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
            //mükerrer kontrolü ama eksik burası
            //var kontrol = db.TBLMUSTERILER.Where(p => p.MUSTERIAD == p1.MUSTERIAD).Count();
            //if (kontrol == 0)
            //{
            //    db.TBLMUSTERILER.Add(p1);
            //    p1.DURUM = true;
            //    db.SaveChanges();
                
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return RedirectToAction("Yenimusteri");
                
            //}

            db.TBLMUSTERILER.Add(p1);
            p1.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Sil(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            musteri.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        
    }
}