﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleAppTests
{
    [TestClass]
    public class UnitTest1
    {
        public HttpListener Listener {get; set;}

        [TestInitialize]
        public void Initialize()
        {
            Task.Run(() => {
                Listener = new HttpListener();
                string url = "http://localhost:8000/";

                Listener.Prefixes.Add(url);
                Listener.Start();
                Task.Delay(1000).Wait();
            });
        }


        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var client = new HttpClient();
            HttpResponseMessage res = null;

            //Act
            string url = Listener.Prefixes.FirstOrDefault();
            res = client.GetAsync(url).Result;



            //Assert
           /* Assert.AreEqual(
                actual: res?.ContentType, 
                expected: "text/html"
            );
            Assert.AreEqual(
                actual: res?.ContentEncoding,
                expected: Encoding.UTF8
                );
            Assert.IsTrue(res?.ContentLength64 > 0);*/
        }
       

        [TestCleanup] 
        public void Cleanup()
        {
            Listener.Stop();
        }
    }
}
