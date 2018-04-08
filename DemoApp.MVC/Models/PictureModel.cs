using DemoApp.DataAccess.Entities;
using DemoApp.DataAccess.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace DemoApp.MVC.Models
{
    public class PictureModel
    {
        [Dependency]
        public virtual PictureRepository PictureRepository { get; set; }

        public virtual PictureModel Init(string dataBaseName)
        {
            PictureRepository.Init(dataBaseName);
            return this;
        }

        public virtual List<PictureDto> GetAll()
        {
            var data = PictureRepository.GetAll();
            imageResize(data);
            return data;
        }

        public PictureDto GetById(Guid id)
        {
            return PictureRepository.GetById(id);
        }

        public PictureDto Create(PictureDto picture)
        {
            PictureRepository.Greate(picture);
            return picture;
        }

        public int Update(PictureDto picture)
        {
            int result = PictureRepository.Update(picture);
            return result;
        }

        public int Delete(Guid Id)
        {
            int result = PictureRepository.Delete(Id);
            return result;
        }
        private void imageResize(List<PictureDto> data)
        {
            foreach(var item in data)
            {

                WebImage foo = new WebImage(item.Data);

                int sourceWidth = foo.Width;
                int sourceHeight = foo.Height;

                int width = 100;
                int height = 100;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)width / (float)sourceWidth);
                nPercentH = ((float)height / (float)sourceHeight);

                if (nPercentH > nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                foo.Resize(destWidth, destHeight);

                
                if (nPercentH > nPercentW)
                {
                    int size = (int)Math.Round((decimal)(foo.Width - width) / 2, 0);
                    foo.Crop(0, size, 0, size);
                }
                else
                {
                    int size = (int)Math.Round((decimal)(foo.Height - height) / 2, 0);
                    foo.Crop(size, 0, size, 0);
                }
                item.Data = foo.GetBytes();
            }
            
        }

        
    }
}