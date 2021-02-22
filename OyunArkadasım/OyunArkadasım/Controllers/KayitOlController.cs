using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OyunArkadasım.Models.Entity;

namespace OyunArkadasım.Controllers
{
    public class KayitOlController : Controller
    {
        // GET: KayitOl
        DBTezEntities db = new DBTezEntities();
        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(TBLUYE u)
        {
            if (!ModelState.IsValid)
            {
                return View(u);         
            }

            db.TBLUYE.Add(u);
            db.SaveChanges();
            return View("../Login/GirisYap");
        }
    }
}