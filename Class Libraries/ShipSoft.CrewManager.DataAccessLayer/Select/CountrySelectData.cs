using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CountrySelectData :DataAccessBase
    {
       public CountrySelectData()
       {
           StoredProcedureName = StoredProcedure.Name.SelectCountryOfBirth.ToString();
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
