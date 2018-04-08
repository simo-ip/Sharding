using DemoApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.DataAccess.Repositories
{
    public class PictureRepository : BaseRepository
    {
        public virtual PictureRepository Init(string dataBaseName)
        {
            base.Init(dataBaseName);
            return this;
        }

        public virtual List<PictureDto> GetAll()
        {
            List<PictureDto> result = new List<PictureDto>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                int bufferSize = 100;
                byte[] outByte = new byte[bufferSize];
                
                SqlCommand command = new SqlCommand("PicturesGetAll", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var picture = new PictureDto();
                        
                        picture.Id = reader.GetGuid(0);
                        picture.UserId = reader.GetGuid(2);

                        long bytesize = reader.GetBytes(1, 0, null, 0, 0);
                        picture.Data = new byte[bytesize];
                        long bytesread = 0;
                        int chunkSize = 1323;
                        int curpos = 0;
                        while (bytesread < bytesize)
                        {
                            bytesread += reader.GetBytes(1, curpos, picture.Data, curpos, chunkSize);
                            curpos += chunkSize;
                        }

                        result.Add(picture);
                    }

                }
            }
            return result;
        }

        public PictureDto GetById(Guid Id)
        {
            List<PictureDto> result = new List<PictureDto>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                int bufferSize = 100;
                byte[] outByte = new byte[bufferSize];

                SqlCommand command = new SqlCommand("PicturesGetById", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Id", Id);
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var picture = new PictureDto();

                        picture.Id = reader.GetGuid(0);
                        picture.UserId = reader.GetGuid(2);
                        picture.Comment = reader.GetString(3);

                        long bytesize = reader.GetBytes(1, 0, null, 0, 0);
                        picture.Data = new byte[bytesize];
                        long bytesread = 0;
                        int chunkSize = 1323;
                        int curpos = 0;
                        while (bytesread < bytesize)
                        {
                            bytesread += reader.GetBytes(1, curpos, picture.Data, curpos, chunkSize);
                            curpos += chunkSize;
                        }

                        result.Add(picture);
                    }

                }
            }
            return result.FirstOrDefault();
        }

        public int Greate(PictureDto picture)
        {
            int result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                
                SqlCommand command = new SqlCommand("PicturesInsert", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Data", picture.Data);
                command.Parameters.AddWithValue("UserId", picture.UserId);
                command.Parameters.AddWithValue("Comment", picture.Comment);
                conn.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        public int Update(PictureDto picture)
        {
            int result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                SqlCommand command = new SqlCommand("PicturesUpdate", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Id", picture.Id);
                command.Parameters.AddWithValue("Data", picture.Data);
                command.Parameters.AddWithValue("Comment", picture.Comment);
                conn.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        public int Delete(Guid Id)
        {
            int result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                SqlCommand command = new SqlCommand("PicturesDelete", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Id", Id);
                conn.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }
    }
}
