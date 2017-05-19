using System;
using System.Collections.Generic;
using CameraRestService.Model;
using CameraRestService.PersistencyFacade;

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
            List<ImageInfo> result = Facade.GetImagesFromDb(ConnString);

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

            int rowsAffected = Facade.StoreDataInDb(ConnString,imgMetaData);

            if (rowsAffected > 0)
            {
                IsUploadSuccessful = true;
            }
            return IsUploadSuccessful;

        }




    }
}
