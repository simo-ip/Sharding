using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoApp.MVC.Models;
using Moq;
using DemoApp.MVC.Controllers;
using DemoApp.DataAccess.Entities;
using System.Collections.Generic;
using System.Web.Mvc;


namespace DemoApp.MVC.Tests
{
    [TestClass]
    public class AccountControllerTest
    {
        private Mock<UserModel> userModelMock;

        private AccountController target;

        [TestInitialize]
        public void Init()
        {
            userModelMock = new Mock<UserModel>();

            target = new AccountController();
            target.UserModel = userModelMock.Object;
        }

        [TestMethod]
        public void AccountController_Register()
        {
            //arrange
            var expected = new User() {};
            userModelMock
                .Setup(it => it.Register(new User() { Email = "", Password = ""}))
                .Returns(new User());

            //act
            var result = target.Register(new RegisterViewModel { Email = "simo@mail.bg", Password="1111111"});

            //assert
            var model = (result as ViewResult).Model as User;
            Assert.AreEqual(model, expected);
        }
    }
}
