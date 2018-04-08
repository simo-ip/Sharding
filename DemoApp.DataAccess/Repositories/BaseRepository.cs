using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DemoApp.DataAccess.Repositories
{
    public class BaseRepository
    {
        protected string ConnectionString;

        protected BaseRepository Init(string dataBaseName)
        {
            var connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            var dbName = String.Format("  database={0};", dataBaseName);
            StringBuilder sb = new StringBuilder(connStr);
            sb.Append(dbName);
            ConnectionString = sb.ToString();

            return this;
        }
    }
}
