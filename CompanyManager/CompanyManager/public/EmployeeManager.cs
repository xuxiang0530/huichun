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

namespace CompanyManager
{
    public partial class EmployeeManager : Form
    {
        public EmployeeManager()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }


        #region 员工相关

        private void setEmployee(employee ey)
        {
            ey.Username = textBox7.Text.Trim();
            ey.Englishname = textBox8.Text.Trim();

            ey.Cardtype = Convert.ToInt16(comboBox5.SelectedValue);
            ey.Idcardno = textBox10.Text.Trim();

            ey.Birthday = dateTimePicker1.Value;

            ey.Sex = checkBox2.Checked ? 1 : 2;

            ey.Usertypeid = Convert.ToInt16(comboBox3.SelectedValue);
            ey.Userpowerid = Convert.ToInt16(comboBox4.SelectedValue);

            ey.Joindate = dateTimePicker2.Value;
            ey.Onjob = checkBox3.Checked;
            ey.Outdate = dateTimePicker3.Value;

            ey.Email = textBox9.Text.Trim();
            ey.Pwd = textBox11.Text.Trim().Md5();
        }

        private void loadEmployee(employee ey)
        {
            label9.Text = string.Format("Employee NO:{0}", ey.Userid);
            textBox7.Text = ey.Username;
            textBox8.Text = ey.Englishname;

            comboBox5.SelectedValue = ey.Cardtype;
            textBox10.Text = ey.Idcardno;

            dateTimePicker1.Value = ey.Birthday;
            if (ey.Sex == 1)
            {
                checkBox2.Checked = true;
            }
            else if (ey.Sex == 2)
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
