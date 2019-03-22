using OrderManagemenet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

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

        public ActionResult Download()
        {
            RequestMetod requestMetod = new RequestMetod();
            requestMetod.DownLoad();
            var resList = GetAllRequest();
            ViewBag.Download = "OK";
            return View("Index", resList);
        }

        public ActionResult Sort(string order_by)
        {
            RequestMetod requestMetod = new RequestMetod();
            if (order_by!=null)
            {
                var resList=requestMetod.SortRequests(order_by);
                return View("Index", resList);
            }
            return View("Index", requestMetod.GetRequests());
        }
        
    }
}