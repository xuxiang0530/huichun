using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManager.DataManager;

namespace CompanyManager.model
{
    class cell :MySqlConn
    {
        #region properity
        private int _cellid;
        private string _batchId;
        private int _contractid;
        private DateTime _cellgetdate;
        private int _celltypeid;
        private string _celltypename;
        private string _getuser;
        private string _getlocation;
        private int _userid;

        public int Cellid
        {
            get
            {
                return _cellid;
            }

            set
            {
                _cellid = value;
            }
        }

        public int Contractid
        {
            get
            {
                return _contractid;
            }

            set
            {
                _contractid = value;
            }
        }

        public DateTime Cellgetdate
        {
            get
            {
                return _cellgetdate;
            }

            set
            {
                _cellgetdate = value;
            }
        }

        public int Celltypeid
        {
            get
            {
                return _celltypeid;
            }

            set
            {
                _celltypeid = value;
            }
        }

        public string Celltypename
        {
            get
            {
                return _celltypename;
            }

            set
            {
                _celltypename = value;
            }
        }

        public string Getuser
        {
            get
            {
                return _getuser;
            }

            set
            {
                _getuser = value;
            }
        }

        public string Getlocation
        {
            get
            {
                return _getlocation;
            }

            set
            {
                _getlocation = value;
            }
        }

        public int Userid
        {
            get
            {
                return _userid;
            }

            set
            {
                _userid = value;
            }
        }

        public string BatchId
        {
            get
            {
                return _batchId;
            }

            set
            {
                _batchId = value;
            }
        }
        #endregion
    }
}
