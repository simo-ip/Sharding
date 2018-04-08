using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApp.MVC.Controllers
{
    public class BaseController : Controller
    {
        public virtual string DbName
        {
            get
            {
                var userName = HttpContext.User.Identity.Name;
                return GetDataBaseName(userName);
            }
        }

        protected string GetDataBaseName(string userEmail)
        {
            string db = "";
            int hc = userEmail.GetHashCode();
            int a = Math.Abs(hc % 3);
            switch(a)
            {
                case 0:
                    {
                        db = "DemoAppDb1";
                        break;
                    }
                case 1:
                    {
                        db = "DemoAppDb2";
                        break;
                    }
                case 2:
                    {
                        db = "DemoAppDb3";
                        break;
                    }
                        
            }

            return db;
        }
    }
}