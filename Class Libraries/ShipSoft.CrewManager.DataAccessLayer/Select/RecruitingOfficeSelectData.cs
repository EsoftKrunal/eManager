using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class RecruitingOfficeSelectData : DataAccessBase
    {
        public RecruitingOfficeSelectData()
       {
           StoredProcedureName = StoredProcedure.Name.SelectRecruitingOffice.ToString();
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
