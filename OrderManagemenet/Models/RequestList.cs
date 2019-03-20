using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderManagemenet.Models
{
    public class RequestList
    {
        public int id { get; set; }
        public IEnumerable<Request> requests { get; set; }
    }
}