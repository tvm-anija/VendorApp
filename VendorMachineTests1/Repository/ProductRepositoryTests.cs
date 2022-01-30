using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Repository.IRepository;
using Moq;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Repository.Tests
{
    [TestClass()]
    public class ProductRepositoryTests
    {
        [TestMethod()]
        public void ProductRepositoryTest()
        {
            //Arrange
            int productID = 6;


            Mock<ApplicationDbContext> mock = new Mock<ApplicationDbContext>();
            Mock<IProductRepository> dbObject = new Mock<IProductRepository>();

            Product result = new Product
            {
                id = 6,
                sellerId = 4,
                User = null,
                cost = 50,
                productName = "Watch",
                amountAvailable = 100
            };
            
            //Act
            dbObject.Setup(db => db.GetProduct(productID)).Returns(result);
            Product actual = dbObject.Object.GetProduct(productID);

            //Assert
            Assert.AreEqual(result, actual);
        }
    }
}