using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Web;

namespace CameraRestService.Model
{
    [DataContract]
    public class Image
    {
        private DateTime datetime;
        private string link;
           
            [DataMember]
            public string Link
            {
                get { return link; }
                set { link  = value; }
            }
            [DataMember]
            public DateTime Datetime
            {
            get { return datetime; }
            set { datetime = value; }
             }
    
    }
}