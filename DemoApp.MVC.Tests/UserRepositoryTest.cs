using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DemoApp.DataAccess.Entities;
using DemoApp.DataAccess.Repositories;
using Moq;
using System.Linq;

namespace DemoApp.MVC.Tests
{
    [TestClass]
    public class UserRepositoryTest
    {
        public UserRepository MockUserRepository;
        public List<User> users;
        
        [TestInitialize]
        public void Init()
        {
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

                    if (!target.Email.Equals("simo@mail.bg"))
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
            MockUserRepository = mockUserRepository.Object;
        }

        [TestMethod]
        public void UserRepository_CanReturnAllUsers()
        {
            List<User> testUsers = this.MockUserRepository.GetAll();

            Assert.IsNotNull(testUsers); 
            Assert.AreEqual(3, testUsers.Count); 
        }

        [TestMethod]
        public void UserRepository_CanReturnUserByEmail()
        {
            User testUser = MockUserRepository.FindByEmail("user1@mail.com");

            Assert.IsNotNull(testUser); 
            Assert.IsInstanceOfType(testUser, typeof(User)); 
            Assert.AreEqual("1111", testUser.Password); 
        }

        [TestMethod]
        public void UserRepository_CanInsertNewUser()
        {
            User newUser = new User { Id = new Guid(),Email = "newuser@mail.com", Password = "1111" };

            int userCount = this.MockUserRepository.GetAll().Count;
            Assert.AreEqual(3, userCount); 

            MockUserRepository.Create(newUser);

            userCount = this.MockUserRepository.GetAll().Count;
            Assert.AreEqual(4, userCount);

            User testUser = MockUserRepository.FindByEmail("newuser@mail.com");
            Assert.IsNotNull(testUser);
            Assert.IsInstanceOfType(testUser, typeof(User));
            Assert.AreEqual("newuser@mail.com", testUser.Email);
        }
    }
}
