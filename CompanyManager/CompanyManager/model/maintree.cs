using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyManager.model
{
    class maintree 
    {
        TreeNode mainTreeNode;

        public maintree()
        {
            string rootName = "菜单";

            MainTreeNode = new TreeNode(rootName);


        }

        public TreeNode MainTreeNode
        {
            get
            {
                return mainTreeNode;
            }

            set
            {
                mainTreeNode = value;
            }
        }

        public  void treeView1_MouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MessageBox.Show("aa");
        }

    }
}
