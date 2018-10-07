using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyManager.model;
using CompanyManager.DataManager;
namespace CompanyManager
{
    public partial class EmployeeManager : Form
    {
        employee ey;
        public EmployeeManager()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            dataGridView9.DataGridViewInit();
            dataGridView9.MouseDoubleClick += new MouseEventHandler(dataGridView9_DoubleCellClick);
            ey = new employee();
            sxEmployeeDataGridView();

            string strSql = "select id,typename from TD_type";
            DataTable dt = MySqlConn.GetDataSet(strSql).Tables[0];
            comboBox3.ComboBoxInit(dt, "id", "typename");

            strSql = "select id,powername from TD_power";
            dt = MySqlConn.GetDataSet(strSql).Tables[0];
            comboBox4.ComboBoxInit(dt, "id", "powername");


            comboBox5.ComboBoxInit(xuxSeecion.CardTypeList, "id", "type");

            tabControl1.TabPages.Remove(tabPage5);
        }

        private void sxEmployeeDataGridView()
        {
            dataGridView9.DataSource = null;
            dataGridView9.DataSource = ey.SearchEmail(textBox12.Text.Trim());

        }


        private void button10_Click(object sender, EventArgs e)
        {
            setEmployee();
            if(ey.Save())
            {
                ey.sxEmployeeDataTable();
                sxEmployeeDataGridView();
                tabControl1.SelectTab(tabPage6);
                MessageBox.Show("保存成功!");
            }
            else
            {
                MessageBox.Show(xuxSeecion.ERRMESSAGE);
            }
        }


        private void dataGridView9_DoubleCellClick(object sender, MouseEventArgs e)
        {
            if (dataGridView9.CurrentRow != null)
            {
                int index = dataGridView9.CurrentRow.Index;

                //DataGridViewCellCollection cl = dataGridView9.Rows[index].Cells;
                ey.GetByUserId(dataGridView9.Rows[index].Cells[0].Value.ToString());
                loadEmployee();
                if (tabControl1.TabPages.IndexOf(tabPage5) == -1)
                {
                    tabControl1.TabPages.Add(tabPage5);
                }
                tabControl1.SelectTab(tabPage5);
            }
        }


        #region 员工相关

        private void setEmployee()
        {
            ey.Username = textBox7.Text.Trim();
            ey.Englishname = textBox8.Text.Trim();

            ey.Cardtype = Convert.ToInt16(comboBox5.SelectedValue);
            ey.Idcardno = textBox10.Text.Trim();

            ey.Birthday = dateTimePicker1.Value;

            ey.Sex = radioButton1.Checked ? 1 : 2;

            ey.Usertypeid = Convert.ToInt16(comboBox3.SelectedValue);
            ey.Userpowerid = Convert.ToInt16(comboBox4.SelectedValue);

            ey.Joindate = dateTimePicker2.Value;
            ey.Onjob = checkBox3.Checked;
            ey.Outdate = dateTimePicker3.Value;

            ey.Email = textBox9.Text.Trim();
            ey.Pwd = textBox11.Text.Trim().Md5();
        }

        private void loadEmployee()
        {
            label9.Text = string.Format("Employee NO:{0}", ey.Userid == -1 ? "新员工" : ey.Userid.ToString());
            textBox7.Text = ey.Username;
            textBox8.Text = ey.Englishname;

            comboBox5.SelectedValue = ey.Cardtype;
            textBox10.Text = ey.Idcardno;

            dateTimePicker1.Value = ey.Birthday;
            if (ey.Sex == 1)
            {
                radioButton1.Checked = true;
            }
            else if (ey.Sex == 2)
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
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

        private void button9_Click(object sender, EventArgs e)
        {
            sxEmployeeDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ey = new employee();
            loadEmployee();
            tabControl1.TabPages.Add(tabPage5);
            tabControl1.SelectTab(tabPage5);
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            button9_Click(null, null);
        }
    }
}
