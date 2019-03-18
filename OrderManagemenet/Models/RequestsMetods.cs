using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OrderManagemenet.Models
{
    public class RequestsMetods
    {

        public RequestList AddFileJson(string fileName)
        {
            RequestList res = new RequestList();
            string str = @"C:/Users/Oleksandr/Desktop/" + fileName;
            using (StreamReader r = new StreamReader(str))
            {
                string jsonString = r.ReadToEnd();
                res=JsonConvert.DeserializeObject<RequestList>(jsonString);
            }
            return res;
        }
        
    }
}