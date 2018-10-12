using System.Data;
using System.Collections;
using CompanyManager.model;
using System.Drawing;

namespace CompanyManager
{
    class xuxSeecion
    {
        private static DataTable _cardTypeList;
        private static DataTable _sexList;
        public static employee LOGINUSER;
        public static string ERRMESSAGE;

        private static DataTable _wareHouseTable;
        private static System.Drawing.Size _mainPanel2Size;
        

        public static DataTable CardTypeList
        {
            get
            {
                return _cardTypeList;
            }

            set
            {
                _cardTypeList = value;
            }
        }

        public static DataTable SexList
        {
            get
            {
                return _sexList;
            }

            set
            {
                _sexList = value;
            }
        }

        public static DataTable WareHouseTable
        {
            get
            {
                return _wareHouseTable;
            }

            set
            {
                _wareHouseTable = value;
            }
        }

        public static Size MainPanel2Size
        {
            get
            {
                return _mainPanel2Size;
            }

            set
            {
                _mainPanel2Size = value;
            }
        }

        public xuxSeecion()
        {
            Hashtable CardTypeHash = new Hashtable();

            CardTypeHash.Add(1, "身份证");
            CardTypeHash.Add(2, "护照");
            CardTypeList = hashToDataTable(CardTypeHash);

            Hashtable SexHash = new Hashtable();
            
            SexHash.Add(1, "男");
            SexHash.Add(2, "女");

            CardTypeList = hashToDataTable(CardTypeHash);
            SexList = hashToDataTable(SexHash);
            sxWareHouse();
        }

        private DataTable hashToDataTable(Hashtable htable)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id",typeof(string));
            dt.Columns.Add("type", typeof(string));
            foreach (DictionaryEntry element in htable)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = element.Key.ToString();
                dr["type"] = element.Value.ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static void sxWareHouse()
        {
            WareHouseTable = CompanyManager.DataManager.MySqlConn.GetDataSet("select id,name,type,note,location from T_warehouse").Tables[0];
        }
    }
}
