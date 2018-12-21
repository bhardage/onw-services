using Microsoft.VisualStudio.TestTools.UnitTesting;
using ONWServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ONWServices.Tests.Unit
{
    [TestClass]
    public class BaseTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {

        }

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void When_Should()
        {
            Assert.IsTrue(true);
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }

        [ClassCleanup]
        public static void ClassCleanup()
        {

        }
    }    
}
