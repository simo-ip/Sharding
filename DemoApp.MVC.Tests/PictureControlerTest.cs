using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DemoApp.MVC.Models;
using DemoApp.MVC.Controllers;
using System.Collections.Generic;
using DemoApp.DataAccess.Entities;
using System.Web.Mvc;
using System.Web;
using System.Security.Principal;
using DemoApp.DataAccess.Repositories;

namespace DemoApp.MVC.Tests
{
    [TestClass]
    public class PictureControlerTest
    {
        private Mock<ControllerContext> contextMock;
        private Mock<PictureModel> pictureModelMock;

        private PictureController target;

        [TestInitialize]
        public void Init()
        {
            // create mock principal
            var mocks = new MockRepository(MockBehavior.Default);
            Mock<IPrincipal> mockPrincipal = mocks.Create<IPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.Name).Returns(TestConstants.UserName);
            mockPrincipal.Setup(p => p.IsInRole(TestConstants.UserName)).Returns(true);

            // create mock context
            contextMock = new Mock<ControllerContext>();
            contextMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(TestConstants.UserName);

            // model
            pictureModelMock = new Mock<PictureModel>();
            pictureModelMock.Setup(x => x.PictureRepository).Returns(new PictureRepository());
            
            // create controller
            target = new PictureController();
            target.PictureModel = pictureModelMock.Object;
            target.ControllerContext = contextMock.Object;
        }

        [TestMethod]
        public void PictureControler_GetAll()
        {
            //arrange
            var expectedallProducts = new List<PictureDto> { new PictureDto() };
            pictureModelMock
                .Setup(it => it.Init(""))
                .Returns(pictureModelMock.Object);
            pictureModelMock
                .Setup(it => it.GetAll())
                .Returns(new List<PictureDto> { new PictureDto() });

            //act
            var result = target.Index();

            //assert
            var model = (result as ViewResult).Model as List<PictureDto>;
            Assert.AreEqual(model.Count, expectedallProducts.Count);
        }

        private static class TestConstants
        {
            public static string UserName = "simo@mail.bg";
        }
    }
}
