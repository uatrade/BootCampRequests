using OrderManagemenet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderManagemenet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var resList = GetAllRequest();
            return View(resList);
        }

        private List<Request> GetAllRequest()
        {
            RequestMetod requestMetod = new RequestMetod();
            var resList = requestMetod.GetRequests();
            return resList;
        }

        public ActionResult AddFile(HttpPostedFileBase upload)
        {
            string fileName = System.IO.Path.GetFileName(upload.FileName);
            upload.SaveAs(Server.MapPath("~/RequestFiles/" + fileName));
            RequestMetod requestMetod = new RequestMetod();
            bool res = requestMetod.CheckFileExtension(fileName);
            if(res)
            ViewBag.File = "Загружен";
            var resList = GetAllRequest();
            return View("Index", resList);
        }
        
    }
}