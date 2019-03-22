using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;
//using System.Xml.Serialization;

namespace OrderManagemenet.Models
{
    public class RequestMetod
    {
        static bool sortTopDown = false;  // change Sort ASC to DESC when NextClick
        public bool CheckFileExtension(string fileName)    // check type of file
        {
            try
            {
                string fullPath = HostingEnvironment.ApplicationPhysicalPath + "RequestFiles\\" + fileName;

                if (Path.GetExtension(fileName) == ".json")
                {
                    bool res = AddFileJson(fullPath);
                    return res;
                }
                if (Path.GetExtension(fileName) == ".csv")
                {
                    bool res = AddFileCsv(fullPath);
                    return res;
                }
                if (Path.GetExtension(fileName) == ".xml")
                {
                    bool res = AddFileXml(fullPath);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool AddFileJson(string fullPath)   //add json file
        {
            try
            {
                RequestList requestList = new RequestList();
                //string fullPath = HostingEnvironment.ApplicationPhysicalPath+ "RequestFiles\\" + fileName;
                using (StreamReader r = new StreamReader(fullPath))
                {
                    string jsonString = r.ReadToEnd();
                    requestList = JsonConvert.DeserializeObject<RequestList>(jsonString);                   
                }
                if (requestList != null)
                {
                    using (OrderContext db = new OrderContext())
                    {
                        foreach (var item in requestList.requests)
                            db.Requests.Add(item);
                            db.SaveChanges();
                        return true;
                    }
                }                 
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddFileCsv(string fullPath)     //CSV
        {
            try
            {
                using (TextFieldParser parser = new TextFieldParser(fullPath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    int countField=0;
                    while (!parser.EndOfData)
                    {
                            string[] arrOrders = parser.ReadFields();
                            foreach (string order in arrOrders)
                            {
                            if(countField==0)
                            {
                                countField++;
                                break;
                            }
                                using (OrderContext db = new OrderContext())
                                {
                                    Request request = new Request();
                                    request.clientId = Int32.Parse(arrOrders[0]);
                                    request.requestId = Int64.Parse(arrOrders[1]);
                                    request.name = arrOrders[2];
                                    request.quantity = Int32.Parse(arrOrders[3]);
                                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                                    request.price = double.Parse(arrOrders[4]);
                                    db.Requests.Add(request);
                                    db.SaveChanges();
                                break;
                            }
                            }
                        }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddFileXml(string fullPath)
        {
            try
            {
                XDocument doc = XDocument.Load(fullPath);
                foreach(XElement el in doc.Root.Elements())
                {
                    Request request = new Request();
                    foreach (XElement element in el.Elements())
                    {
                        if(element.Name== "clientId")
                            request.clientId = Int32.Parse(element.Value);
                        if(element.Name == "requestId")
                            request.requestId = Int64.Parse(element.Value);
                        if (element.Name == "name")
                            request.name = element.Value;
                        if (element.Name == "quantity")
                            request.quantity = Int32.Parse(element.Value);
                        if (element.Name == "price")
                        {
                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                            request.price = double.Parse(element.Value);
                        }
                    }
                    using (OrderContext db = new OrderContext())
                    {
                        db.Requests.Add(request);
                        db.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Request> GetRequests()
        {
           try
            {
                using (OrderContext db = new OrderContext())
                {
                    var resList= db.Requests.ToList();
                    return resList;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Request> SortRequests(string order_by)
        {
            try
            {
                var res = GetRequests();
                if (res != null&& order_by=="name"&& sortTopDown==false)
                {
                    sortTopDown = true;
                        return res.OrderBy(x => x.name).ToList();
                }
                if (res != null && order_by == "name"&& sortTopDown==true)
                {
                    sortTopDown = false;
                    return res.OrderByDescending(x => x.name).ToList();
                }

                if (res != null && order_by == "requestId"&& sortTopDown == false)
                {
                    sortTopDown = true;
                    return res.OrderBy(x => x.requestId).ToList();
                }
                if (res != null && order_by == "requestId" && sortTopDown == true)
                {
                    sortTopDown = false;
                    return res.OrderByDescending(x => x.requestId).ToList();
                }
                if (res != null && order_by == "clientId" && sortTopDown == false)
                {
                    sortTopDown = true;
                    return res.OrderBy(x => x.clientId).ToList();
                }
                if (res != null && order_by == "clientId" && sortTopDown == true)
                {
                    sortTopDown = false;
                    return res.OrderByDescending(x => x.clientId).ToList();
                }
                if (res != null && order_by == "quantity" && sortTopDown == false)
                {
                    sortTopDown = true;
                    return res.OrderBy(x => x.quantity).ToList();
                }
                if (res != null && order_by == "quantity" && sortTopDown == true)
                {
                    sortTopDown = false;
                    return res.OrderByDescending(x => x.quantity).ToList();
                }
                if (res != null && order_by == "price" && sortTopDown == false)
                {
                    sortTopDown = true;
                    return res.OrderBy(x => x.price).ToList();
                }
                if (res != null && order_by == "price" && sortTopDown == true)
                {
                    sortTopDown = false;
                    return res.OrderByDescending(x => x.price).ToList();
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DownLoad()
        {
            try
            {
                var dataFile = HttpContext.Current.Server.MapPath("~/Download/data.json");

                using (OrderContext db = new OrderContext())
                {
                    var res = db.Requests.ToList();
                    string json = JsonConvert.SerializeObject(res);
                    if(json==null)
                        return false;
                    File.WriteAllText(dataFile, json);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}