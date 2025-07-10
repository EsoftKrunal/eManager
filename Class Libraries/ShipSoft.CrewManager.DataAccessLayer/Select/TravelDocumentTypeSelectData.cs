using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class TravelDocumentTypeSelectData:DataAccessBase
    {
       public TravelDocumentTypeSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.selecttraveldocumenttype.ToString();
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
