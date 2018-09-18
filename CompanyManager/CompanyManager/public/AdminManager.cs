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

namespace CompanyManager
{
    public partial class AdminManager : Form
    {
        DataTable userType;
        DataTable userPower;
        DataTable fileType;

        public AdminManager()
        {
            InitializeComponent();
            dataGridView1.DataGridViewInit();
            dataGridView2.DataGridViewInit();
            dataGridView3.DataGridViewInit();
            dataGridView4.DataGridViewInit();

            sx_dataGridview1fileType();
            sx_dataGridview2userType();
            sx_dataGridview4userPower();

            
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
        private void sx_dataGridview1fileType()
        {
            string sqlstr = "SELECT a.filetypeid id,a.typename,note  FROM TD_filetype a";
            DataTable dt = MySqlConn.GetDataSet(string.Format(sqlstr)).Tables[0];
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[dt.Columns.IndexOf("typename")].HeaderCell.Value = "文件类型";
            dataGridView1.Columns[dt.Columns.IndexOf("note")].HeaderCell.Value = "备注";
        }

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
        /// 文件类型保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string fileTypeName = textBox1.Text.Trim();
            string note  = textBox5.Text.Trim();

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
                string strSql = string.Format("INSERT INTO TD_filetype(typename,note) VALUES(N'{0}',N'{1}')", fileTypeName,note);

                MySqlConn.DoCommand(strSql);
                textBox1.Text = "";
                textBox5.Text = "";
                sx_dataGridview1fileType();
                MessageBox.Show("Successfully");
            }

        }

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

        private void button3_Click(object sender, EventArgs e)
        {
            string powerName = textBox3.Text.Trim();
            string note = textBox4.Text.Trim();

            string strSql = "INSERT INTO TD_power(powername,note) VALUES(N'{0}',N'{1}')";

            foreach(DataGridViewRow dr in dataGridView4.Rows)
            {
                if(dr.Cells[1].Value.ToString().Equals(powerName))
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
    }
}
