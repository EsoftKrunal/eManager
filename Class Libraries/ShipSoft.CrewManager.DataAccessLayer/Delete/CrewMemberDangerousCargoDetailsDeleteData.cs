using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager;

namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
   public class CrewMemberDangerousCargoDetailsDeleteData : DataAccessBase
    {
       private CrewCargoDetails _crewCargoDetails;
       private CrewMemberDangerousCargoDetailsDeleteDataParameters _crewMemberDangerousCargoDetailsDeleteDataParameters;

       public CrewMemberDangerousCargoDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCrewMemberDangerousCargoDetailsById.ToString();
        }
        public void Delete()
        {
            _crewMemberDangerousCargoDetailsDeleteDataParameters = new CrewMemberDangerousCargoDetailsDeleteDataParameters(CrewCargoDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewMemberDangerousCargoDetailsDeleteDataParameters.Parameters;
            dbhelper.Run();
        }
       public CrewCargoDetails CrewCargoDetails
        {
            get { return _crewCargoDetails; }
            set { _crewCargoDetails = value; }
        }
    }
    public class CrewMemberDangerousCargoDetailsDeleteDataParameters
    {
        private CrewCargoDetails _crewCargoDetails;
        private SqlParameter[] _parameters;

        public CrewMemberDangerousCargoDetailsDeleteDataParameters(CrewCargoDetails param)
        {
            CrewCargoDetails = param;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@DangerousCargoId" , CrewCargoDetails.DangerousCargoId),
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
