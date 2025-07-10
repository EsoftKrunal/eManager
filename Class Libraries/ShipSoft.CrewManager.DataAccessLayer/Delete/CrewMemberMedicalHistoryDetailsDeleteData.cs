using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager;
using System.Data.SqlClient ; 
namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
   public class CrewMemberMedicalHistoryDetailsDeleteData:DataAccessBase 
    {
        
        private  CrewMemberMedicalHistory  _crewMemberMedicalHistory;
        private CrewMemberMedicalHistoryDetailsDeleteDataParameters _crewMemberMedicalHistoryDetailsDeleteDataParameters;

        public CrewMemberMedicalHistoryDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCrewMemberMedicalDetailsById.ToString();
        }
        public void Delete()
        {
            _crewMemberMedicalHistoryDetailsDeleteDataParameters = new CrewMemberMedicalHistoryDetailsDeleteDataParameters(MedicalDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewMemberMedicalHistoryDetailsDeleteDataParameters.Parameters;
            dbhelper.Run();
        }
       public CrewMemberMedicalHistory MedicalDetails
        {
            get { return _crewMemberMedicalHistory; }
            set { _crewMemberMedicalHistory = value; }
        }
    }
     public class CrewMemberMedicalHistoryDetailsDeleteDataParameters
    {
        private CrewMemberMedicalHistory _CrewMemberMedicalHistory;
        private SqlParameter[] _parameters;

         public CrewMemberMedicalHistoryDetailsDeleteDataParameters(CrewMemberMedicalHistory exp)
        {
            MedicalDetails = exp;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
		        new SqlParameter( "@CrewMedicalId"  , MedicalDetails.MedicalCaseId)
		    };

            Parameters = parameters;
        }

         public CrewMemberMedicalHistory MedicalDetails
         {
             get { return _CrewMemberMedicalHistory; }
             set { _CrewMemberMedicalHistory = value; }
         }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }

}
