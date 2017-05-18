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
using Dropbox.Api;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Dropbox.Api.Files;
using Dropbox.Api.Sharing;

namespace CameraRestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    public class Service1 : IService1
    {
        private const string TokenString = "zcZd9um6pSAAAAAAAAAABjaZN3e3590O3JlftulYkAXPo3tOJgfcxz3reU9lR_ek";
        private const string DropboxFolder = "/3SemExam3dPrintercamRest";
        private const string ConnString =
            "Server=tcp:norbi-server.database.windows.net,1433;Initial Catalog=3SemFinal-DB;Persist Security Info=False;User ID=shadowzone88;Password=Russel888988;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        /// <summary>
        /// Gets all image data from database
        /// </summary>
        /// <returns>List of ImageInfo - CreationDateTime and DropBoxUrl</returns>
        public IList<ImageInfo> GetImages()
        {
            List<ImageInfo> result = new List<ImageInfo>();

            using (SqlConnection databaseConnection = new SqlConnection(ConnString))
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
                                ImageInfo imgMeta = ReadImage(reader);
                                result.Add(imgMeta);
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// takes a string input DropBox Url, creates an uploadDate and stores it in the database
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool PostImage(string input)
        {
            bool IsUploadSuccessful = false;

            string dropboxUrl = input;
            DateTime creationDateTime = DateTime.Now;

            ImageInfo imgMetaData = new ImageInfo(creationDateTime, "", "", dropboxUrl);

            int rowsAffected = StoreDataInDb(imgMetaData);

            if (rowsAffected > 0)
            {
                IsUploadSuccessful = true;
            }
            return IsUploadSuccessful;

        }
        
        /// <summary>
        /// Reads returned data from database and puts the values in an ImageInfo object
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>ImageInfo object - CreationDate and DropBoxUrl</returns>
        private static ImageInfo ReadImage(IDataRecord reader)
        {
            DateTime date = reader.GetDateTime(1);
            string link = reader.GetString(2);
            ImageInfo img = new ImageInfo()
            {
                FileCreationDate = date,
                FileName = link
            };
            return img;
        }

        /// <summary>
        /// Inserts imageInfo into the database
        /// </summary>
        /// <param name="imgMeta"></param>
        /// <returns></returns>
        private static int StoreDataInDb(ImageInfo imgMeta)
        {
            using (SqlConnection databaseConnection = new SqlConnection(ConnString))
            {
                databaseConnection.Open();

                using (
                    SqlCommand insertCommand =
                        new SqlCommand("insert into Images (Datetime,Link) values (@fileCreationDate, @sharedLink)", databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@fileCreationDate", imgMeta.FileCreationDate);
                    insertCommand.Parameters.AddWithValue("@sharedLink", imgMeta.SharedLink);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        
    }
}
