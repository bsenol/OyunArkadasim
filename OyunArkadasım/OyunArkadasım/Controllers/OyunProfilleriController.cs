using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OyunArkadasım.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace OyunArkadasım.Controllers
{
    public class OyunProfilleriController : Controller
    {
        // GET: OyunProfilleri
        DBTezEntities db = new DBTezEntities();

        public ActionResult Index(int id) //uye id sine göre oyun profili getirme.
        {
            var profiller = db.TBLUYEOYUNBILGI.Where(x => x.UYEID == id).ToList();
            return View(profiller);
        }
        [HttpGet]
        public ActionResult OyunProfiliEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLOYUN.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.OYUNADI,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLUYE.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Id.ToString(),
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            List<SelectListItem> deger3 = (from i in db.TBLDERECE.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.DereceAdı,
                                               Value = i.DereceID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            List<SelectListItem> deger4 = (from i in db.TBLROL.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.RolAdı,
                                               Value = i.Id.ToString()
                                           }).ToList();

            ViewBag.dgr4 = deger4;
            return View();
        }
        [HttpPost]
        public ActionResult OyunProfiliEkle(TBLUYEOYUNBILGI n)
        {
            var prf = db.TBLOYUN.Where(p => p.ID == n.TBLOYUN.ID).FirstOrDefault();
            var uye = db.TBLUYE.Where(u => u.Id == n.TBLUYE.Id).FirstOrDefault();
            var drc = db.TBLDERECE.Where(u => u.DereceID == n.TBLDERECE.DereceID).FirstOrDefault();
            var rol = db.TBLROL.Where(u => u.Id == n.TBLROL.Id).FirstOrDefault();
            n.TBLOYUN = prf;
            n.TBLUYE = uye;
            n.TBLDERECE = drc;
            n.TBLROL = rol;
            db.TBLUYEOYUNBILGI.Add(n);
            db.SaveChanges();
            return RedirectToAction("Index", uye);
        }
        public ActionResult OyunProfiliSil(int id)
        {
            var p = db.TBLUYEOYUNBILGI.Find(id);
            var uye = db.TBLUYE.Where(u => u.Id == p.TBLUYE.Id).FirstOrDefault();
            db.TBLUYEOYUNBILGI.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index", uye);
        }
        public ActionResult OyunProfiliGetir(int id)
        {
            var p = db.TBLUYEOYUNBILGI.Find(id);
            List<SelectListItem> deger1 = (from i in db.TBLDERECE.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.DereceAdı,
                                               Value = i.DereceID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from i in db.TBLROL.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.RolAdı,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View("OyunProfiliGetir", p);
        }
        public ActionResult OyunProfiliGuncelle(TBLUYEOYUNBILGI p)
        {
            var prf = db.TBLUYEOYUNBILGI.Find(p.ID);
            var uye = db.TBLUYE.Where(y => y.Id == p.TBLUYE.Id).FirstOrDefault();
            var drc = db.TBLDERECE.Where(x => x.DereceID == p.TBLDERECE.DereceID).FirstOrDefault();
            var rol = db.TBLROL.Where(x => x.Id == p.TBLROL.Id).FirstOrDefault();
            prf.UYEID = uye.Id;
            prf.TBLDERECE = drc;
            prf.TBLROL = rol;
            prf.NICKNAME = p.NICKNAME;
            prf.PUAN = p.PUAN;
            db.SaveChanges();

            return RedirectToAction("Index", uye);

        }

        [Authorize]
        public ActionResult TumOyuncuProfilleri(string p, int sayfa = 1)//admin paneli için
        {
            //var profiller = db.TBLUYEOYUNBILGI.ToList().ToPagedList(sayfa, 7); ;
            //return View(profiller);
            var profiller = from k in db.TBLUYEOYUNBILGI select k;
            if (!string.IsNullOrEmpty(p))
            {
                profiller = profiller.Where(m => m.NICKNAME.Contains(p));
            }
            return View(profiller.ToList().ToPagedList(sayfa, 7));
        }

        [Authorize]
        public ActionResult LolProfiliBul(int sayfa = 1)
        {
            var lolprofilleri = db.TBLUYEOYUNBILGI.Where(x => x.OYUNID.Equals(1)).ToList();
            return View(lolprofilleri);
        }


        public JsonResult LolDereceGetirAjax()
        {

            var drc = db.TBLDERECE.Where(x => x.OyunID == 1).Select(x => x.DereceAdı);
            return Json(drc);

        }

        public JsonResult LolRolGetirAjax()
        {
            var roller = db.TBLROL.Where(x => x.OyunID == 1).Select(x => x.RolAdı);
            return Json(roller);

        }
        [Authorize]
        public ActionResult CsProfiliBul(int sayfa = 1)
        {
            var csprofilleri = db.TBLUYEOYUNBILGI.Where(x => x.OYUNID.Equals(2)).ToList();
            return View(csprofilleri);
        }

        public JsonResult CsDereceGetirAjax()
        {
            var drc = db.TBLDERECE.Where(x => x.OyunID == 2).Select(x => x.DereceAdı);
            return Json(drc);

        }

        [Authorize]
        public ActionResult ValorantProfiliBul(int sayfa = 1)
        {
            var valprofilleri = db.TBLUYEOYUNBILGI.Where(x => x.OYUNID.Equals(3)).ToList();
            return View(valprofilleri);
        }

        [Authorize]
        public JsonResult ValorantDereceGetirAjax()
        {
            var drc = db.TBLDERECE.Where(x => x.OyunID == 3).Select(x => x.DereceAdı);
            return Json(drc);

        }

        [Authorize]
        public JsonResult ValorantRolGetirAjax()
        {
            var roller = db.TBLROL.Where(x => x.OyunID == 3).Select(x => x.RolAdı);
            return Json(roller);

        }
    }
}