using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyManager
{
    public partial class PasswordUpload : Form
    {
        string oldpassword;
        string newpassword1; 
        string newpassword2;


        protected override void OnClosing(CancelEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        public PasswordUpload()
        {
            InitializeComponent();
            textBox2.Enabled = true;
            textBox3.Enabled = false;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            oldpassword = textBox1.Text.Trim();
            newpassword1 = textBox2.Text.Trim();
            newpassword2 = textBox3.Text.Trim();
            if (newpassword1.Equals(newpassword2) && oldpassword.Md5().Equals(xuxSeecion.LOGINUSER.Pwd))
            {
                xuxSeecion.LOGINUSER.Pwd = newpassword1.Md5();
                if (xuxSeecion.LOGINUSER.SavePwd() > 0)
                {
                    MessageBox.Show("密码修改成功");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    button1.Enabled = false;
                }
                else
                {

                    MessageBox.Show("密码修改失败.");
                }
            }
            else
            {
                MessageBox.Show("未知错误 #1");
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            newpassword2 = textBox3.Text.Trim();

            if(!newpassword1.Equals(newpassword2))
            {
                MessageBox.Show("两次新密码不一样");
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            oldpassword = textBox1.Text.Trim();

            if(oldpassword.Md5().Equals(xuxSeecion.LOGINUSER.Pwd))
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
            }
            else
            {
                MessageBox.Show("旧密码不正确");
                textBox2.Enabled = true;
                textBox3.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            newpassword1 = textBox2.Text.Trim();
            if (!oldpassword.Md5().Equals(xuxSeecion.LOGINUSER.Pwd))
            {
                //MessageBox.Show("旧密码不正确");
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button1.Enabled = false;
                return;
            }

            if (oldpassword.Equals(newpassword1))
            {
                MessageBox.Show("新旧密码完全一样");
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button1.Enabled = false;
                return;
            }

            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
