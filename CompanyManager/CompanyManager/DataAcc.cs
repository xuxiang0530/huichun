using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Data.OleDb;

namespace CompanyManager
{
    class DataAcc
    {
        private static string sqlconn = "uid=hdbemployee;pwd=k3k4k5k6;data source=192.168.0.208; database=DB_DataInformation;Connect Timeout=30";
        
        public static string int_to_ABC(int i)
        {
            byte[] array = new byte[1];
            array[0] = (byte)(Convert.ToInt32(i + 64)); //ASCII码强制转换二进制
            return Convert.ToString(System.Text.Encoding.ASCII.GetString(array));
        }

        public static DataSet readXls_2007(string FileNameFull)
        {
            DataSet ds = new DataSet();
            string strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileNameFull + ";Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1\"";
            OleDbConnection myConn = new OleDbConnection(strCon);
            myConn.Open();
            string strCom;
            StringCollection SheetNames = ExcelSheetName(FileNameFull);
            foreach (string sheet in SheetNames)
            {
                strCom = " SELECT * FROM [" + sheet + "]";
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
                DataSet myDataSet = new DataSet();
                myCommand.Fill(myDataSet, sheet);
                System.Data.DataTable da = myDataSet.Tables[0];
                myDataSet.Tables.Remove(myDataSet.Tables[0]);
                if(da.Rows.Count > 0)
                    ds.Tables.Add(da);
            }

            myConn.Close();

            return ds;
        }

        public static StringCollection ExcelSheetName(string filepath)
        {
            StringCollection names = new StringCollection();
            string strConn;
            strConn = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=2'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            System.Data.DataTable sheetNames = conn.GetOleDbSchemaTable
            (System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            conn.Close();
            foreach (DataRow dr in sheetNames.Rows)
            {
                names.Add(dr[2].ToString());
            }
            return names;
        }
        public static int DoCommand(string sql)
        {
            int r = -1;
            string conn = sqlconn;
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                r = cmd.ExecuteNonQuery();
            }
            catch (Exception e) { writeTxt(e.Message); }
            finally
            {
                con.Close();
            }
            return r;
        }
        public static string FilePathFull()
        {
            //string filepath = System.AppDomain.CurrentDomain.BaseDirectory;
            OpenFileDialog flog = new OpenFileDialog();
            //flog.InitialDirectory = filepath;
            DialogResult result = flog.ShowDialog();
            if (result == DialogResult.OK)//确定事件响应
            {
                return flog.FileName;
            }
            else
            {
                return "";
            }
        }

        public static void writeTxt(string Txt)
        {
            //string a = System.AppDomain.CurrentDomain.BaseDirectory + "&" +System.AppDomain.CurrentDomain.RelativeSearchPath;
            StreamWriter sw = new StreamWriter(@System.AppDomain.CurrentDomain.BaseDirectory + "\\LOG.txt", true, System.Text.Encoding.Default);
            sw.WriteLine(DateTime.Now.ToString() + "  " + Txt);
            sw.Close();
            //			FileStream.TextFile=File.Open(@"D:\xuxwork\LOG.txt",FileMode.Append);
            //　			byte [] Info = 
            //　			TextFile.Write(Info,0,Info.Length);
            //　			TextFile.Close();
        }
        public static int ABC_to_int(string str)
        {
            byte[] array = new byte[1];   //定义一组数组array
            array = System.Text.Encoding.ASCII.GetBytes(str); //string转换的字母
            int asciicode = (short)(array[0]);
            return Convert.ToInt16(Convert.ToString(asciicode)) - 64; //将转换一的ASCII码转换成int型
        }
        public static string GetMD5Hash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            char[] temp = new char[res.Length];
            System.Array.Copy(res, temp, res.Length);
            return new String(temp);
        }
        public static DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();
            string conn = sqlconn;
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandTimeout = 180;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            return ds;
        }

        public static bool IsNumeric(string value)
        {
            //return Regex.IsMatch(value, @"^\d*\d$ ");
            if (value.Length == 0)
                return false;
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }
        public static bool IsIntNum(string str)
        {
            System.Text.RegularExpressions.Regex reg1
            = new System.Text.RegularExpressions.
                Regex(@"^[-]?[0-9]{1}\d*$|^[0]{1}$");
            bool ismatch = reg1.IsMatch(str);
            //if (!ismatch)
            //    MessageBox.Show("您输入的数字不是整数！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return ismatch;
        }
        public static void writeFileFull(string filePath, string Txt)
        {
            StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.Default);
            sw.Write(Txt);
            sw.Close();
        }
        public static List<string> readCsvSkipFirstRow(string filePath)
        {
            //开始读取文本文件
            //FileStream fs = new FileStream(filePath, FileMode.Open);      //12月11日更改，此方法会独占文本文件
            //FileStream fs = File.OpenRead(filePath);
            //下面方法实现内容为：FileMode.Open ，打开文件//FileAccess.Read，文件操作类型为读取// FileShare.ReadWrite，允许其他进程读和写文件
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            System.IO.StreamReader srcsv = new StreamReader(fs, System.Text.Encoding.GetEncoding("GB2312"));
            List<string> strline = new List<string>();
            string strnewline = srcsv.ReadLine();
            strnewline = srcsv.ReadLine();

            while (strnewline != null && strnewline.Trim().Length > 2)
            {
                strline.Add(strnewline);
                strnewline = srcsv.ReadLine();
            }
            srcsv.Close();
            fs.Close();
            return strline;
        }
        public static List<string> rowItem(string str)
        {
            string[] strItem = str.Split(',');
            bool bz = false;
            List<string> re = new List<string>();
            string temp = "";
            string OneItem = "";

            foreach (string t in strItem)
            {
                if (t.Length > 1 && t.Substring(0, 1).Equals("\"") && t.Substring(t.Length - 1, 1).Equals("\""))
                    temp = t.Substring(1, t.Length - 2);
                else
                    temp = t;

                if (temp.Contains('\"'))
                {
                    bz = !bz;
                }
                if (bz)
                {
                    OneItem += temp.Trim() + ",";
                }
                else
                {
                    if (OneItem.Equals(""))
                    {
                        re.Add((OneItem + temp).Trim());
                        OneItem = "";
                    }
                    else
                    {
                        re.Add(temp);
                    }
                }
            }

            return re;

        }
        public static List<string> rowItemTB(string str)
        {
            string[] strItem = str.Split('\t');
            bool bz = false;
            List<string> re = new List<string>();
            string temp = "";
            string OneItem = "";

            foreach (string t in strItem)
            {
                if (t.Length > 1 && t.Substring(0, 1).Equals("\"") && t.Substring(t.Length - 1, 1).Equals("\""))
                    temp = t.Substring(1, t.Length - 2);
                else
                    temp = t;

                if (temp.Contains('\"'))
                {
                    bz = !bz;
                }
                if (bz)
                {
                    OneItem += temp.Trim() + ",";
                }
                else
                {
                    if (OneItem.Equals(""))
                    {
                        re.Add((OneItem + temp).Trim());
                        OneItem = "";
                    }
                    else
                    {
                        re.Add(temp);
                    }
                }
            }

            return re;

        }
        public static string[] readCsvFirstRowItem(string filePath)
        {
            //开始读取文本文件
            //FileStream fs = new FileStream(filePath, FileMode.Open);      //12月11日更改，此方法会独占文本文件
            //FileStream fs = File.OpenRead(filePath);
            //下面方法实现内容为：FileMode.Open ，打开文件//FileAccess.Read，文件操作类型为读取// FileShare.ReadWrite，允许其他进程读和写文件
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            System.IO.StreamReader srcsv = new StreamReader(fs, System.Text.Encoding.GetEncoding("GB2312"));
            string[] strline = {};
            string strnewline = srcsv.ReadLine();
            if (strnewline != null && strnewline.Length > 2)
            {
                string[] FirstRowItem = strnewline.Split(',');
                return FirstRowItem;
            }
            srcsv.Close();
            fs.Close();
            return strline;
        }
        public static string readCsvFirstRow(string filePath)
        {
            //开始读取文本文件
            //FileStream fs = new FileStream(filePath, FileMode.Open);      //12月11日更改，此方法会独占文本文件
            //FileStream fs = File.OpenRead(filePath);
            //下面方法实现内容为：FileMode.Open ，打开文件//FileAccess.Read，文件操作类型为读取// FileShare.ReadWrite，允许其他进程读和写文件
            FileStream fs = new FileStream(filePath,FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            System.IO.StreamReader srcsv = new StreamReader(fs, System.Text.Encoding.GetEncoding("GB2312"));
            string strnewline = srcsv.ReadLine();
            if (strnewline != null && strnewline.Length > 2)
            {
                return strnewline;
            }
            srcsv.Close();
            fs.Close();
            return "";
        }


        public static string FolderPathFull()
        {
            //string filepath = System.AppDomain.CurrentDomain.BaseDirectory;
            FolderBrowserDialog flog = new FolderBrowserDialog();
            //flog.SelectedPath = filepath;
            DialogResult result = flog.ShowDialog();
            if (result == DialogResult.OK)//确定事件响应
            {
                return flog.SelectedPath;
            }
            else
            {
                return "";
            }
        }

        public static string readTxtLastLogin()
        {
            string str = "";
            if (File.Exists(@System.AppDomain.CurrentDomain.BaseDirectory + "\\LastLoginUser.txt"))
            {
                FileStream fs = new FileStream(@System.AppDomain.CurrentDomain.BaseDirectory + "\\LastLoginUser.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                System.IO.StreamReader srcsv = new StreamReader(fs, System.Text.Encoding.GetEncoding("GB2312"));
                str = srcsv.ReadToEnd();
                srcsv.Close();
                fs.Close();
            }
            return str;
        }

        public static void writeLastLoginUser(string LastLoginUserMail)
        {

            FileStream fs = new FileStream(@System.AppDomain.CurrentDomain.BaseDirectory + "\\LastLoginUser.txt", FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite);

            System.IO.StreamWriter srcsv = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));
            srcsv.Write(LastLoginUserMail);
            srcsv.Close();
            fs.Close();
        }

        public static List<string> readCsvRow(string filePath)
        {
            //开始读取文本文件
            //FileStream fs = new FileStream(filePath, FileMode.Open);      //12月11日更改，此方法会独占文本文件
            //FileStream fs = File.OpenRead(filePath);
            //下面方法实现内容为：FileMode.Open ，打开文件//FileAccess.Read，文件操作类型为读取// FileShare.ReadWrite，允许其他进程读和写文件

            List<string> re = new List<string>();
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            System.IO.StreamReader srcsv = new StreamReader(fs, System.Text.Encoding.GetEncoding("GB2312"));
            string strnewline = srcsv.ReadLine();
            while (strnewline != null )
            {
                strnewline = strnewline.Replace("\0", "");

                if (strnewline.Length < 2)
                {
                    strnewline = srcsv.ReadLine();
                    continue;
                }

                re.Add(replaceTB(strnewline));
                strnewline = srcsv.ReadLine();
            }
            srcsv.Close();
            fs.Close();
            return re;
        }

        public static string replaceTB(string str)
        {
            string[] strItem = str.Split(',');
            bool bz = false;
            string re = "";
            string temp = "";

            foreach (string t in strItem)
            {
                if (t.Length > 1 && t.Substring(0, 1).Equals("\"") && t.Substring(t.Length - 1, 1).Equals("\""))
                    temp = t.Substring(1, t.Length - 2);
                else
                    temp = t;
                if (temp.Contains('\"'))
                {
                    bz = !bz;
                }
                if (bz)
                {
                    re += temp + ",";
                }
                else
                {
                    re += temp + '\t';
                }
            }

            re = re.Substring(0, re.Length - 1);
            return re;

        }
    }
}
