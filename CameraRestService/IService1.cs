﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CameraRestService.Model;

namespace CameraRestService
{
    //uri:
    //http://camerarestservice.azurewebsites.net/Service1.svc/images/
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
    UriTemplate = "images/")]
        bool PostImage(string input);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "images/")]
        IList<ImageInfo> GetImages();
    }


   
}
