using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager;

namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
    public class CrewMemberAcademicDetailsDeleteData : DataAccessBase
    {
        private AcademicDetails _AcademicDetails;
        private CrewMemberAcademicDetailsDeleteDataParameters _crewmemberacademicdeletedataparameters;

        public CrewMemberAcademicDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCrewMemberAcademicDetailsById.ToString();
        }
        public void Delete()
        {
            _crewmemberacademicdeletedataparameters = new CrewMemberAcademicDetailsDeleteDataParameters(AcademicDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewmemberacademicdeletedataparameters.Parameters;
            dbhelper.Run();
        }
        public AcademicDetails AcademicDetails
        {
            get { return _AcademicDetails; }
            set { _AcademicDetails = value; }
        }
    }

    public class CrewMemberAcademicDetailsDeleteDataParameters
    {
        private AcademicDetails _AcademicDetails;
        private SqlParameter[] _parameters;

        public CrewMemberAcademicDetailsDeleteDataParameters(AcademicDetails exp)
        {
            AcademicDetails = exp;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
		        new SqlParameter( "@AcademicDetailsId"  , AcademicDetails.AcademicDetailsId)
		    };

            Parameters = parameters;
        }

        public AcademicDetails AcademicDetails
        {
            get { return _AcademicDetails; }
            set { _AcademicDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
