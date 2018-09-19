using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManager.DataManager;
using System.Windows.Forms;

namespace CompanyManager
{
    abstract class treenote : MySqlConn
    {
        public treenote(string xianshi)
        {
            TreeNode tn = new TreeNode(xianshi);
            //tn.m
        }
        public abstract void treeView1_MouseClick(object sender, TreeNodeMouseClickEventArgs e);
    }
}
