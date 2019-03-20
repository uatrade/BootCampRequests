using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;


namespace OrderManagemenet.Models
{
    public class RequestMetod
    {
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
                    //bool res = AddFileJson(fullPath);
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
    }
}