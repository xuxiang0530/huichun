using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.model
{
    class contract
    {
        #region properity
        private int _contractId;
        private int _userId;
        private string _contractNo;
        private byte[] _contractFile;

        public int ContractId
        {
            get
            {
                return _contractId;
            }

            set
            {
                _contractId = value;
            }
        }

        public int UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                _userId = value;
            }
        }

        public string ContractNo
        {
            get
            {
                return _contractNo;
            }

            set
            {
                _contractNo = value;
            }
        }

        public byte[] ContractFile
        {
            get
            {
                return _contractFile;
            }

            set
            {
                _contractFile = value;
            }
        }
        #endregion
    }
}
