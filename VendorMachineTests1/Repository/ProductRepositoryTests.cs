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
    [TestClass]
    public class ProductRepositoryTests
    {
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


        [TestMethod]
        public void GetProduct()
        {
            //Arrange
            
            //Act
            dbObject.Setup(db => db.GetProduct(productID)).Returns(result);
            Product actual = dbObject.Object.GetProduct(productID);

            //Assert
            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void CreateProduct()
        {
            //Arrange
            bool returnValue = true;
            //Act
            dbObject.Setup(db => db.CreateProduct(result)).Returns(true);
            bool actual = dbObject.Object.CreateProduct(result);

            //Assert
            Assert.AreEqual(returnValue, actual);
        }

        [TestMethod]
        public void UpdateProduct()
        {
            //Arrange
            bool returnValue = true;
            //Act
            dbObject.Setup(db => db.UpdateProduct(result)).Returns(true);
            bool actual = dbObject.Object.UpdateProduct(result);

            //Assert
            Assert.AreEqual(returnValue, actual);
        }

        [TestMethod]
        public void DeleteProduct()
        {
            //Arrange
            bool returnValue = true;
            //Act
            dbObject.Setup(db => db.DeleteProduct(result)).Returns(true);
            bool actual = dbObject.Object.DeleteProduct(result);

            //Assert
            Assert.AreEqual(returnValue, actual);
        }

        [TestMethod]
        public void Buy()
        {
            //Arrange
            bool returnValue = true;

            ShoppingCart cart = new ShoppingCart
            {
                ApplicationUserId = "4",
                Count = 2,
                Id = 1,
                MenuItemId = 1006,
                Product = new Product
                {
                    amountAvailable = 100,
                    cost = 20,
                    id = 1006,
                    productName = "TV",
                    sellerId = 6,
                    User = null
                },
                User = new User
                {
                    Deposit = 1000,
                    Password = "",
                    Role = User.RoleTye.buyer,
                    Token = "",
                    UserId = 4,
                    UserName = "Emma"
                }
            };

            //Act
            dbObject.Setup(db => db.AddItemToShoppingCart(cart)).Returns(true);
            bool actual = dbObject.Object.AddItemToShoppingCart(cart);

            //Assert
            Assert.AreEqual(returnValue, actual);
        }
    }
}