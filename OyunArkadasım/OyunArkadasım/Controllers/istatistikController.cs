using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OyunArkadasım.Models.Entity;
namespace OyunArkadasım.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        DBTezEntities db = new DBTezEntities();
        public ActionResult istatistik()
        {
            var uyesayisi = db.TBLUYE.Count();
            ViewBag.dgr1 = uyesayisi;
            var opsayisi = db.TBLUYEOYUNBILGI.Count();
            ViewBag.dgr2 = opsayisi;
            var mesajsayisi = db.TBLMESAJLAR.Count();
            ViewBag.dgr3 = mesajsayisi;
            var loltop1 = db.TBLUYEOYUNBILGI.Where(x => x.OYUNID.Equals(1)).OrderByDescending(z => z.PUAN).Select(y => y.NICKNAME).FirstOrDefault();
            ViewBag.dgr4 = loltop1;
            var cstop1 = db.TBLUYEOYUNBILGI.Where(x => x.OYUNID.Equals(2)).OrderByDescending(z => z.PUAN).Select(y => y.NICKNAME).FirstOrDefault();
            ViewBag.dgr5 = cstop1;
            var valotop1 = db.TBLUYEOYUNBILGI.Where(x => x.OYUNID.Equals(3)).OrderByDescending(z => z.PUAN).Select(y => y.NICKNAME).FirstOrDefault();
            ViewBag.dgr6 = valotop1;
            return View();
        }
    }
}