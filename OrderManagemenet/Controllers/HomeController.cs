using OrderManagemenet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderManagemenet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddFile(HttpPostedFileBase upload)
        {
            RequestsMetods orderMetods = new RequestsMetods();
            var res=orderMetods.AddFileJson(upload.FileName);
            ViewBag.File = "Загружен";
            return View("Index", res);
        }
        
    }
}