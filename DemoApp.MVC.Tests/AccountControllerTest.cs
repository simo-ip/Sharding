using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoApp.MVC.Models;
using Moq;
using DemoApp.MVC.Controllers;
using DemoApp.DataAccess.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
using DemoApp.DataAccess.Repositories;
using System.Linq;
using System.Security.Principal;
using System.Web;
using DemoApp.MVC.Infrastructure;


namespace DemoApp.MVC.Tests
{
    [TestClass]
    public class AccountControllerTest
    {
        private AccountController Target;
        public List<User> users;

        [TestInitialize]
        public void Init()
        {
            
            // create mock principal
            var mocks = new MockRepository(MockBehavior.Default);
            Mock<IPrincipal> mockPrincipal = mocks.Create<IPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.Name).Returns(TestConstants.UserName);
            mockPrincipal.Setup(p => p.IsInRole(TestConstants.UserName)).Returns(true);

            // create mock context
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["test"]).Returns("Hello World");
            controllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("simo@mail.bg");
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            controllerContext.SetupGet(p => p.HttpContext.Response.Cookies).Returns(new HttpCookieCollection());

            controllerContext.Setup(p => p.HttpContext.Request.Form.Get("ReturnUrl")).Returns("sample-return-url");
            controllerContext.Setup(p => p.HttpContext.Request.Params.Get("q")).Returns("sample-search-term");

            //Mock<ControllerContext> contextMock;
            //contextMock = new Mock<ControllerContext>();
            //contextMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(TestConstants.UserName);
            //contextMock.Setup(ctx => ctx.HttpContext.Session).Returns(new MockHttpSession());
            // model
            users = new List<User>
            {
                new User { Email = "user1@mail.com", Password = "1111"},
                new User { Email = "user2@mail.com", Password = "2222"},
                new User { Email = "user3@mail.com", Password = "3333"},
            };
            Mock<UserRepository> mockUserRepository;
            mockUserRepository = new Mock<UserRepository>();
            mockUserRepository.Setup(mr => mr.GetAll()).Returns(users);
            mockUserRepository
                .Setup(mr => mr.FindByEmail(It.IsAny<string>()))
                .Returns((string s) => users.Where(x => x.Email == s).Single());
            mockUserRepository.Setup(mr => mr.Create(It.IsAny<User>())).Returns(
                (User target) =>
                {
                    DateTime now = DateTime.Now;

                    if (!target.Email.Equals("dummy@mail.bg"))
                    {
                        target.Id = new Guid();
                        target.Email = target.Email;
                        target.Password = target.Password;
                        users.Add(target);
                    }
                    else
                    {
                        var original = users.Where(q => q.Email == target.Email).Single();

                        if (original != null)
                        {
                            original.Password = target.Password;
                        }

                    }

                    return target.Id;
                });
            Mock<FormsAuth> formAuth = new Mock<FormsAuth>();
            formAuth.Setup(x => x.Login(""));
            Target = new AccountController();
            Target.UserModel = new UserModel();
            Target.UserModel.UserRepository = mockUserRepository.Object;
            Target.ControllerContext = controllerContext.Object;
            Target.FormsAuth = formAuth.Object;
        }

       

        private static class TestConstants
        {
            public static string UserName = "simo@mail.bg";
            public static string Password = "sim0";

            public static User User = new User()
            {
                Email = TestConstants.UserName,
                Password = TestConstants.Password
            };
        }
    }

    public class MockHttpSession : HttpSessionStateBase
    {
        Dictionary<string, object> m_SessionStorage = new Dictionary<string, object>();

        public override object this[string name]
        {
            get { return m_SessionStorage[name]; }
            set { m_SessionStorage[name] = value; }
        }
    }
}
