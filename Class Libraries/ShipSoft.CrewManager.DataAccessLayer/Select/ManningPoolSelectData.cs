using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class ManningPoolSelectData : DataAccessBase
    {
        public ManningPoolSelectData()
       {
           StoredProcedureName = StoredProcedure.Name.SelectManningPool.ToString();
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
