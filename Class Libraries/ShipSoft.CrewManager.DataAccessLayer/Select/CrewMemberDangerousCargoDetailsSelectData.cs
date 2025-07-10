using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CrewMemberDangerousCargoDetailsSelectData : DataAccessBase
    {
       private CrewCargoDetails _crewCargoDetails;

       public CrewMemberDangerousCargoDetailsSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.selectCrewDangerousCargoDetails.ToString();
        }
       public DataSet Get()
        {
            DataSet ds;
            CrewMemberDangerousCargoDetailsSelectDataParameters _DangerousCargoDetailsByIdParameters = new CrewMemberDangerousCargoDetailsSelectDataParameters(CrewCargoDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _DangerousCargoDetailsByIdParameters.Parameters);
            return ds;
        }
       public CrewCargoDetails CrewCargoDetails
       {
           get { return _crewCargoDetails; }
           set { _crewCargoDetails = value; }
       }
    }
    public class CrewMemberDangerousCargoDetailsSelectDataParameters
    {
        private CrewCargoDetails _crewCargoDetails;
        private SqlParameter[] _parameters;

        public CrewMemberDangerousCargoDetailsSelectDataParameters(CrewCargoDetails param)
        {
            CrewCargoDetails = param;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@DangerousCargoId" , CrewCargoDetails.DangerousCargoId),
                new SqlParameter( "@CrewId" , CrewCargoDetails.CrewId),
            };

            Parameters = parameters;
        }

        public CrewCargoDetails CrewCargoDetails
        {
            get { return _crewCargoDetails; }
            set { _crewCargoDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
