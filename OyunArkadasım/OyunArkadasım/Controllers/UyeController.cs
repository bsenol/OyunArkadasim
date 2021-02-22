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
    public class UyeController : Controller
    {
        // GET: Uye
        DBTezEntities db = new DBTezEntities();

        public ActionResult Index(string u, int sayfa = 1)
        {
            var uye = from k in db.TBLUYE select k;
            if (!string.IsNullOrEmpty(u))
            {
                uye = uye.Where(m => m.KULLANICIADI.Contains(u));
            }
            return View(uye.ToList().ToPagedList(sayfa,7));

        }

        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(TBLUYE u)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
            }
            db.TBLUYE.Add(u);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UyeSil(int id)
        {
            var uye = db.TBLUYE.Find(id);
            db.TBLUYE.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UyeGuncelle(int id)//UyeGetirme işlemi
        {
            var uye = db.TBLUYE.Find(id);
            ViewBag.dgr = id;
            return View("UyeGuncelle", uye);
        }

        public ActionResult UyeGuncelle1(TBLUYE u)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeGuncelle");
            }
            var uye = db.TBLUYE.Find(u.Id);
            uye.Id = u.Id;
            uye.KULLANICIADI = u.KULLANICIADI;
            uye.PAROLA = u.PAROLA;
            uye.EMAIL = u.EMAIL;
            uye.YAS = u.YAS;
            uye.AD = u.AD;
            uye.SOYAD = u.SOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult TumUyeleriGoruntule(int sayfa = 1)
        {
            var uyeler = db.TBLUYE.ToList();
            return View(uyeler);
        }

        [Authorize]
        public ActionResult UyeProfilGoster(int id)
        {
            var p = db.TBLUYE.Find(id);
            ViewData["uyeid"] = id;
            ViewData["kullaniciadi"] = p.KULLANICIADI;
            return View("UyeProfilGoster", p);
        }

        [Authorize]
        public ActionResult UyeLolProfiliGoster(int id)
        {
            var uye = db.TBLUYE.Find(id);
            ViewData["kullaniciadi"] = uye.KULLANICIADI;
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(1)) && (x.UYEID == uye.Id)).FirstOrDefault();
            ViewData["uyeid"] = id;
            return View("UyeLolProfiliGoster", op);
        }

        [Authorize]
        public ActionResult UyeCsProfiliGoster(int id)
        {
            var uye = db.TBLUYE.Find(id);
            ViewData["kullaniciadi"] = uye.KULLANICIADI;
            ViewData["uyeid"] = id;

            try
            {
                var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(2)) && (x.UYEID == uye.Id)).FirstOrDefault();
                if (op == null)
                {
                    return RedirectToAction("UyeProfilGoster",uye);
                }
                else
                    return View("UyeCsProfiliGoster", op);
            }
            catch (Exception)
            {
                return RedirectToAction("UyeProfilGoster", uye);
            }

            
        }

        [Authorize]
        public ActionResult UyeValorantProfiliGoster(int id)
        {
            var uye = db.TBLUYE.Find(id);
            ViewData["kullaniciadi"] = uye.KULLANICIADI;
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(3)) && (x.UYEID == uye.Id)).FirstOrDefault();
            ViewData["uyeid"] = id;
            return View("UyeValorantProfiliGoster", op);
        }

        [Authorize]
        public ActionResult LolPuanlama(int id)
        {
            var uye = db.TBLUYE.Find(id);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(1)) && (x.UYEID == uye.Id)).FirstOrDefault();
            ViewData["kullaniciadi"] = uye.KULLANICIADI;
            ViewData["uyeid"] = id;
            op.PUAN = op.PUAN + 1;
            db.SaveChanges();
            return View("UyeProfilGoster", uye);
        }

        [Authorize]
        public ActionResult LolPuanlama2(int id)
        {
            var uye = db.TBLUYE.Find(id);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(1)) && (x.UYEID == uye.Id)).FirstOrDefault();
            ViewData["kullaniciadi"] = uye.KULLANICIADI;
            ViewData["uyeid"] = id;
            op.PUAN = op.PUAN - 1;
            if (op.PUAN <= 0)
            {
                op.PUAN = 0;
            }
            db.SaveChanges();
            return View("UyeProfilGoster", uye);
        }

        [Authorize]
        public ActionResult CsPuanlama(int id)
        {
            var uye = db.TBLUYE.Find(id);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(2)) && (x.UYEID == uye.Id)).FirstOrDefault();
            ViewData["kullaniciadi"] = uye.KULLANICIADI;
            ViewData["uyeid"] = id;
            op.PUAN = op.PUAN + 1;
            db.SaveChanges();
            return View("UyeProfilGoster", uye);
        }

        [Authorize]
        public ActionResult CsPuanlama2(int id)
        {
            var uye = db.TBLUYE.Find(id);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(2)) && (x.UYEID == uye.Id)).FirstOrDefault();
            ViewData["kullaniciadi"] = uye.KULLANICIADI;
            ViewData["uyeid"] = id;
            op.PUAN = op.PUAN - 1;
            if (op.PUAN <= 0)
            {
                op.PUAN = 0;
            }
            db.SaveChanges();
            return View("UyeProfilGoster", uye);
        }

        [Authorize]
        public ActionResult ValorantPuanlama(int id)
        {
            var uye = db.TBLUYE.Find(id);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(3)) && (x.UYEID == uye.Id)).FirstOrDefault();
            ViewData["kullaniciadi"] = uye.KULLANICIADI;
            ViewData["uyeid"] = id;
            op.PUAN = op.PUAN + 1;
            db.SaveChanges();
            return View("UyeProfilGoster", uye);
        }

        [Authorize]
        public ActionResult ValorantPuanlama2(int id)
        {
            var uye = db.TBLUYE.Find(id);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(3)) && (x.UYEID == uye.Id)).FirstOrDefault();
            ViewData["kullaniciadi"] = uye.KULLANICIADI;
            ViewData["uyeid"] = id;
            op.PUAN = op.PUAN - 1;
            if (op.PUAN <= 0)
            {
                op.PUAN = 0;
            }
            db.SaveChanges();
            return View("UyeProfilGoster", uye);
        }

        public ActionResult LolSıralama()
        {
            var lolprofilleri = db.TBLUYEOYUNBILGI.Where(x => x.OYUNID.Equals(1)).OrderByDescending(i => new { i.PUAN,i.DERECESİ }).ToList();

            return View(lolprofilleri);
        }


        public ActionResult CsSıralama()
        {
            var csprofilleri = db.TBLUYEOYUNBILGI.Where(x => x.OYUNID.Equals(2)).OrderByDescending(i => new { i.PUAN, i.DERECESİ }).ToList();

            return View(csprofilleri);
        }

        public ActionResult ValorantSıralama()
        {
            var valoprofilleri = db.TBLUYEOYUNBILGI.Where(x => x.OYUNID.Equals(3)).OrderByDescending(i => new { i.PUAN, i.DERECESİ }).ToList();

            return View(valoprofilleri);
        }

        [Authorize]
        [HttpGet]
        public ActionResult MesajGonder(int id)
        {
            var uye = db.TBLUYE.Find(id);
            ViewData["kullaniciadi"] = uye.KULLANICIADI;
            ViewData["uyeid"] = id;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult MesajGonder(TBLMESAJLAR m)
        {
            var uyeadi = (string)Session["KullaniciAdi"].ToString();
            m.gonderen = uyeadi.ToString();
            m.tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLMESAJLAR.Add(m);
            db.SaveChanges();
            var uye = db.TBLUYE.Find(ViewData["uyeid"]);
            return RedirectToAction("GidenMesajlar", "Mesajlar");
        }
    }
}