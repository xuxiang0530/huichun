using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManager.DataManager;
using MySql.Data.MySqlClient;

namespace CompanyManager.model
{
    class celluser :MySqlConn
    {

        private int _userId;
        private string _name;
        private int _sex;               //0:男,1:女
        private int _cardType;          //1:身份证,2:护照,3:其他
        private string _idCardNo;
        private string _phone;

        #region properity
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
                return _idCardNo;
            }

            set
            {
                _idCardNo = value;
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                _phone = value;
            }
        }
        #endregion

        public celluser()
        {
            UserId = -1;
        }

        public celluser(string userId)
        {
            if (userId.Equals("-1"))
            {
                UserId = -1;
            }
            else
            {
                string sqlStr = "SELECT userid,name,sex,cardtype,idcardno,phone FROM T_user where userid = @para1";
                string[] para = { userId };
                System.Data.DataTable dt = Select(sqlStr, para);

                UserId = Convert.ToInt16(userId);
                if (dt.Rows.Count > 0)
                {
                    Name = dt.Rows[0].ItemArray[dt.Columns.IndexOf("name")].ToString();
                    Sex = Convert.ToInt16(dt.Rows[0].ItemArray[dt.Columns.IndexOf("sex")]);
                    CardType = Convert.ToInt16(dt.Rows[0].ItemArray[dt.Columns.IndexOf("cardtype")]);
                    IdCardNo = dt.Rows[0].ItemArray[dt.Columns.IndexOf("idcardno")].ToString();
                    Phone = dt.Rows[0].ItemArray[dt.Columns.IndexOf("phone")].ToString();
                }
            }
        }

        private bool ExisttsUserByCartNo(string cartNo)
        {
            string sqlStr = "SELECT userid FROM T_user where idcardno = @para1";
            string[] para = { cartNo.Trim() };

            if(Select(sqlStr,para).Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsertUser()
        {
            string sqlStr = "insert into T_user (name,sex,cardtype,idcardno,phone) values(?name,?sex,?cardtype,?idcardno,?phone)";

            MySqlParameter[] para = new MySqlParameter[5];

            //para[0] = new MySqlParameter("?userid",MySqlDbType.int
            para[0] = new MySqlParameter("?name", MySqlDbType.VarChar, 50);
            para[1] = new MySqlParameter("?sex", MySqlDbType.Int16);
            para[2] = new MySqlParameter("?cardtype", MySqlDbType.Int16);
            para[3] = new MySqlParameter("?idcardno", MySqlDbType.VarChar, 30);
            para[4] = new MySqlParameter("?phone", MySqlDbType.VarChar, 30);

            para[0].Value = Name;
            para[1].Value = Sex;
            para[2].Value = CardType;
            para[3].Value = IdCardNo;
            para[4].Value = Phone;

            if(ExecuteNonQuery(sqlStr,para) > 0)
            {
                return true;
            }
            else
            {
                xuxSeecion.ERRMESSAGE = "人员插入失败";
                return false;
            }
        }


        private bool UpdateUser()
        {
            string sqlStr = "update T_user set name = ?name,sex = ?sex,cardtype = ?cardtype,idcardno = ?idcardno,phone = ?phone where userid = ?userid";

            MySqlParameter[] para = new MySqlParameter[6];

            //para[0] = new MySqlParameter("?userid",MySqlDbType.int
            para[0] = new MySqlParameter("?name", MySqlDbType.VarChar, 50);
            para[1] = new MySqlParameter("?sex", MySqlDbType.Int16);
            para[2] = new MySqlParameter("?cardtype", MySqlDbType.Int16);
            para[3] = new MySqlParameter("?idcardno", MySqlDbType.VarChar, 30);
            para[4] = new MySqlParameter("?phone", MySqlDbType.VarChar, 30);
            para[5] = new MySqlParameter("?userid", MySqlDbType.Int16);

            para[0].Value = Name;
            para[1].Value = Sex;
            para[2].Value = CardType;
            para[3].Value = IdCardNo;
            para[4].Value = Phone;
            para[5].Value = UserId;

            if (ExecuteNonQuery(sqlStr, para) > 0)
            {
                return true;
            }
            else
            {
                xuxSeecion.ERRMESSAGE = "人员更新失败";
                return false;
            }
        }

        public bool Save()
        {
            if(UserId == -1 && ExisttsUserByCartNo(IdCardNo))
            {
                xuxSeecion.ERRMESSAGE = "证件号已有";
                return false;
            }
            else if(UserId == -1)
            {
                return InsertUser();
            }
            else
            {
                return UpdateUser();
            }
        }
    }
}
