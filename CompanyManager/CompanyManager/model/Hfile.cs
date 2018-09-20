using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManager.DataManager;
using MySql.Data.MySqlClient;

namespace CompanyManager.model
{
    class Hfile : MySqlConn
    {
        #region properity
        private int _fileid;
        private int _filetypeid;
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

        public int Filetypeid
        {
            get
            {
                return _filetypeid;
            }

            set
            {
                _filetypeid = value;
            }
        }
        #endregion

        public Hfile()
        {
            Fileid = -1;

        }

        public bool save()
        {
            if(Fileid == -1)
            {
                return false;
            }
            string sqlstr = "insert into T_file (filetypeid,filename,filefullname,uploaddate,uploaduserid,disableflag,filedata,note) values (@para1,@para2,@para3,@para4,@para5,@para6,@para7)";
//            MySqlParameter[] para
//                //mysql

//string cmd = "select userBackImage from users where userName=?name";
//            MySqlParameter para = new MySqlParameter("?name", MySqlDbType.VarChar, 100);
//            para.Value = userName;

//            MySqlDataReader reader = MYSQLHelper.ExecuteReader(CommandType.Text, cmd, para);
            return true;
        }
    }
}
