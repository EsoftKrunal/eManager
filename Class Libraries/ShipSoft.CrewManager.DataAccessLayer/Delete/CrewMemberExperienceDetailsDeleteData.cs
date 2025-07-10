using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager;

namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
    public class CrewMemberExperienceDetailsDeleteData : DataAccessBase
    {
        private ExperienceDetails _ExperienceDetails;
        private CrewMemberExperienceDetailsDeleteDataParameters _crewmemberexperiencedeletedataparameters;

        public CrewMemberExperienceDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCrewMemberExperienceDetailsById.ToString();
        }
        public void Delete()
        {
            _crewmemberexperiencedeletedataparameters = new CrewMemberExperienceDetailsDeleteDataParameters(ExperienceDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewmemberexperiencedeletedataparameters.Parameters;
            dbhelper.Run();
        }
        public ExperienceDetails ExperienceDetails
        {
            get { return _ExperienceDetails; }
            set { _ExperienceDetails = value; }
        }
    }

    public class CrewMemberExperienceDetailsDeleteDataParameters
    {
        private ExperienceDetails _ExperienceDetails;
        private SqlParameter[] _parameters;

        public CrewMemberExperienceDetailsDeleteDataParameters(ExperienceDetails exp)
        {
            ExperienceDetails = exp;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
		        new SqlParameter( "@CrewExperienceId"  , ExperienceDetails.ExperienceId)
		    };

            Parameters = parameters;
        }

        public ExperienceDetails ExperienceDetails
        {
            get { return _ExperienceDetails; }
            set { _ExperienceDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
