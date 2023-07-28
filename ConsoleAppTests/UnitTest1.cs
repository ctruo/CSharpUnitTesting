using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ConsoleAppTests
{
    [TestClass]
    public class UnitTest1
    {
        //

        [TestInitialize]
        public void Initialize()
        {
            //new thread {
            //  HttpListener listener = new HttpListener(8000)
            //  thread.Sleep(1000)
            //}
        }


        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
         

            //client = new HttpClient(8000)

            //Act
            //res = client.sendGet()

            //Assert
            //AssertIsEqual(res.ContentType, "text/html")
            //AssertIsEqual(res.ContentEncoding, Encoding.UTF8)
            //AssertGreaterThan(res.ContentLength, 0)
        }

        [TestCleanup] public void Cleanup()
        {
            //listener.shutdown()
        }
    }
}
