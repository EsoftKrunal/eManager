using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ShipSoft.CrewManager.DataAccessLayer
{
    public class DataAccessBase
    {
        private string _storedprocedureName;

        protected string StoredProcedureName
        {
            get { return _storedprocedureName; }
            set { _storedprocedureName = value; }
        }

        public string ConnectionString
        {
            get { return AppConfiguration.ConnectionString.ToString(); }
        }
    }
}
