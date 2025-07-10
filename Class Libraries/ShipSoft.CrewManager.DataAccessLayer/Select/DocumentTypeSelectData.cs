using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class DocumentTypeSelectData:DataAccessBase
    {
       public DocumentTypeSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.selectdocumenttype.ToString();
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
