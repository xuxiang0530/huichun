using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManager.DataManager;

namespace CompanyManager.model
{
    class celluser :MySqlConn
    {

        #region properity
        private int _userId;
        private string _name;
        private int _sex;               //0:男,1:女
        private int _cardType;          //1:身份证,2:护照,3:其他
        private string idCardNo;

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

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public int Sex
        {
            get
            {
                return _sex;
            }

            set
            {
                _sex = value;
            }
        }

        public int CardType
        {
            get
            {
                return _cardType;
            }

            set
            {
                _cardType = value;
            }
        }

        public string IdCardNo
        {
            get
            {
                return idCardNo;
            }

            set
            {
                idCardNo = value;
            }
        }
        #endregion
    }
}
