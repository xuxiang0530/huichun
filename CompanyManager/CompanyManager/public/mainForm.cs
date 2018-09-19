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
using CompanyManager.xuxstatic;

namespace CompanyManager
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();

            init();
        }

        private void init()
        {

            maintree mt = new maintree();
            treeView1.Nodes.Add(mt.MainTreeNode);

            treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(mt.treeView1_MouseClick);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void 系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void UploadPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordUpload pufm = new PasswordUpload();
            pufm.ShowDialog();
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminManager am = new AdminManager();
            am.ShowDialog();
        }
    }
}
