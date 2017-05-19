using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CameraRestService.Model;

namespace CameraRestServiceTests
{
    [TestClass]
    public class ImageInfoTests
    {
        public ImageInfo TestObj { get; set; }

        [TestInitialize]
        public void Initialize()
        {
           TestObj = new ImageInfo();
        }

        [TestMethod]
        public void SetAndGetFileNameTest()
        {
            //Arrange
            string expectedValue = "cat.jpg";

            //Act
            TestObj.FileName = expectedValue;

            //Assert
            Assert.AreEqual(expectedValue, TestObj.FileName);
        }

        [TestMethod]
        public void SetAndGetDateTimeTest()
        {
            //Arrange
            DateTime expectedValue = DateTime.Now;

            //Act
            TestObj.FileCreationDate = expectedValue;
            
            //Assert
            Assert.AreEqual(expectedValue, TestObj.FileCreationDate);
        }

        [TestMethod]
        public void SetAndGetRemoteFolderPathTest()
        {
            //Arrange
            string expectedValue = "/DropboxFolder";

            //Act
            TestObj.RemoteFolder = expectedValue;

            //Assert
            Assert.AreEqual(expectedValue, TestObj.RemoteFolder);
        }

        [TestMethod]
        public void SetAndGetSharedLinkTest()
        {
            //Arrange
            string expectedValue = "http://somelinkaddress.com";

            //Act
            TestObj.SharedLink = expectedValue;

            //Assert
            Assert.AreEqual(expectedValue, TestObj.SharedLink);
        }

        [TestMethod]
        public void CreateNewObjectUsingOverloadConstructorValuesShouldBeSetTest()
        {
            //Arrange
            //June 6th 2015, 20:30:00
             DateTime creationDate = new DateTime(2015, 06, 30,20,30,0);
            string remotePath = "/someDropBoxFolderPath";
            string fileName = "cutecat.jpg";
            string sharedLink = "http://pathtomypicture.com/cutecat";

            //Act
            TestObj = new ImageInfo(creationDate,remotePath,fileName,sharedLink);

            Assert.AreEqual(creationDate,TestObj.FileCreationDate);
            Assert.AreEqual(creationDate, TestObj.RemoteFolder);
            Assert.AreEqual(creationDate, TestObj.FileName);
            Assert.AreEqual(creationDate, TestObj.SharedLink);

        }

    }
}
