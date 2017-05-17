using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI.WebControls;

namespace CameraRestService.Model
{

    public class Image
    {
        private DateTime _fileCreationDate;
        private string _fileName;
        private string _data;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public DateTime FileCreationDate
        {
            get { return _fileCreationDate; }
            set { _fileCreationDate = value; }
        }

        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public Image(DateTime fileCreationDate, string fileName, string data)
        {
            _fileCreationDate = fileCreationDate;
            _fileName = fileName;
            _data = data;
        }

        public Image()
        {

        }


        public Byte[] GetDataAsByteArray()
        {
            return Convert.FromBase64String(Data);
        }

        public void SetDataAsBase64String(Byte[] dataAsByteArray)
        {
            FileName = Convert.ToBase64String(dataAsByteArray);
        }
    }
}