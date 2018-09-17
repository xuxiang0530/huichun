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
        DataTable dtUserList = null;
        public AdminManager()
        {
            InitializeComponent();
            dataGridView1.DataGridViewInit();
            dataGridView2.DataGridViewInit();
            dataGridView3.DataGridViewInit();
            sx_dataGridview1();
            sx_dataGridview2();
            DataTable dt = MySqlConn.GetDataSet("SELECT id,TypeName FROM [DB_DataInformation].[dbo].[TD_UserType]").Tables[0];
            comboBox2.ComboBoxInit(dt, "id", "TypeName");

            dtUserList = MySqlConn.GetDataSet(string.Format("SELECT USERID,USERNAME,EMAIL  FROM DB_UserManager.dbo.T_USER WHERE ISZZ = 1")).Tables[0];

            if (dtUserList.Rows.Count > 0)
            {
                string[] cn = new string[dtUserList.Rows.Count];
                for (int i = 0; i < cn.Length; i++)
                {
                    cn[i] = dtUserList.Rows[i].ItemArray[2].ToString();
                }
                comboBox1.AutoCompleteCustomSource.AddRange(cn);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void sx_dataGridview1()
        {
            string sqlstr = "SELECT a.id,a.TypeName [文件类型] FROM DB_DataInformation.dbo.TD_FileType a";
            DataTable dt = MySqlConn.GetDataSet(string.Format(sqlstr)).Tables[0];
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
        }

        private void sx_dataGridview2()
        {
            string sqlstr = "SELECT a.ID id,a.[type] [保密等级] FROM DB_DataInformation.dbo.T_FileSecrecyGrade a";
            DataTable dt = MySqlConn.GetDataSet(string.Format(sqlstr)).Tables[0];
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = dt;
        }

        private void sx_dataGridview3()
        {
            string sqlstr = "";
            DataTable dt = MySqlConn.GetDataSet(string.Format(sqlstr)).Tables[0];
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = dt;
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

            if (fileTypeName.Equals(""))
            {
                MessageBox.Show("value is null");
                return;
            }
            else
            {
                string strSql = string.Format("INSERT INTO DB_DataInformation.dbo.TD_FileType VALUES(N'{0}')", fileTypeName);

                MySqlConn.DoCommand(strSql);
                textBox1.Text = "";
                sx_dataGridview1();
                MessageBox.Show("Successfully");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string SecrecyGrade = textBox2.Text.Trim();

            if (SecrecyGrade.Equals(""))
            {
                MessageBox.Show("value is null");
                return;
            }
            else
            {
                string strSql = string.Format("INSERT INTO DB_DataInformation.dbo.T_FileSecrecyGrade VALUES(N'{0}')", SecrecyGrade);

                MySqlConn.DoCommand(strSql);
                textBox2.Text = "";
                sx_dataGridview2();
                MessageBox.Show("Successfully");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string userId = "";
            string strCombobox = comboBox1.Text.Trim();
            string strSql = "INSERT INTO DB_DataInformation.dbo.T_UserInfo(UserId,UserTypeId) VALUES('{0}',{1})";
            foreach (DataRow dr in dtUserList.Rows)
            {
                if(dr.ItemArray[3].ToString().Equals(strCombobox))
                {
                    userId = dr.ItemArray[0].ToString();
                }
            }

            if(userId.Equals(""))
            {
                MessageBox.Show("你输入的邮箱不对,核实哦");
                return;
            }
            else
            {
                if(
                MySqlConn.DoCommand(string.Format(strSql,userId,comboBox2.SelectedValue.ToString()))
                    > 0)
                {
                    MessageBox.Show("Seccessfully");
                }
                else
                {
                    MessageBox.Show("sql error");
                }
            }
        }
    }
}
