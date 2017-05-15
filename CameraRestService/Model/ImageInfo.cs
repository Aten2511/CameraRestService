using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CameraRestService.Model
{
    public class ImageInfo
    {

        private DateTime _fileCreationDate;
        private string _remoteFolder;
        private string _fileName;
        private string _sharedLink;

        public DateTime FileCreationDate
        {
            get { return _fileCreationDate; }
            set { _fileCreationDate = value; }
        }

        public string RemoteFolder
        {
            get { return _remoteFolder; }
            set { _remoteFolder = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public string SharedLink
        {
            get { return _sharedLink; }
            set { _sharedLink = value; }
        }

        public ImageInfo()
        {

        }

        public ImageInfo(DateTime fileCreationDate, string remoteFolder, string fileName, string sharedLink)
        {
            _fileCreationDate = fileCreationDate;
            _remoteFolder = remoteFolder;
            _fileName = fileName;
            _sharedLink = sharedLink;
        }
    }
}