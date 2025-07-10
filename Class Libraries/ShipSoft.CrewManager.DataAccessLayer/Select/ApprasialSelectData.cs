using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class  ApprasialSelectData:DataAccessBase 
    {
        public ApprasialSelectData()
       {
           StoredProcedureName = StoredProcedure.Name.SelectAppraisal.ToString();
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
