using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CourseNameSelectData:DataAccessBase
    {
        public CourseNameSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.selectcoursename.ToString();
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
