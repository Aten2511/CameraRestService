using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI.WebControls;

namespace CameraRestService.Model
{
    [DataContract]
    public class Image
    {
        private DateTime _fileCreationDate;
        private string _fileName;
        private Byte[] _data;

        [DataMember]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        [DataMember]
        public DateTime FileCreationDate
        {
            get { return _fileCreationDate; }
            set { _fileCreationDate = value; }
        }

        [DataMember]
        public Byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }


    }
}