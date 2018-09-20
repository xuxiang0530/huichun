using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.model
{
    class Hfile
    {
        #region properity
        private int _fileid;
        private string _fileName;
        private string _fileFullName;
        private DateTime _upLoadTime;
        private int _uploadUserId;
        private bool _disableFlag;
        private byte[] _fileData;
        private string _note;

        public int Fileid
        {
            get
            {
                return _fileid;
            }

            set
            {
                _fileid = value;
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        }

        public string FileFullName
        {
            get
            {
                return _fileFullName;
            }

            set
            {
                _fileFullName = value;
            }
        }

        public DateTime UpLoadTime
        {
            get
            {
                return _upLoadTime;
            }

            set
            {
                _upLoadTime = value;
            }
        }

        public int UploadUserId
        {
            get
            {
                return _uploadUserId;
            }

            set
            {
                _uploadUserId = value;
            }
        }

        public bool DisableFlag
        {
            get
            {
                return _disableFlag;
            }

            set
            {
                _disableFlag = value;
            }
        }

        public byte[] FileData
        {
            get
            {
                return _fileData;
            }

            set
            {
                _fileData = value;
            }
        }

        public string Note
        {
            get
            {
                return _note;
            }

            set
            {
                _note = value;
            }
        }
        #endregion

        public bool save()
        {
            string sqlstr = "";

            return true;
        }
    }
}
