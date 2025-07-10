using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class NearestAirportSelectDataById:DataAccessBase
    {
       private int _CountryId;
       public NearestAirportSelectDataById()
       {
           StoredProcedureName = StoredProcedure.Name.selectNearestAirport.ToString();
       }
       public DataSet Get()
       {
           DataSet ds;
           NearestAirportSelectDataByIdParameters documentdetailsselectdataparameters = new NearestAirportSelectDataByIdParameters(CountryId);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           ds = dbhelper.Run(base.ConnectionString, documentdetailsselectdataparameters.Parameters);
           return ds;
       }
       public int CountryId
       {
           get { return _CountryId; }
           set { _CountryId = value; }
       }
       public class NearestAirportSelectDataByIdParameters
       {
           private int _CountryId;
           private SqlParameter[] _parameters;
           public NearestAirportSelectDataByIdParameters(int param)
           {
               CountryId = param;
               Build();
           }
           private void Build()
           {
               SqlParameter[] parameters =
            {
                new SqlParameter( "@CountryId",CountryId)
            };

               Parameters = parameters;
           }
           public int CountryId
           {
               get { return _CountryId; }
               set { _CountryId = value; }
           }
           public SqlParameter[] Parameters
           {
               get { return _parameters; }
               set { _parameters = value; }
           }
       }
    }
}
