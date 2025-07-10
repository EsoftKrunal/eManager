using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class SignOffReasonSelectData : DataAccessBase
    {
        public SignOffReasonSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.SelectSignOffReason.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;

            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(ConnectionString);
            return ds;
        }
    }
}
