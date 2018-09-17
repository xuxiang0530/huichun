using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.model
{
    class plate
    {
        #region properity
        private int _plateId;
        private int _warehouseid;
        private int _x;
        private int _y;
        private List<cell> _plateCell;

        public int PlateId
        {
            get
            {
                return _plateId;
            }

            set
            {
                _plateId = value;
            }
        }

        public int Warehouseid
        {
            get
            {
                return _warehouseid;
            }

            set
            {
                _warehouseid = value;
            }
        }

        public int X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }
        }

        internal List<cell> PlateCell
        {
            get
            {
                return _plateCell;
            }

            set
            {
                _plateCell = value;
            }
        }
        #endregion
    }
}
