using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DemoApp.DataAccess.Repositories;
using Moq;
using DemoApp.MVC.Models;
using DemoApp.DataAccess.Entities;

namespace DemoApp.MVC.Tests
{
    [TestClass]
    public class PictureModelTest
    {
        private Mock<PictureRepository> pictureRepositoryMock { get; set; }

        private PictureModel target { get; set; }
        
        [TestInitialize]
        public void Init()
        {
            pictureRepositoryMock = new Mock<PictureRepository>();
            pictureRepositoryMock
                .Setup(it => it.Init(""))
                .Returns(pictureRepositoryMock.Object);
            pictureRepositoryMock.Setup(rm => rm.GetAll()).Returns(
                new List<PictureDto> {
                    new PictureDto { Id = TestConstants.UserId, Comment = "comment 1", UserId = TestConstants.UserId, Data = TestConstants.Pic } 
                });

            // create model
            target = new PictureModel();
            target.PictureRepository = pictureRepositoryMock.Object;
            
        }

        [TestMethod]
        public void PictureModel_CanReturnAllPictures()
        {                                 
            // TODO
            var actual = target.GetAll();

            Assert.AreEqual(1, actual.Count);
        }

        #region Constants

        private static class TestConstants
        {
            public static Guid UserId = new Guid("9DBA9E94-97D0-E411-8295-4C80930EE768");
            public static Guid FirstPictureId = new Guid("66DA95C5-950F-4BF2-8E11-067348A6C1E2");
            public static Guid SecondPictureId = new Guid("A59DBA62-EF34-4BA0-B031-081AD7E3F84E");
            public static Guid ThirdPictureId = new Guid("9D08584F-2370-4F5A-B610-3F6ADE786B2E");
            public static string str = "/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAgGBgcGBQgHBwcJCQgKDBQNDAsLDBkSEw8UHRofHh0aHBwgJC4nICIsIxwcKDcpLDAxNDQ0Hyc5PTgyPC4zNDL/2wBDAQkJCQwLDBgNDRgyIRwhMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjL/wAARCABjAGQDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD1YaTp3/QOsv8AwGT/AApw0nTf+gbZf+Ayf4VeC04LW1zn5WUP7J07/oG2X/gMn+FKNI07/oG2X/gMn+FaAUU4KKLhysoLpGm/9A2y/wDAZP8ACnf2Rpuf+QZZf+Ayf4VfxzTgBRoHKzP/ALI03p/Zll/4DJ/hUg0XTiM/2XZY/wCvZP8ACrwFOHHekOzKC6NphOP7Msv/AAHT/CpP7D0z/oGWX/gOn+FTTXlrajNxcRR/77AVUk8S6RDwb6M5/u5b+VSxpLqTf2Hpn/QMsv8AwHT/AApRoulDrpdl/wCA6f4VQn8X6NBHuF15nosaljWRdfESCMMYbCRgBnc7hR+lKzZV4o6VtF0kHnTbUf7tun+FFeQ6p8Sdbub1pLeUWsWAFjjHH1op8ge1R6z5g9aUSr/eH51wdn4zsbm3Dz+ZA2DnIJUfiKjl8caWJii/aGycbwgx/PNacq7mfO+x6AZ0UZLjFQtqMS8Llv5V5Vqnj6XzzHp6IqA/6yRSSfw7VRj8VazNC8QuF3P/ABlPmT3HajlXcXPJ9D1qTW44yweSKMhd3zNggev096525+IumRttjmlnfONsKYB/GvL8T3zuSJHkz887tnP40kVnIG3RlWIzn6/570XigtM725+I0kny2lsUweWuHP6Ada5+78ZazcsypeSAE8CMBAKwCreZ5ap5k+OAOQpq5FpskGWnkONvKqeSTz0qOZD5WOGpytLtKGSVuPmfJP41ba7VCEdgZD1UdqqRRpbq5QLuC8kHk896SOEGRpQMORg1OjKsy6ZXdtwC9MYxWTd30hZ4+QDwc9q2ETCgbs464NVTZxR3O/5nZjkbh0pqSQcrZkpY3Mq7wCoPQE0VsFTIxO3ODjmij2ouQHmjt2HlDcztg4GMVGkNu+WZ8szYLN0UZ5rRaS1lkH2VAqj+/IST9RgU94iFU/Z42bOAFHH41yqt2ZtYxJ7AebIkKIyZyHKYx9Kdbae63O+UtGqHJy2Mj15rUWAkkiLLA9AB/Oo2tZjIGaMMQflYn7taqbZLSRn6lcQ3DiG3dUhTliScE/SktgyJJ5UpbO3lk2j8M81dewjEhkK4J6kLyaa9orYZrh8A8giiUnbYFbe4yNmt+YvKDMBl27UskrSuWbbv7EAkfjTPs0Y6Ntx1PXNKI22fu5PmPBB5zWTnIehX8qTepjEfJ+YYzz61aQOv+snDLnGAOKQ8LtmZdv8ADjsamieHCgumQOF2jr60e0kFhvmuV2DjJxknbSkbAcTZYj+HmmSBoz5gcYx97FVXkMilgZN3QZx/+unzsLFrzo/Vz6kUVHHavt+dNx9doopczAvwot0wiuUWOJuGlK58r3AB/SrF3ZNZSLFHqlvcxSJhGViduPXPSs9Y7qU7WRQGGcE5OKlSxfALquB04FcSmbWbLa6bcmD7Rbzxs5wGQTDP4A9+aTe4G2ZWjl67XXHHrVMwbdsn7xdvI+bkH1q0ZjKUaeVmYNklupGPX/61dFKvyvUynT5kI0gPfP41EzA054yZDs5Q/NljnH6UwQyldxMe0nH3TXX9bg9Gc/sJdBMj1pMimN5ok2CI5H95MUDzWP8AqVx3O7pT+s02L2Mx5Csu0gc+1OSOIfwfUjFKYpCBtQA/xBqjLNGGMgAx0qJVaLeqNI06q2JWijY53SfTIqCSH51MZbJ4wQDn8qtJcbbYxuFc53ghfmXPv6VNZ395EdoCEbQMunI9wTWXtKXRG3sp9WZhaXJDOwI4wRRWrt8wBi5JI5J70UvarsP2T7lOUSMRJIuQPu8cfl3/ABqdHnAVMyAnnj0+vaplFtDCVdx93OFXgfT3rLur9FVIkimkydxfpiuOxoXXYBi7bcdBk/0pVkjIOSGxyM5/wrPkkjcrtTcD16n8KLaWSLzPLyu/jLrkqPbvTWm4al9iMf8ALQZ6+3Hr2qH7bCIySZQFGFCoDuNVi8ZBL5OT3bFRSTJDJ8+0yMPlH8WKV+whXuZ5Cuw7W6gZ4/HipY7iYndIdvcgmoDOqhVZMBjxzgn2/Kms7M37tU5yAM7sU3N2sF30LLzxE5Mitn/axSNdHPzsD3+7x9ag/s68kiH7hIxxjHc/XtUhslt4PMunJdeoVuDUq7C8izbXn7zbGCWPHyrj/IqVrmKENksX7Dqfy71mf2rDsEdsWBH7s4XLHrmqdqss0ux12QjIBZiM/ieadh87NJtbbdh44tw9ZNv6UVJHZRRoFUhfY4NFFw1IvNnaIO0Tlj1DDn8qaiyKxz8wxnHA/GrN3p09rPnfGyHOGRskDPcVAbeTeN7AAj5WP+f5UncNRpuIt4yeSOADmo0/ekq0pQA5bavH51a+zh4BujUdvM5wPrmqsrYiMbouw5UEjOc98Ug5UhzGKDMrbcN1IYZPvUMrzDEkNsdhOPOI5z9asrbqoDMGAc8Kfm3/AOAqzBGrMEEQjVFPAbJzWqjoPluU7a3cndI6PgZIxn/9VXEd4WwyDjoQmKUSQbicDIXnsv6VTnuEgjaX5sdDnJOPYHrS91Id1FF681CCCIMZF3PgcHgGsi6dJnDSqzy8suMYGM9v/rVFZXUty7siGGL+EbdzOR68fKMVJdzvagyZiADAHjP0J9qybdzGU+YHZPlUTAbf4cDj34qt9taSUBXDsrbfmHH5noKpwXNtJNI05VpXPJiTCjHoO9TLdIkifuRHvyVwoLH36ZqlFoSNJ9Ty2EXcF4Jx3oqjNPIxUpemIbR8vlMD9aKdirnQqSunxSAkOepp8EKTeR5gLZJzyaKKS3ZsPKhTMgHygYAPPaqdrDHcSSCVQwVOB0xxRRWlLYEaUMMaWwKrgjbz+FZl67Qwbo2KsDwRRRSkEtjD02RzPaKXYh3+bnrzXSvBEZJnMSFhJtBIzxRRUT2MH8JiK7NfImdqncp2jbkZ6HFSPGj6fNuXOEwPpzRRUPdEPoc1cAeZKe6SMF56dK25ZpIDIsTlFSMMoB6Giiuit0KJLfMtvG7ksxHJJooooWwH/9k=";
            public static byte[] Pic = Convert.FromBase64String(str);
            
        }

        
        #endregion
    }
}
