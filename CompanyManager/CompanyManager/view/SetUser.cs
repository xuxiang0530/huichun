using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyManager.view
{
    public partial class SetUser : Form
    {
        CompanyManager.model.celluser user;
        public SetUser(int userId)
        {
            InitializeComponent();
            Init();
            if (userId == -1)
            {
                user = new model.celluser();
            }
            else
            {
                user = new model.celluser(userId.ToString());
                loadUser();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void loadUser()
        {
            if (user.UserId != -1)
            {
                textBox1.Text = user.Name;
                comboBox1.SelectedValue = user.CardType;
                textBox2.Text = user.IdCardNo;
                textBox3.Text = user.Phone;
                comboBox2.SelectedValue = user.Sex;
            }
        }
        private void setUser()
        {
            user.Name = textBox1.Text.Trim();
            user.CardType = Convert.ToInt16(comboBox1.SelectedValue);
            user.IdCardNo = textBox2.Text.Trim();
            user.Phone = textBox3.Text.Trim();
            user.Sex = Convert.ToInt16(comboBox2.SelectedValue);
        }

        private void Init()
        {
            comboBox1.ComboBoxInit(xuxSeecion.CardTypeList, "id", "type");
            comboBox2.ComboBoxInit(xuxSeecion.SexList, "id", "type");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            setUser();
            if(user.Save())
            {
                MessageBox.Show("Save sessfully");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(xuxSeecion.ERRMESSAGE);
            }
        }
    }
}
