using System.Windows.Forms;

namespace CompanyManager
{
    public static class InitTool
    {
        public static DataGridView DataGridViewInit(this System.Windows.Forms.DataGridView dv)
        {
            dv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dv.AllowUserToAddRows = false;
            dv.AllowUserToDeleteRows = false;
            dv.RowHeadersVisible = false;
            dv.AllowUserToResizeColumns = true;
            dv.AllowUserToResizeRows = false;
            dv.ReadOnly = true;
            dv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            return dv;
        }

        public static Button Button(this System.Windows.Forms.Button btn)
        {
            btn.Width = 80;
            btn.Height = 30;

            return btn;
        }
        public static ComboBox ComboBoxInit(this ComboBox cb, System.Data.DataTable dt, string value, string display)
        {
            cb.DataSource = dt;
            cb.ValueMember = value;
            cb.DisplayMember = display;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            return cb;
        }
    }
}
