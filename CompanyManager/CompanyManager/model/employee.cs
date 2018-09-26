using System;
using CompanyManager.DataManager;
using System.Data;
using MySql.Data.MySqlClient;
namespace CompanyManager.model
{
    class employee : MySqlConn
    {
        #region properity
        private int _userid;
        private string _username;
        private string _englishname;
        private int _usertypeid;
        private int _userpowerid;
        private DateTime _birthday;
        private int _cardtype;
        private string _idcardno;
        private int _sex;
        private int _employeeno;
        private DateTime _joindate;
        private DateTime _outdate;
        private bool _onjob;
        private string _email;
        private string _pwd;
        
        public employee()
        {
            Userid = -1;
            Username = "";
        }

        public bool Save()
        {
            bool f = false;
            if(Userid == -1 && Username.Equals(""))
            {
                return f;
            }
            else if (Userid == -1)
            {
                f = insertUser();
            }
            else
            {
                f = updateUser();
            }


            return f;
        }

        private bool updateUser()
        {
            bool f = false;
            string sqlstr = @"UPDATE T_employeeInfo SET 
                username = ?username
                ,englishname = ?englishname
                ,usertypeid = ?usertypeid
                ,userpowerid = ?userpowerid
                ,birthday = ?birthday
                ,cardtype = ?cardtype
                ,idcardno = ?idcardno
                ,sex = ?sex
                ,employeeno = ?employeeno
                ,joindate = ?joindate
                ,outdate = ?outdate
                ,onjob = ?onjob
                ,email = ?email
                ,pwd = ?pwd
                WHERE userid = ?userid";
            MySqlParameter[] para = new MySqlParameter[15];

            //para[0] = new MySqlParameter("?userid",MySqlDbType.int
            para[0] = new MySqlParameter("?username", MySqlDbType.VarChar, 50);
            para[1] = new MySqlParameter("?englishname", MySqlDbType.VarChar, 50);
            para[2] = new MySqlParameter("?usertypeid", MySqlDbType.Int16);
            para[3] = new MySqlParameter("?userpowerid", MySqlDbType.Int16);
            para[4] = new MySqlParameter("?birthday", MySqlDbType.Date);
            para[5] = new MySqlParameter("?cardtype", MySqlDbType.Int16);
            para[6] = new MySqlParameter("?idcardno", MySqlDbType.VarChar, 30);
            para[7] = new MySqlParameter("?sex", MySqlDbType.Int16);
            para[8] = new MySqlParameter("?employeeno", MySqlDbType.Int16);
            para[9] = new MySqlParameter("?joindate", MySqlDbType.Date);
            para[10] = new MySqlParameter("?outdate", MySqlDbType.Date);
            para[11] = new MySqlParameter("?onjob", MySqlDbType.Int16);
            para[12] = new MySqlParameter("?email", MySqlDbType.VarChar, 100);
            para[13] = new MySqlParameter("?pwd", MySqlDbType.VarChar, 200);
            para[14] = new MySqlParameter("?userid", MySqlDbType.Int16);

            para[0].Value = Username;
            para[1].Value = Englishname;
            para[2].Value = Usertypeid;
            para[3].Value = Userpowerid;
            para[4].Value = Birthday.ToShortDateString();
            para[5].Value = Cardtype;
            para[6].Value = Idcardno;
            para[7].Value = Sex;
            para[8].Value = Employeeno;
            para[9].Value = Joindate.ToShortDateString();
            para[10].Value = Onjob;
            para[11].Value = Email;
            para[12].Value = Pwd;
            para[13].Value = Userid;

            if (ExecuteNonQuery(sqlstr, para) > 0)
            {
                f = true;
            }
            else
            {
                f = false;
            }

            return f;
        }

        private bool insertUser()
        {
            bool f = false;
            string sqlstr = "INSERT INTO T_employeeInfo (username, englishname, usertypeid, userpowerid, birthday, cardtype, idcardno, sex, employeeno, joindate, onjob, email, pwd, outdate) values(?username,?englishname,?usertypeid,?userpowerid,?birthday,?cardtype,?idcardno,?sex,?employeeno,?joindate,?onjob,?email,?pwd,?outdate)";
            MySqlParameter[] para = new MySqlParameter[14];

            //para[0] = new MySqlParameter("?userid",MySqlDbType.int
            para[0] = new MySqlParameter("?username", MySqlDbType.VarChar, 50);
            para[1] = new MySqlParameter("?englishname", MySqlDbType.VarChar, 50);
            para[2] = new MySqlParameter("?usertypeid", MySqlDbType.Int16);
            para[3] = new MySqlParameter("?userpowerid", MySqlDbType.Int16);
            para[4] = new MySqlParameter("?birthday", MySqlDbType.Date);
            para[5] = new MySqlParameter("?cardtype", MySqlDbType.Int16);
            para[6] = new MySqlParameter("?idcardno", MySqlDbType.VarChar, 30);
            para[7] = new MySqlParameter("?sex", MySqlDbType.Int16);
            para[8] = new MySqlParameter("?employeeno", MySqlDbType.Int16);
            para[9] = new MySqlParameter("?joindate", MySqlDbType.Date);
            para[10] = new MySqlParameter("?onjob", MySqlDbType.Int16);
            para[11] = new MySqlParameter("?email", MySqlDbType.VarChar, 100);
            para[12] = new MySqlParameter("?pwd", MySqlDbType.VarChar, 200);
            para[13] = new MySqlParameter("?outdate", MySqlDbType.Date);

            para[0].Value = Username;
            para[1].Value = Englishname;
            para[2].Value = Usertypeid;
            para[3].Value = Userpowerid;
            para[4].Value = Birthday.ToShortDateString();
            para[5].Value = Cardtype;
            para[6].Value = Idcardno;
            para[7].Value = Sex;
            para[8].Value = Employeeno;
            para[9].Value = Joindate.ToShortDateString();
            para[10].Value = Onjob;
            para[11].Value = Email;
            para[12].Value = Pwd;

            if (ExecuteNonQuery(sqlstr, para) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            return f;
        }

        public employee(string email,string pwd)
        {
            //test();
            string[] para = new string[] { email, pwd };

            DataTable dt = Select("select userid,username,englishname,usertypeid,userpowerid,birthday,cardtype,idcardno,sex,employeeno,joindate," 
                + "onjob,email,outdate from T_employeeInfo where email = @para1 and pwd = @para2 and onjob = 1", para);

            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                Userid = Convert.ToInt16( dr.ItemArray[dt.Columns.IndexOf("userid")]);
                Username = dr.ItemArray[dt.Columns.IndexOf("username")].ToString();
                Englishname = dr.ItemArray[dt.Columns.IndexOf("englishname")].ToString();
                Usertypeid = Convert.ToInt16(dr.ItemArray[dt.Columns.IndexOf("usertypeid")]);
                Userpowerid = Convert.ToInt16(dr.ItemArray[dt.Columns.IndexOf("userpowerid")]);
                Birthday = Convert.ToDateTime(dr.ItemArray[dt.Columns.IndexOf("birthday")]);
                Cardtype = Convert.ToInt16(dr.ItemArray[dt.Columns.IndexOf("Cardtype")]);
                Idcardno = dr.ItemArray[dt.Columns.IndexOf("idcardno")].ToString();
                Sex = Convert.ToInt16(dr.ItemArray[dt.Columns.IndexOf("sex")]);
                //Employeeno = Convert.ToInt16(dr.ItemArray[dt.Columns.IndexOf("employeeno")]);
                Joindate = Convert.ToDateTime(dr.ItemArray[dt.Columns.IndexOf("joindate")]);
                Outdate = Convert.ToDateTime(dr.ItemArray[dt.Columns.IndexOf("outdate")]);
                Onjob = Convert.ToInt16(dr.ItemArray[dt.Columns.IndexOf("onjob")]) == 1 ?true:false;
                Email = email ;
                Pwd = pwd;
            }
            else
            {
                Userid = -1;
            }
        }


        #region 测试代码
        //public void test()
        //{
        //    string[] para1 = new string[] { "test" };
        //    string[] para2 = new string[] { "test", "test" };
        //    string[] para3 = new string[] { "test", "test", "test" };
        //    string[] para4 = new string[] { "test", "test", "test", "test" };


        //    DataTable dt = Select("select * from TD_power");
        //    if (dt != null && dt.Rows.Count == 0)
        //    {
        //        int rows = ExecuteNonQuery("insert into TD_power (powername,note) values (@para1,@para2)", para2);
        //    }

        //    dt = Select("select * from TD_type");
        //    if (dt != null && dt.Rows.Count == 0)
        //    {
        //        int rows = ExecuteNonQuery("insert into TD_type (typename) values (@para1)", para1);
        //    }

        //    dt = Select("select * from T_employeeInfo");


        //    if (dt != null && dt.Rows.Count == 0)
        //    {
        //        string[] parauser = new string[] { "xuxiang", "xux", "1", "1", "1980-05-30", "1", "310103198005301616", "1", "2018-09-01", "1", "tianyaxx@163.com", "test".Md5() };
        //        int rows = ExecuteNonQuery("insert into T_employeeInfo (username,englishname,usertypeid,userpowerid,birthday,cardtype,idcardno,sex,joindate,onjob,email,pwd) values (@para1,@para2,@para3,@para4,@para5,@para6,@para7,@para8,@para9,@para10,@para11,@para12)", parauser);
        //    }


        //}
        #endregion

        public int Userid
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
                _employeeno = value;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
            }
        }

        public string Englishname
        {
            get
            {
                return _englishname;
            }

            set
            {
                _englishname = value;
            }
        }

        public int Usertypeid
        {
            get
            {
                return _usertypeid;
            }

            set
            {
                _usertypeid = value;
            }
        }

        public int Userpowerid
        {
            get
            {
                return _userpowerid;
            }

            set
            {

                _userpowerid = value;
            }
        }

        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }

            set
            {
                _birthday = value;
            }
        }

        public int Cardtype
        {
            get
            {
                return _cardtype;
            }

            set
            {
                _cardtype = value;
            }
        }

        public string Idcardno
        {
            get
            {
                return _idcardno;
            }

            set
            {
                _idcardno = value;
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

        public int Employeeno
        {
            get
            {
                return _userid;
            }

            //set
            //{
            //    _employeeno = value;
            //}
        }

        public DateTime Joindate
        {
            get
            {
                return _joindate;
            }

            set
            {
                _joindate = value;
            }
        }

        public bool Onjob
        {
            get
            {
                return _onjob;
            }

            set
            {
                _onjob = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }

        public string Pwd
        {
            get
            {
                return _pwd;
            }

            set
            {
                _pwd = value;
            }
        }

        public DateTime Outdate
        {
            get
            {
                return _outdate;
            }

            set
            {
                _outdate = value;
            }
        }
        #endregion


        public int SavePwd()
        {

            return ExecuteNonQuery("update T_employeeInfo set pwd = @para1", new string[] { Pwd });
        }
    }
}
