using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CrewMemberMedicalcaseHistorySelectData:DataAccessBase 
    {
        private CrewMemberMedicalHistory _crewmembermedicalhistory;
        public CrewMemberMedicalcaseHistorySelectData()
       {
           StoredProcedureName = StoredProcedure.Name.SelectCrewMemberMedicalCaseHistory.ToString(); ;
       }
      
      public DataSet GetMedicalCaseHistoryDetail()
        {
            DataSet ds;

            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(ConnectionString);

            return ds;
        }
        public DataSet Get()
        {
            DataSet ds;
            CrewMemberMedicalcaseHistorySelectDataParameters medicalDetailsSelectDataParameters = new CrewMemberMedicalcaseHistorySelectDataParameters(Medicaldetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, medicalDetailsSelectDataParameters.Parameters);
            return ds;
        }
        public CrewMemberMedicalHistory Medicaldetails
        {
            get { return _crewmembermedicalhistory; }
            set { _crewmembermedicalhistory = value; }
        }
    }

    public class CrewMemberMedicalcaseHistorySelectDataParameters
    {
        private CrewMemberMedicalHistory _crewmembermedicalhistory;
        private SqlParameter[] _parameters;

        public CrewMemberMedicalcaseHistorySelectDataParameters(CrewMemberMedicalHistory medicaldetails)
        {
            Medicaldetails = medicaldetails;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" , Medicaldetails.CrewId )
            };

            Parameters = parameters;
        }

        public CrewMemberMedicalHistory Medicaldetails
        {
            get { return _crewmembermedicalhistory; }
            set { _crewmembermedicalhistory = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
