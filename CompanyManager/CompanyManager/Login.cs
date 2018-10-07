using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CompanyManager.DataManager;
using CompanyManager.model;


namespace CompanyManager
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            //UpdateLibrary.update.startJc();

            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);

            textBox1.Text = DataAcc.readTxtLastLogin().Trim();

            string DomainUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            if (textBox1.Text.Equals(""))
            {
                textBox1.Text = DomainUserName;
                //textBox1.Text = "@.com";
            }
            textBox2.SetWatermark("Email password");

            if (DomainUserName.Equals(@"DESKTOP-UF4UQ1S\tianyaxx"))
            {
                textBox1.Text = "tianyaxx@163.com";
                textBox2.Text = "test";
            }

            if (DomainUserName.Equals(@"HDBIOSCIENCES\xuxiang"))
            {
                textBox1.Text = "tianyaxx@163.com";
                textBox2.Text = "test";
                MySqlConn.ConnetStr = "192.168.1.102";

            }
        }


        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Email = "";
            string Pwd = "";

            Email = textBox1.Text.Trim();
            Pwd = textBox2.Text.Trim();
            
            if (Email.Length == 0 || Pwd.Length == 0)
            {
                MessageBox.Show("用户名或者密码不能为空。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            employee ey = new employee(Email, Pwd.Md5());
            if(ey.Userid != -1)
            {
                xuxSeecion.LOGINUSER = ey;
                xuxSeecion xux = new xuxSeecion();
                xuxSeecion.sxWareHouse();
                mainForm mf = new mainForm();
                this.Hide();
                if (mf.ShowDialog() == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("用户邮箱或密码有错,请更正后再试");
                return;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
