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

       public async Task<int> AddImage(Image img)
        {
            #region return data testcode

            int dbRowsAffected = 0;

            //TODO test if upload was a success before storing in database
            bool uploadToDbSuccess = false;

            #endregion

            #region testcode for dummydata

            #endregion
            

            //Uploads the data to Dropbox and writes the dropbox path
            using (DropboxClient client = new DropboxClient(TokenString))
            {

                string returnString = await Upload(client, DropboxFolder, img.FileName, img.Data);
                string remotePath = $"{DropboxFolder}/{img.FileName}";

                //string variable for shared link url
                string url = "";

                //Tries to get a sharedlink (url as string)
                //Fails if link has already been created
                try
                {
                    url = await GetOrCreateSharedLink(client, remotePath);
                    Console.WriteLine($"{returnString}, URL: {url}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                //Creates object that holds the metadata that needs to be stored in the db
                ImageInfo imgMeta = new ImageInfo(img.FileCreationDate, DropboxFolder, img.FileName, url);

                //TODO store ImageInfo in db
                dbRowsAffected = StoreDataInDb(imgMeta);
            }

            return dbRowsAffected;
            
        }

        /// <summary>
        /// Uploads file to Dropbox
        /// </summary>
        /// <param name="dbx">the dropbox client (DropboxClient)</param>
        /// <param name="folder">The remote dropbox directory folder path (string)</param>
        /// <param name="fileName">the name of the file including file extension (string)</param>
        /// <param name="content">The filedata (Byte array)</param>
        /// <returns>(string) directorypath/filename and file revision</returns>
        private static async Task<string> Upload(DropboxClient dbx, string folder, string fileName, byte[] content)
        {
            using (var mem = new MemoryStream(content))
            {
                var updated = await dbx.Files.UploadAsync(
                    folder + "/" + fileName,
                    WriteMode.Overwrite.Instance,
                    body: mem);
                return $"{folder}/{fileName}, {updated.Rev}";
            }
        }
       
        /// <summary>
        /// Get or creates a shared link (url) to a Dropbox file
        /// </summary>
        /// <param name="client">Dropbox client</param>
        /// <param name="remotePath">filename including full directory path</param>
        /// <returns>Url as string to the shared file</returns>
        private static async Task<string> GetOrCreateSharedLink(DropboxClient client, string remotePath)
        {
            string url = "";
            try
            {
                SharedLinkMetadata meta = await client.Sharing.CreateSharedLinkWithSettingsAsync($"{remotePath}");
                url = meta.Url;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                try
                {
                    ListSharedLinksResult result = await client.Sharing.ListSharedLinksAsync($"{remotePath}");
                    if (result.Links.Count > 0)
                    {
                        url = result.Links[0].Url;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    url = "";
                }
            }
            return url;
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
                        new SqlCommand("insert into Images (FileCreationDate,Link) values (@fileCreationDate, @sharedLink)", databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@fileCreationDate", imgMeta.FileCreationDate);
                    insertCommand.Parameters.AddWithValue("@sharedLink", imgMeta.SharedLink);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
        
        
        private static Image ReadImage(IDataRecord reader)
        {
            DateTime date = reader.GetDateTime(1);
            string link = reader.GetString(2);
            Image img = new Image()
            {
                FileCreationDate = date,
                FileName = link
            };
            return img;
        }

        public IList<Image> GetImages()
        {
            List<Image> result = new List<Image>();

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
