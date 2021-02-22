using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OyunArkadasım.Models.Entity;
using System.Web.Security;


namespace OyunArkadasım.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DBTezEntities db = new DBTezEntities();
        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(TBLUYE u)
        {
            var bilgiler = db.TBLUYE.FirstOrDefault(x => x.KULLANICIADI == u.KULLANICIADI && x.PAROLA == u.PAROLA);

            if (bilgiler != null)
            {

                FormsAuthentication.SetAuthCookie(bilgiler.KULLANICIADI, false);
                Session["KullaniciAdi"] = bilgiler.KULLANICIADI.ToString();
                Session["UyeId"] = bilgiler.Id.ToString();
                return RedirectToAction("Index", "Anasayfa");
            }
            else
            {
                ViewBag.uyari = "Kullanıcı Adı yada şifre yanlış.";
                return View();
            }
        }

        public ActionResult AdminGiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminGiris(TBLADMIN a)
        {
            var login = db.TBLADMIN.Where(x => x.KullaniciAdi == a.KullaniciAdi && x.Sifre == a.Sifre).SingleOrDefault();
            if (login != null)
            {
                Session["KullaniciAdi"] = login.KullaniciAdi.ToString();
                Session["adminid"] = login.ID.ToString();
                FormsAuthentication.SetAuthCookie(login.KullaniciAdi, false);
                return RedirectToAction("IndexAdmin", "Anasayfa");
            }
            else
            {
                ViewBag.uyari = "Kullanıcı Adı yada şifre yanlış.";
                return View(a);
            }
        }

        public ActionResult CikisYap()
        {
            Session["KullaniciAdi"] = null;
            Session["adminid"] = null;
            Session["UyeId"] = null;
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap");
        }
    }
}