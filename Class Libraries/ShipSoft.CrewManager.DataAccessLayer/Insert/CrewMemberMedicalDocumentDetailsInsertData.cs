using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
    public class CrewMemberMedicalDocumentDetailsInsertData : DataAccessBase
    {
        private MedicalDetails _medicaldetails;

        private CrewMemberMedicalInsertDataParameters _crewmembermedicalinsertdataparameters;
        public CrewMemberMedicalDocumentDetailsInsertData()
        {
            StoredProcedureName = StoredProcedure.Name.insertupdatecrewmembermedicaldocumentdetails.ToString();
        }
        public void Add()
        {
            _crewmembermedicalinsertdataparameters = new CrewMemberMedicalInsertDataParameters(MedicalDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            object id = dbhelper.RunScalar(base.ConnectionString, _crewmembermedicalinsertdataparameters.Parameters);

        }
        public MedicalDetails MedicalDetails
        {
            get { return _medicaldetails; }
            set { _medicaldetails = value; }
        }
        public class CrewMemberMedicalInsertDataParameters
        {
            private MedicalDetails _medicaldetails;
            private SqlParameter[] _parameters;
            public CrewMemberMedicalInsertDataParameters(MedicalDetails medicaldetails)
            {
                MedicalDetails = medicaldetails;
                Build();
            }

            private void Build()
            {
                SqlParameter[] parameters =
            {
                new SqlParameter("@MedicalDetailsId",MedicalDetails.MedicalDetailsId),
                new SqlParameter("@CrewId",MedicalDetails.CrewId),
                new SqlParameter("@DocumentTypeId",MedicalDetails.DocumentTypeId),
                //new SqlParameter("@DocumentSubtypeId",DocumentDetails.DocumentSubTypeId),
                new SqlParameter("@DocumentName",MedicalDetails.DocumentName),
                new SqlParameter("@BloodGroup",MedicalDetails.BloodGroup),
                new SqlParameter("@DocumentNumber",MedicalDetails.DocumentNumber),
                //new SqlParameter("@DocumentName",DocumentDetails.DocumentName),
                new SqlParameter("@IssueDate",MedicalDetails.IssueDate),
                new SqlParameter("@ExpiryDate",MedicalDetails.ExpiryDate),
                //new SqlParameter("@NeverExpires",MedicalDetails.NeverExpires),
                new SqlParameter("@PlaceOfIssue",MedicalDetails.PlaceOfIssue),
                new SqlParameter("@ImagePath",MedicalDetails.ImagePath),
                //new SqlParameter("@Remarks",DocumentDetails.Remarks),
                new SqlParameter("@CreatedBy",MedicalDetails.CreatedBy),
                new SqlParameter("@ModifiedBy",MedicalDetails.ModifiedBy),
                //new SqlParameter("@ModifiedOn",DocumentDetails.ModifiedOn),
            };
                Parameters = parameters;
            }
            public MedicalDetails MedicalDetails
            {
                get { return _medicaldetails; }
                set { _medicaldetails = value; }
            }

            public SqlParameter[] Parameters
            {
                get { return _parameters; }
                set { _parameters = value; }
            }
        }
    }
}
