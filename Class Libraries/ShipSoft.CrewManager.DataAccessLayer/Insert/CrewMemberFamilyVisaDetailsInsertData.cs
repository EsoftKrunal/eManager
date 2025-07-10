using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
   public class CrewMemberFamilyVisaDetailsInsertData :DataAccessBase

    {
        private CrewFamilyDocumentDetails _crewFamilyDocumentDetails;
        private CrewMemberFamilyVisaDetailsInsertDataParameters _crewrecordvisaInsertDataparameters;

        public CrewMemberFamilyVisaDetailsInsertData()
        {
            StoredProcedureName = StoredProcedure.Name.InsertUpdateCrewMemberFamilyVisaDetails.ToString();
        }
        public void Add()
        {
            _crewrecordvisaInsertDataparameters = new CrewMemberFamilyVisaDetailsInsertDataParameters(CrewFamilyDocumentDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewrecordvisaInsertDataparameters.Parameters;
            dbhelper.Run();
        }
        public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
        {
            get { return _crewFamilyDocumentDetails; }
            set { _crewFamilyDocumentDetails = value; }
        }
    }
    public class CrewMemberFamilyVisaDetailsInsertDataParameters
    {
        private CrewFamilyDocumentDetails _crewFamilyDocumentDetails;
        private SqlParameter[] _parameters;

        public CrewMemberFamilyVisaDetailsInsertDataParameters(CrewFamilyDocumentDetails crewFamilydocumentDetails)
        {
            CrewFamilyDocumentDetails = crewFamilydocumentDetails;
            Build();
        }
        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewFamilyDocumentId"		, _crewFamilyDocumentDetails.Crewfamilydocumentid) ,
                new SqlParameter( "@CrewFamilyId"		, _crewFamilyDocumentDetails.CrewFamilyId ) ,
                new SqlParameter( "@DocumentName"		, _crewFamilyDocumentDetails.DocumentName) ,
                new SqlParameter( "@DocumentType"		, _crewFamilyDocumentDetails.DocumentType ) ,
                new SqlParameter( "@DocumentNumber"		, _crewFamilyDocumentDetails.DocumentNumber) ,
                new SqlParameter( "@IssueDate"		, _crewFamilyDocumentDetails.Issuedate) ,
                new SqlParameter( "@ExpiryDate"		,_crewFamilyDocumentDetails.Expirydate ) ,
                new SqlParameter( "@PlaceOfIssue"		,_crewFamilyDocumentDetails.Placeofissue) ,
                new SqlParameter( "@DocumentFlag"		,_crewFamilyDocumentDetails.DocumentFlag ) ,
            };

            Parameters = parameters;
        }
        public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
        {
            get { return _crewFamilyDocumentDetails; }
            set { _crewFamilyDocumentDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

    }
}
