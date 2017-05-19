using CameraRestService.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace CameraRestService.PersistencyFacade
{
    public class Facade
    {
        #region private methods
        /// <summary>
        /// Reads returned data from database and puts the values in an ImageInfo object
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>ImageInfo object - CreationDate and DropBoxUrl</returns>
        public static ImageInfo ReadImage(IDataRecord reader)
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
        /// Gets the imageInfo list from database
        /// </summary>
        /// <param name="connString">SQL server database connectionstring</param>
        /// <returns>List of ImageInfo</returns>
        public static List<ImageInfo> GetImagesFromDb(string connString)
        {
            List<ImageInfo> result = new List<ImageInfo>();

            try
            {
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
                                    ImageInfo imgMeta = ReadImage(reader);
                                    result.Add(imgMeta);
                                }
                            }
                        }
                    }
                }
            }
            catch (InvalidOperationException invalidOperationException)
            {
                //TODO Create some kind of exception logging to return to service consumer
                Console.WriteLine(invalidOperationException.Message);
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine(sqlException.Errors);
            }
            catch (System.IO.IOException ioException)
            {
                Console.WriteLine(ioException.Message);
            }
            catch (System.Configuration.ConfigurationException confException)
            {
                Console.WriteLine(confException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        /// <summary>
        /// Inserts imageInfo into the database
        /// </summary>
        /// <param name="imgMeta"></param>
        /// <returns></returns>
        public static int StoreDataInDb(string connString, ImageInfo imgMeta)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection databaseConnection = new SqlConnection(connString))
                {
                    databaseConnection.Open();

                    using (
                        SqlCommand insertCommand =
                            new SqlCommand(
                                "insert into Images (Datetime,Link) values (@fileCreationDate, @sharedLink)",
                                databaseConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@fileCreationDate", imgMeta.FileCreationDate);
                        insertCommand.Parameters.AddWithValue("@sharedLink", imgMeta.SharedLink);
                        rowsAffected = insertCommand.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
            }
            catch (InvalidCastException castException)
            {
                Console.WriteLine(castException.Message);

            }
            catch (InvalidOperationException invalidOperationException)
            {
                //TODO Create some kind of exception logging to return to service consumer instead of using console writeline
                Console.WriteLine(invalidOperationException.Message);
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine(sqlException.Errors);
            }
            catch (System.IO.IOException ioException)
            {
                Console.WriteLine(ioException.Message);
            }
            catch (System.Configuration.ConfigurationException confException)
            {
                Console.WriteLine(confException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return rowsAffected;

        }

        #endregion
    }
}