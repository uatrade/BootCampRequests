using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagemenet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagemenet.Models.Tests
{
    [TestClass()]
    public class RequestMetodTests
    {
        [TestMethod()]
        public void DownLoadTest()
        {
            RequestMetod requestMetod = new RequestMetod();
            bool res=requestMetod.DownLoad();

            Assert.IsTrue(res);
        }
    }
}