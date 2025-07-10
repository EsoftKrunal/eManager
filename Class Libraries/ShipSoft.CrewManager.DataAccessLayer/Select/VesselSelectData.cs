using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class VesselSelectData : DataAccessBase
    {
       public VesselSelectData()
       {
           StoredProcedureName = StoredProcedure.Name.SelectVessel.ToString();
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
