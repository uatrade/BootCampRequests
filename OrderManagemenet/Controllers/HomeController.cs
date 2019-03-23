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
            RequestMetod requestMetod = new RequestMetod();
            try
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                upload.SaveAs(Server.MapPath("~/RequestFiles/" + fileName));
                bool res = requestMetod.CheckFileExtension(fileName);
                if (res)
                    ViewBag.File = "Загружен";
                var resList = GetAllRequest();
                return View("Index", resList);
            }
            catch (Exception)
            {
                return View("Index", requestMetod.GetRequests());
            }
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
        public ActionResult ClientInfo(string clientId)
        {
            RequestMetod requestMetod = new RequestMetod();
            try
            {
                if (clientId != null)
                {
                    var resList = requestMetod.GetClientInfo(clientId);
                    if (resList.Count !=0)
                    {
                        return View("Index", resList);
                    }
                }
            }
            catch (Exception)
            {
                return View("Index", requestMetod.GetRequests());
            }
            return View("Index", requestMetod.GetRequests());
        }

        public ActionResult RequestName(string name)
        {
            RequestMetod requestMetod = new RequestMetod();
            try
            {
                if (name != null)
                {
                    var resList = requestMetod.GetRequestNameInfo(name);
                    if (resList.Count != 0)
                    {
                        return View("Index", resList);
                    }
                }
            }
            catch (Exception)
            {
                return View("Index", requestMetod.GetRequests());
            }
            return View("Index", requestMetod.GetRequests());
        }

    }
}