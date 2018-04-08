using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DemoApp.MVC.Infrastructure
{
    //public interface IAuthentication
    //{
    //    void Login(string username);
    //    void Logout();
    //}

    public class FormsAuth //: IAuthentication
    {
        public virtual void Login(string username)
        {
            FormsAuthentication.SetAuthCookie(username, false);
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}