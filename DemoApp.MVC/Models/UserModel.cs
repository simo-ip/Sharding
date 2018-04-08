using DemoApp.DataAccess.Entities;
using DemoApp.DataAccess.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApp.MVC.Models
{
    public class UserModel
    {
        [Dependency]
        public virtual UserRepository UserRepository { get; set; }

        public virtual UserModel Init(string dataBaseName)
        {
            UserRepository.Init(dataBaseName);
            return this;
        }

        public Guid Register(User user)
        {
            return UserRepository.Create(user);
        }

        public virtual User Validate(string username, string password)
        {
            return UserRepository.Validate(username, password);
        }
    }
}