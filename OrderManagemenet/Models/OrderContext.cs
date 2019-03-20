using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrderManagemenet.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext()
            : base("DbConnection")
        { }
        public DbSet<RequestList> RequestLists { get; set; }
        public DbSet<Request> Requests{ get; set; }
    }
}