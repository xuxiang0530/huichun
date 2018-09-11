using System.Data;
using System.Collections;
using CompanyManager.model;

namespace CompanyManager.xuxstatic
{
    class xuxSeecion
    {
        private static DataTable _cardTypeLIst;
        private static DataTable _sexList;
        public static employee LOGINUSER;

        public static DataTable CardTypeLIst
        {
            get
            {
                return _cardTypeLIst;
            }

            set
            {
                _cardTypeLIst = value;
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

        public xuxSeecion()
        {
            Hashtable CardTypeHash = new Hashtable();

            CardTypeHash.Add(1, "身份证");
            CardTypeHash.Add(2, "护照");

            Hashtable SexHash = new Hashtable();

            SexHash.Add(1, "男");
            SexHash.Add(2, "女");

            CardTypeLIst = hashToDataTable(CardTypeHash);
            SexList = hashToDataTable(SexHash);
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
    }
}
