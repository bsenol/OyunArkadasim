using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OyunArkadasım.Models.Entity;

namespace OyunArkadasım.Controllers
{
    public class MesajlarController : Controller
    {
        // GET: Mesajlar

        DBTezEntities db = new DBTezEntities();

        [Authorize]
        public ActionResult Index() // kullanıcıya gelen mesajlar.
        {
            var uyeadi = (string)Session["KullaniciAdi"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.alici == uyeadi.ToString()).ToList();
            return View(mesajlar);
        }

        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJLAR m)
        {
            var uyeadi = (string)Session["KullaniciAdi"].ToString();
            m.gonderen = uyeadi.ToString();
            m.tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLMESAJLAR.Add(m);
            db.SaveChanges();
            return RedirectToAction("GidenMesajlar");
        }

        [Authorize]
        public ActionResult GidenMesajlar() 
        {
            var uyeadi = (string)Session["KullaniciAdi"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.gonderen == uyeadi.ToString()).ToList();
            return View(mesajlar);
        }

        [Authorize]
        public ActionResult MesajSil(int id)
        {
            var msj = db.TBLMESAJLAR.Find(id);
            db.TBLMESAJLAR.Remove(msj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpGet]
        public ActionResult AdminMesajGonder(int id)
        {
            var uye = db.TBLUYE.Find(id);
            ViewData["kullaniciadi"] = uye.KULLANICIADI;
            ViewData["uyeid"] = id;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AdminMesajGonder(TBLMESAJLAR m)
        {
            var uyeadi = (string)Session["KullaniciAdi"].ToString();
            m.gonderen = uyeadi.ToString();
            m.tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLMESAJLAR.Add(m);
            db.SaveChanges();
            var uye = db.TBLUYE.Find(ViewData["uyeid"]);
            return RedirectToAction("AdminGidenMesajlar", "Mesajlar");
        }

        [Authorize]
        public ActionResult AdminGelenMeseajlar() // kullanıcıya gelen mesajlar.
        {
            var uyeadi = (string)Session["KullaniciAdi"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.alici == uyeadi.ToString()).ToList();
            return View(mesajlar);
        }

        [Authorize]
        public ActionResult AdminGidenMesajlar()
        {
            var uyeadi = (string)Session["KullaniciAdi"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.gonderen == uyeadi.ToString()).ToList();
            return View(mesajlar);
        }

        [Authorize]
        public ActionResult AdminMesajSil(int id)
        {
            var msj = db.TBLMESAJLAR.Find(id);
            db.TBLMESAJLAR.Remove(msj);
            db.SaveChanges();
            return RedirectToAction("AdminGelenMeseajlar");
        }
    }
}