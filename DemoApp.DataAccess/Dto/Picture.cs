using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.DataAccess.Entities
{
    public class PictureDto
    {
        public Guid Id { get; set; }
        public byte[] Data { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
    }
}
