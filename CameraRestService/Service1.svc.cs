using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CameraRestService.Model;

namespace CameraRestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.


    //public int AddReading(string light, string tem, string pot)
    //{
    //    
    //    }


    public class Service1 : IService1
    {
        private const string connString =
            "Server=tcp:norbi-server.database.windows.net,1433;Initial Catalog=3SemFinal-DB;Persist Security Info=False;User ID=shadowzone88;Password=Russel888988;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public int AddImage(Image img)
        {
            using (SqlConnection databaseConnection = new SqlConnection(connString))
            {
                databaseConnection.Open();

                using (
                    SqlCommand insertCommand =
                        new SqlCommand("insert into Images (Datetime,Link) values (@Datetime, @Link)",databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@Datetime", img.Datetime);
                    insertCommand.Parameters.AddWithValue("@Link", img.Link);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        private static Image ReadImage(IDataRecord reader)
        {
            DateTime  date = reader.GetDateTime(1);
            string link = reader.GetString(2);
            Image img = new Image()
            {
                Datetime = date,
                Link = link
            };
            return img;
        }
        public IList<Image> GetImages()
        {
            List<Image> result = new List<Image>();

            using (SqlConnection databaseConnection = new SqlConnection(connString))
            {
                databaseConnection.Open();

                using (
                    SqlCommand getCommand =
                        new SqlCommand("Select * from Images", databaseConnection))
                {
                    using (SqlDataReader reader = getCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Image img1 = ReadImage(reader);
                                result.Add(img1);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
