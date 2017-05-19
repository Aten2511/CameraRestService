using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraRestService;
using CameraRestService.Model;
using System.Data;

namespace CameraRestServiceTests
{
    [TestClass]
    public class Service1Tests
    {
        public Service1 Service1Obj { get; set; }

        [TestInitialize]
        public void CreateServiceObjInitializer()
        {
            Service1Obj = new Service1();
        }

        [TestMethod]
        public void GetListNotNullTest()
        {
            //Arrange
            IList<ImageInfo> listFromDataBase = new List<ImageInfo>();

            //Act
            listFromDataBase = Service1Obj.GetImages();

            //Assert
            Assert.IsNotNull(listFromDataBase);
        }

        [TestMethod]
        public void PostLinkShouldReturnTrueTest()
        {
            //Arrange
            string linkToPost = "http://somewhereontheinternet.com/cutecat";

            //Act
            bool returnValue = Service1Obj.PostImage(linkToPost);

            //Assert
            Assert.IsTrue(returnValue);
        }

    }
}
