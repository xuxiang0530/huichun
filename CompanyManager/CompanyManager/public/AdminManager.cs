using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyManager.DataManager;
using CompanyManager.model;

namespace CompanyManager
{
    public partial class AdminManager : Form
    {
        DataTable userTypeTbl;
        DataTable userPowerTbl;
        DataTable fileTypeTbl;
        int logisticsTypeId = -1;

        public AdminManager()
        {
            InitializeComponent();
            init();


            
            //if (dtUserList.Rows.Count > 0)
            //{
            //    string[] cn = new string[dtUserList.Rows.Count];
            //    for (int i = 0; i < cn.Length; i++)
            //    {
            //        cn[i] = dtUserList.Rows[i].ItemArray[2].ToString();
            //    }
            //    comboBox1.AutoCompleteCustomSource.AddRange(cn);
            //}
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        #region 文件管理
        private void sx_dataGridview1fileType()
        {
            string sqlstr = "SELECT a.filetypeid id,a.typename,note  FROM TD_filetype a";
            DataTable dt = MySqlConn.GetDataSet(string.Format(sqlstr)).Tables[0];
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[dt.Columns.IndexOf("typename")].HeaderCell.Value = "文件类型";
            dataGridView1.Columns[dt.Columns.IndexOf("note")].HeaderCell.Value = "备注";
        }

        /// <summary>
        /// 文件类型保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string fileTypeName = textBox1.Text.Trim();
            string note = textBox5.Text.Trim();

            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (dr.Cells[1].Value.ToString().Equals(fileTypeName))
                {
                    MessageBox.Show("已有..");
                    return;
                }
            }
            if (fileTypeName.Equals(""))
            {
                MessageBox.Show("value is null");
                return;
            }
            else
            {
                string strSql = string.Format("INSERT INTO TD_filetype(typename,note) VALUES(N'{0}',N'{1}')", fileTypeName, note);

                MySqlConn.DoCommand(strSql);
                textBox1.Text = "";
                textBox5.Text = "";
                sx_dataGridview1fileType();
                MessageBox.Show("Successfully");
            }

        }
        #endregion

        #region 人员管理
        private void sx_dataGridview2userType()
        {
            string sqlstr = "SELECT a.id,a.typename  FROM TD_type a";
            DataTable dt = MySqlConn.GetDataSet(string.Format(sqlstr)).Tables[0];
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = dt;

            dataGridView2.Columns[dt.Columns.IndexOf("typename")].HeaderCell.Value = "人员类型";
        }

        private void sx_dataGridview4userPower()
        {
            string sqlstr = "select id,powername ,note  from TD_power;";
            DataTable dt = MySqlConn.GetDataSet(string.Format(sqlstr)).Tables[0];
            dataGridView4.DataSource = null;
            dataGridView4.DataSource = dt;

            dataGridView4.Columns[dt.Columns.IndexOf("powername")].HeaderCell.Value = "权限名称";
            dataGridView4.Columns[dt.Columns.IndexOf("note")].HeaderCell.Value = "备注";
        }


        //鼠标单击事件 ,好像暂时不需要,先不实现了
        private void dataGridView1_CellClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int index = dataGridView1.CurrentRow.Index;

                DataGridViewCellCollection cl = dataGridView1.Rows[index].Cells;
                
            }
        }

        /// <summary>
        /// 人员类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string userType = textBox2.Text.Trim();


            foreach (DataGridViewRow dr in dataGridView2.Rows)
            {
                if (dr.Cells[1].Value.ToString().Equals(userType))
                {
                    MessageBox.Show("已有..");
                    return;
                }
            }

            if (userType.Equals(""))
            {
                MessageBox.Show("value is null");
                return;
            }
            else
            {
                string strSql = string.Format("INSERT INTO TD_type(typename) VALUES(N'{0}')", userType);

                if (MySqlConn.DoCommand(strSql) > 0)
                {
                    textBox2.Text = "";
                    sx_dataGridview2userType();
                    MessageBox.Show("Successfully");
                }
                else
                {
                    MessageBox.Show("error");
                }
            }
        }

        /// <summary>
        /// 人员权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string powerName = textBox3.Text.Trim();
            string note = textBox4.Text.Trim();

            string strSql = "INSERT INTO TD_power(powername,note) VALUES(N'{0}',N'{1}')";

            foreach (DataGridViewRow dr in dataGridView4.Rows)
            {
                if (dr.Cells[1].Value.ToString().Equals(powerName))
                {
                    MessageBox.Show("已有..");
                    return;
                }
            }

            if (powerName.Equals(""))
            {
                MessageBox.Show("value is null");
                return;
            }
            else
            {
                if (
            MySqlConn.DoCommand(string.Format(strSql, powerName, note))
                > 0)
                {
                    sx_dataGridview4userPower();
                    textBox3.Text = "";
                    textBox4.Text = "";

                    MessageBox.Show("Seccessfully");
                }
                else
                {
                    MessageBox.Show("sql error");
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        #endregion




        #region 物流类型
        private void button7_Click(object sender, EventArgs e)
        {
            string logisticsTypeName = textBox6.Text.Trim();


            foreach (DataGridViewRow dr in dataGridView8.Rows)
            {
                if (dr.Cells[1].Value.ToString().Equals(logisticsTypeName))
                {
                    MessageBox.Show("已有..");
                    return;
                }
            }

            if (logisticsTypeName.Equals(""))
            {
                MessageBox.Show("value is null");
                return;
            }
            else
            {
                string strSql = string.Format("INSERT INTO TD_logisticsType(typename) VALUES(N'{0}')", logisticsTypeName);

                if (MySqlConn.DoCommand(strSql) > 0)
                {
                    textBox6.Text = "";
                    sx_dataGridview8LogisticsType();
                    MessageBox.Show("Successfully");
                }
                else
                {
                    MessageBox.Show("error");
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string logisticsTypeName = textBox6.Text.Trim();
            if (logisticsTypeId == -1)
            {
                MessageBox.Show("未选择物流类型");
                return;
            }
            foreach (DataGridViewRow dr in dataGridView8.Rows)
            {
                if (dr.Cells[1].Value.ToString().Equals(logisticsTypeName))
                {
                    MessageBox.Show("与现有类型重名");
                    return;
                }
            }

            if (logisticsTypeName.Equals(""))
            {
                MessageBox.Show("value is null");
                return;
            }
            else
            {
                string strSql = string.Format("UPDATE TD_logisticsType SET typename =N'{0}' WHERE typeid = {1}", logisticsTypeName,logisticsTypeId.ToString());

                if (MySqlConn.DoCommand(strSql) > 0)
                {
                    textBox6.Text = "";
                    sx_dataGridview8LogisticsType();
                    logisticsTypeId = -1;
                    MessageBox.Show("Successfully");
                    
                }
                else
                {
                    MessageBox.Show("error");
                }
            }
        }

        private void sx_dataGridview8LogisticsType()
        {
            string sqlstr = "SELECT a.typeid id,a.typename  FROM TD_logisticsType a";
            DataTable dt = MySqlConn.GetDataSet(string.Format(sqlstr)).Tables[0];
            dataGridView8.DataSource = null;
            dataGridView8.DataSource = dt;

            dataGridView8.Columns[dt.Columns.IndexOf("typename")].HeaderCell.Value = "文件类型";
        }

        private void dataGridView8_CellClick(object sender, MouseEventArgs e)
        {
            if (dataGridView8.CurrentRow != null)
            {
                int index = dataGridView8.CurrentRow.Index;
                DataGridViewCellCollection cl = dataGridView8.Rows[index].Cells;
               
                logisticsTypeId = Convert.ToInt16(cl[0].Value);
                textBox6.Text = cl[1].Value.ToString();
            }
            else
            {
                logisticsTypeId = -1;
            }
        }
        #endregion

        #region 初始化
        private void init()
        {

            dataGridView1.DataGridViewInit();
            dataGridView2.DataGridViewInit();
            dataGridView3.DataGridViewInit();
            dataGridView4.DataGridViewInit();
            dataGridView8.DataGridViewInit();
            dataGridView8.MouseClick += new MouseEventHandler(dataGridView8_CellClick);

            sx_dataGridview1fileType();
            sx_dataGridview2userType();
            sx_dataGridview4userPower();
            sx_dataGridview8LogisticsType();
        }
        #endregion


        #region 人员新增与查找
        /// <summary>
        /// save user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            employee saveEy = new employee();
        }

        private void loadEmployee(employee ey)
        {
            label9.Text = string.Format("Employee NO:{0}", ey.Userid);
            textBox7.Text = ey.Username;
            textBox8.Text = ey.Englishname;

            comboBox5.SelectedValue = ey.Cardtype;
            textBox10.Text = ey.Idcardno;

            dateTimePicker1.Value = ey.Birthday;
            if(ey.Sex == 1)
            {
                checkBox2.Checked = true;
            }
            else if(ey.Sex == 2)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
                checkBox1.Checked = false;
            }

            comboBox3.SelectedValue = ey.Usertypeid;
            comboBox4.SelectedValue = ey.Userpowerid;

            dateTimePicker2.Value = ey.Joindate;
            checkBox3.Checked = ey.Onjob;
            dateTimePicker3.Value = ey.Outdate;

            textBox9.Text = ey.Email;
            textBox11.SetWatermark("只有新增人员时,此处可设置初始密码");

        }

        #endregion
    }
}
