using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OyunArkadasım.Models.Entity;

namespace OyunArkadasım.Controllers
{
    public class ProfilimController : Controller
    {
        // GET: Profilim
        DBTezEntities db = new DBTezEntities();

        [HttpGet]
        [Authorize]
        public ActionResult Index()//Profilim Düzenleme
        {
            var kullaniciadi = (string)Session["KullaniciAdi"];
            var degerler = db.TBLUYE.FirstOrDefault(z => z.KULLANICIADI == kullaniciadi);
            return View(degerler);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index2(TBLUYE u)
        {

            var kullanici = (string)Session["KullaniciAdi"];
            var uye = db.TBLUYE.FirstOrDefault(x => x.KULLANICIADI == kullanici);
            uye.PAROLA = u.PAROLA;
            uye.AD = u.AD;
            uye.SOYAD = u.SOYAD;
            uye.YAS = u.YAS;
            uye.EMAIL = u.EMAIL;
            //uye.KULLANICIADI = u.KULLANICIADI;
            db.SaveChanges();
            return RedirectToAction("ProfilGoster");
        }

        [Authorize]
        public ActionResult ProfilGoster()// Üyenin Kendi Profilini Gösterme
        {
            var kullaniciadi = (string)Session["KullaniciAdi"];
            var degerler = db.TBLUYE.FirstOrDefault(z => z.KULLANICIADI == kullaniciadi);
            return View(degerler);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ProfilimOyunProfiliEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLOYUN.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.OYUNADI,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLDERECE.Where(d => d.OyunID == 1).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.DereceAdı,
                                               Value = i.DereceID.ToString()
                                           }).ToList();

            ViewBag.dgr2 = deger2;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfilimOyunProfiliEkle(TBLUYEOYUNBILGI n)
        {
            var prf = db.TBLOYUN.Where(p => p.ID == n.TBLOYUN.ID).FirstOrDefault();
            var drc = db.TBLDERECE.Where(p => p.DereceID == n.TBLDERECE.DereceID).FirstOrDefault();
            var k = (string)Session["UyeId"];
            var uye = db.TBLUYE.FirstOrDefault(z => z.Id.ToString() == k);
            n.TBLOYUN = prf;
            n.TBLDERECE = drc;
            n.UYEID = uye.Id;
            db.TBLUYEOYUNBILGI.Add(n);
            db.SaveChanges();

            return RedirectToAction("ProfilGoster");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ProfilimLolProfiliEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLROL.Where(r => r.OyunID == 1).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.RolAdı,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLDERECE.Where(d => d.OyunID == 1).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.DereceAdı,
                                               Value = i.DereceID.ToString()
                                           }).ToList();

            ViewBag.dgr2 = deger2;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfilimLolProfiliEkle(TBLUYEOYUNBILGI n)
        {
            var prf = db.TBLOYUN.Where(p => p.ID == 1).FirstOrDefault();
            var drc = db.TBLDERECE.Where(p => p.DereceID == n.TBLDERECE.DereceID).FirstOrDefault();
            var rol = db.TBLROL.Where(r => r.Id == n.TBLROL.Id).FirstOrDefault();
            var k = (string)Session["UyeId"];
            var uye = db.TBLUYE.FirstOrDefault(z => z.Id.ToString() == k);
            n.TBLOYUN = prf;
            n.TBLDERECE = drc;
            n.TBLROL = rol;
            n.UYEID = uye.Id;
            db.TBLUYEOYUNBILGI.Add(n);
            db.SaveChanges();

            return RedirectToAction("LolProfilimiGoster");
        }

        [Authorize]
        public ActionResult LolProfilimiGoster()
        {
            var prf = (string)Session["UyeId"];
            var prf2 = db.TBLUYEOYUNBILGI.FirstOrDefault(z => z.UYEID.ToString() == prf);

            try
            {
                var dd = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(1)) && (x.UYEID == prf2.UYEID)).FirstOrDefault();
                if (dd == null)
                {
                    return RedirectToAction("ProfilimLolProfiliEkle");
                }
                else
                    return View(dd);
            }
            catch (Exception)
            {
                return RedirectToAction("ProfilimLolProfiliEkle");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult LolProfilimiDuzenle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLROL.Where(r => r.OyunID == 1).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.RolAdı,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLDERECE.Where(d => d.OyunID == 1).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.DereceAdı,
                                               Value = i.DereceID.ToString()
                                           }).ToList();

            ViewBag.dgr2 = deger2;
            var kullaniciadi = (string)Session["KullaniciAdi"];
            var degerler = db.TBLUYE.FirstOrDefault(z => z.KULLANICIADI == kullaniciadi);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(1)) && (x.UYEID == degerler.Id)).FirstOrDefault();
            return View(op);
        }

        [Authorize]
        [HttpPost]
        public ActionResult LolProfilimiDuzenle2(TBLUYEOYUNBILGI p)
        {
            var k = (string)Session["UyeId"];
            var u = db.TBLUYE.FirstOrDefault(z => z.Id.ToString() == k);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(1)) && (x.UYEID == u.Id)).FirstOrDefault();
            var drc = db.TBLDERECE.Where(x => x.DereceID == p.TBLDERECE.DereceID).FirstOrDefault();
            var rol = db.TBLROL.Where(x => x.Id == p.TBLROL.Id).FirstOrDefault();
            op.NICKNAME = p.NICKNAME;
            op.TBLDERECE = drc;
            op.TBLROL = rol;
            db.SaveChanges();
            return RedirectToAction("LolProfilimiGoster");
        }

        [Authorize]
        public ActionResult LolProfiliniSil()
        {
            var k = (string)Session["UyeId"];
            var u = db.TBLUYE.FirstOrDefault(z => z.Id.ToString() == k);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(1)) && (x.UYEID == u.Id)).FirstOrDefault();
            db.TBLUYEOYUNBILGI.Remove(op);
            db.SaveChanges();
            return RedirectToAction("ProfilGoster");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ProfilimValorantProfiliEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLROL.Where(r => r.OyunID == 3).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.RolAdı,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLDERECE.Where(d => d.OyunID == 3).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.DereceAdı,
                                               Value = i.DereceID.ToString()
                                           }).ToList();

            ViewBag.dgr2 = deger2;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfilimValorantProfiliEkle(TBLUYEOYUNBILGI n)
        {
            var prf = db.TBLOYUN.Where(p => p.ID == 3).FirstOrDefault();
            var drc = db.TBLDERECE.Where(p => p.DereceID == n.TBLDERECE.DereceID).FirstOrDefault();
            var rol = db.TBLROL.Where(r => r.Id == n.TBLROL.Id).FirstOrDefault();
            var k = (string)Session["UyeId"];
            var uye = db.TBLUYE.FirstOrDefault(z => z.Id.ToString() == k);
            n.TBLOYUN = prf;
            n.TBLDERECE = drc;
            n.TBLROL = rol;
            n.UYEID = uye.Id;
            db.TBLUYEOYUNBILGI.Add(n);
            db.SaveChanges();

            return RedirectToAction("ValorantProfilimiGoster");
        }

        [Authorize]
        public ActionResult ValorantProfilimiGoster()
        {
            var prf = (string)Session["UyeId"];
            var prf2 = db.TBLUYEOYUNBILGI.FirstOrDefault(z => z.UYEID.ToString() == prf);

            try
            {
                var dd = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(3)) && (x.UYEID == prf2.UYEID)).FirstOrDefault();
                if (dd == null)
                {
                    return RedirectToAction("ProfilimValorantProfiliEkle");
                }
                else
                    return View(dd);
            }
            catch (Exception)
            {
                return RedirectToAction("ProfilimValorantProfiliEkle");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult ValorantProfilimiDuzenle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLROL.Where(r => r.OyunID == 3).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.RolAdı,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLDERECE.Where(d => d.OyunID == 3).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.DereceAdı,
                                               Value = i.DereceID.ToString()
                                           }).ToList();

            ViewBag.dgr2 = deger2;
            var kullaniciadi = (string)Session["KullaniciAdi"];
            var degerler = db.TBLUYE.FirstOrDefault(z => z.KULLANICIADI == kullaniciadi);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(3)) && (x.UYEID == degerler.Id)).FirstOrDefault();
            return View(op);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ValorantProfilimiDuzenle2(TBLUYEOYUNBILGI p)
        {
            var k = (string)Session["UyeId"];
            var u = db.TBLUYE.FirstOrDefault(z => z.Id.ToString() == k);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(3)) && (x.UYEID == u.Id)).FirstOrDefault();
            var drc = db.TBLDERECE.Where(x => x.DereceID == p.TBLDERECE.DereceID).FirstOrDefault();
            var rol = db.TBLROL.Where(x => x.Id == p.TBLROL.Id).FirstOrDefault();
            op.NICKNAME = p.NICKNAME;
            op.TBLDERECE = drc;
            op.TBLROL = rol;
            db.SaveChanges();
            return RedirectToAction("ValorantProfilimiGoster");
        }

        [Authorize]
        public ActionResult ValorantProfiliniSil()
        {
            var k = (string)Session["UyeId"];
            var u = db.TBLUYE.FirstOrDefault(z => z.Id.ToString() == k);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(3)) && (x.UYEID == u.Id)).FirstOrDefault();
            db.TBLUYEOYUNBILGI.Remove(op);
            db.SaveChanges();
            return RedirectToAction("ProfilGoster");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ProfilimCsProfiliEkle()
        {

            List<SelectListItem> deger2 = (from i in db.TBLDERECE.Where(d => d.OyunID == 2).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.DereceAdı,
                                               Value = i.DereceID.ToString()
                                           }).ToList();

            ViewBag.dgr2 = deger2;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfilimCsProfiliEkle(TBLUYEOYUNBILGI n)
        {
            var prf = db.TBLOYUN.Where(p => p.ID == 2).FirstOrDefault();
            var drc = db.TBLDERECE.Where(p => p.DereceID == n.TBLDERECE.DereceID).FirstOrDefault();
            var k = (string)Session["UyeId"];
            var uye = db.TBLUYE.FirstOrDefault(z => z.Id.ToString() == k);
            n.TBLOYUN = prf;
            n.TBLDERECE = drc;
            n.UYEID = uye.Id;
            db.TBLUYEOYUNBILGI.Add(n);
            db.SaveChanges();

            return RedirectToAction("CsProfilimiGoster");
        }

        [Authorize]
        public ActionResult CsProfilimiGoster()
        {
            var prf = (string)Session["UyeId"];
            var prf2 = db.TBLUYEOYUNBILGI.FirstOrDefault(z => z.UYEID.ToString() == prf);

            try
            {
                var dd = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(2)) && (x.UYEID == prf2.UYEID)).FirstOrDefault();
                if (dd == null)
                {
                    return RedirectToAction("ProfilimCsProfiliEkle");
                }
                else
                    return View(dd);
            }
            catch (Exception)
            {
                return RedirectToAction("ProfilimCsProfiliEkle");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult CsProfilimiDuzenle()
        {
            List<SelectListItem> deger2 = (from i in db.TBLDERECE.Where(d => d.OyunID == 2).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.DereceAdı,
                                               Value = i.DereceID.ToString()
                                           }).ToList();

            ViewBag.dgr2 = deger2;
            var kullaniciadi = (string)Session["KullaniciAdi"];
            var degerler = db.TBLUYE.FirstOrDefault(z => z.KULLANICIADI == kullaniciadi);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(2)) && (x.UYEID == degerler.Id)).FirstOrDefault();
            return View(op);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CsProfilimiDuzenle2(TBLUYEOYUNBILGI p)
        {
            var k = (string)Session["UyeId"];
            var u = db.TBLUYE.FirstOrDefault(z => z.Id.ToString() == k);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(2)) && (x.UYEID == u.Id)).FirstOrDefault();
            var drc = db.TBLDERECE.Where(x => x.DereceID == p.TBLDERECE.DereceID).FirstOrDefault();
            op.NICKNAME = p.NICKNAME;
            op.TBLDERECE = drc;
            db.SaveChanges();
            return RedirectToAction("CsProfilimiGoster");
        }
        [Authorize]
        public ActionResult CsProfiliniSil()
        {
            var k = (string)Session["UyeId"];
            var u = db.TBLUYE.FirstOrDefault(z => z.Id.ToString() == k);
            var op = db.TBLUYEOYUNBILGI.Where(x => (x.OYUNID.Equals(2)) && (x.UYEID == u.Id)).FirstOrDefault();
            db.TBLUYEOYUNBILGI.Remove(op);
            db.SaveChanges();
            return RedirectToAction("ProfilGoster");
        }

    }
}