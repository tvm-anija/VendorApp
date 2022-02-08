using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repository;
using WebApi.Repository.IRepository;

namespace VendorMachineTests1.Repository
{
    [TestClass]
    public class UserRepositoryTests
    {
        Mock<ApplicationDbContext> mock = new Mock<ApplicationDbContext>();
        Mock<IUserRepository> dbObject = new Mock<IUserRepository>();

        User result = new User
        {
            Deposit = 500,
            Password="",
            Role=User.RoleTye.buyer,
            Token="",
            UserId=4,
            UserName="Emma"
        };
        [TestMethod]
        public void Deposit()
        {
            //Arrange
            bool returnValue = true;
            //Act
            dbObject.Setup(db => db.UpdateDeposit(result)).Returns(true);
            bool actual = dbObject.Object.UpdateDeposit(result);

            //Assert
            Assert.AreEqual(returnValue, actual);
        }
}

}