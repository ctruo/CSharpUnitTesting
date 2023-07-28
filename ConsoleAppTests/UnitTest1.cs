using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Text;
using System.Web;

namespace ConsoleAppTests
{
    [TestClass]
    public class UnitTest1
    {
        //public Listener {get; set;}

        [TestInitialize]
        public void Initialize()
        {
            //new thread {
            //  Listener = new HttpListener(8000)
            //  thread.Sleep(1000)
            //}
        }


        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            //client = new HttpClient(8000)
            HttpListenerResponse res = null;

            //Act
            //res = client.sendGet()



            //Assert
            Assert.AreEqual(
                expected: res?.ContentType, 
                "text/html"
            );
            Assert.AreEqual(
                expected:res?.ContentEncoding,
                Encoding.UTF8
                );
            Assert.IsTrue(res?.ContentLength64 > 0);
        }
       

        [TestCleanup] 
        public void Cleanup()
        {
            //Listener.shutdown()
        }
    }
}
