using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OyunArkadasım.Models.Entity;

namespace OyunArkadasım.Controllers
{
    public class OyunlarController : Controller
    {
        // GET: Oyunlar
        DBTezEntities db = new DBTezEntities();
        public ActionResult Index()
        {
            return View();
        }
    }
}