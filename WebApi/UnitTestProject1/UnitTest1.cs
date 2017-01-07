using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Models;
using System.Linq;
using WebApi.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Setup()
        {
            Database.SetInitializer(new DbInitializer());
            using (ApplicationDbContext context = GetContext())
            {
                context.Database.Initialize(true);
            }
        }

        [TestMethod]
        public void insertionFonctionnelle()
        {
            using (var context = GetContext())
            {
                Assert.AreEqual(1, context.Customers.ToList().Count);
            }
        }

        public ApplicationDbContext GetContext()
        {
            return new ApplicationDbContext();
        }
    }
}
