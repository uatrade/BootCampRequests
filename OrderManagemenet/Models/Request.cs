using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderManagemenet.Models
{
    public class Request
    {
        public int clientId { get; set; }
        public long requestId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
    }
}