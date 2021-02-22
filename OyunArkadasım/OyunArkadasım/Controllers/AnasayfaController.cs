using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OyunArkadasım.Models.Entity;

namespace OyunArkadasım.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa

        DBTezEntities db = new DBTezEntities();
        public ActionResult Index()
        {
            var prf1 = db.Top5lol();
            ViewBag.dgr1 = prf1;
            var prf2 = db.Top5cs();
            ViewBag.dgr2 = prf2;
            var prf3 = db.Top5valo();
            ViewBag.dgr3 = prf3;
            return View();
        }

        public ActionResult IndexAdmin()
        {
            var prf1 = db.Top5lol();
            ViewBag.dgr1 = prf1;
            var prf2 = db.Top5cs();
            ViewBag.dgr2 = prf2;
            var prf3 = db.Top5valo();
            ViewBag.dgr3 = prf3;
            return View();
        }

    }
}