using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
   public class CrewMemberMedicalDetailsDeleteData:DataAccessBase
    {
       private MedicalDetails _MedicalDetails;
        private CrewMemberMedicalDetailsDeleteDataParameters _crewmembertmedicaldeletedataparameters;

        public CrewMemberMedicalDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCrewMemberMedicalDocumentDetailsById.ToString();
        }
        public void Delete()
        {
            _crewmembertmedicaldeletedataparameters = new CrewMemberMedicalDetailsDeleteDataParameters(MedicalDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewmembertmedicaldeletedataparameters.Parameters;
            dbhelper.Run();
        }
        public MedicalDetails MedicalDetails
        {
            get { return _MedicalDetails; }
            set { _MedicalDetails = value; }
        }
    }
    public class CrewMemberMedicalDetailsDeleteDataParameters
    {
        private MedicalDetails _MedicalDetails;
        private SqlParameter[] _parameters;

        public CrewMemberMedicalDetailsDeleteDataParameters(MedicalDetails doc)
        {
            MedicalDetails = doc;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
                new SqlParameter( "@MedicalDetailsId"  , MedicalDetails.MedicalDetailsId)
		    };

            Parameters = parameters;
        }

        public MedicalDetails MedicalDetails
        {
            get { return _MedicalDetails; }
            set { _MedicalDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
