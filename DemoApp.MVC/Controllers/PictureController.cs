using DemoApp.DataAccess.Entities;
using DemoApp.MVC.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApp.MVC.Controllers
{
    [Authorize]
    public class PictureController : BaseController
    {
        [Dependency]
        public PictureModel PictureModel { get; set; }

        public ActionResult Index()
        {
            PictureModel.Init(DbName);

            var model = PictureModel.GetAll();
            
            return View(model);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, PictureDto model)
        {
            try
            {
                if (file != null)
                {
                    var userName = System.Web.HttpContext.Current.User.Identity.Name;
                    Guid userId = Guid.Parse(System.Web.HttpContext.Current.Session["UserId"].ToString());
                    
                    Stream s = file.InputStream;
                    byte[] data = new byte[file.ContentLength + 1];
                    s.Read(data, 0, file.ContentLength);
                    model.Data = data;
                    model.UserId = userId;
                    PictureModel.Init(DbName).Create(model);
                    return RedirectToAction("Index");

                }
                else
                {
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(PictureDto model)
        {
            var m = PictureModel.Init(DbName).GetById(model.Id);
            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, PictureDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        Stream s = file.InputStream;
                        byte[] data = new byte[file.ContentLength + 1];
                        s.Read(data, 0, file.ContentLength);                      
                        model.Data = data;
                        
                    }
                    
                    PictureModel.Init(DbName).Update(model);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(PictureDto model)
        {
            var m = PictureModel.Init(DbName).GetById(model.Id);
            return View(m);
        }

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                PictureModel.Init(DbName).Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
