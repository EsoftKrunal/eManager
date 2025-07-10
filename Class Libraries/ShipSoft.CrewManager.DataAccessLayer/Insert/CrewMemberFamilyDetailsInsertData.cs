using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
    public class CrewMemberFamilyDetailsInsertData : DataAccessBase
    {
        private FamilyDetails _familydetails;
        private CrewMemberFamilyDetailsInsertDataParameters _crewrecordcontactInsertDataparameters;

        public CrewMemberFamilyDetailsInsertData()
        {
            StoredProcedureName = StoredProcedure.Name.InsertUpdateCrewMemberFamilyDetails.ToString();
        }
        public void Add()
        {

            _crewrecordcontactInsertDataparameters = new CrewMemberFamilyDetailsInsertDataParameters(FamilyDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            object id = dbhelper.RunScalar(base.ConnectionString, _crewrecordcontactInsertDataparameters.Parameters);
            this._familydetails.Crewfamilyid = Convert.ToInt32(id.ToString());
        }
        public FamilyDetails FamilyDetails
        {
            get { return _familydetails; }
            set { _familydetails = value; }
        }
    }

    public class CrewMemberFamilyDetailsInsertDataParameters
    {
        private FamilyDetails _familydetails;
        private SqlParameter[] _parameters;

        public CrewMemberFamilyDetailsInsertDataParameters(FamilyDetails Familydetails)
        {
            FamilyDetails = Familydetails;
            Build();
        }
        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewFamilyId"		, _familydetails.Crewfamilyid) ,
                new SqlParameter( "@CrewId"		, _familydetails.CrewId ) ,
                new SqlParameter( "@FirstName"		, _familydetails.Firstname) ,
                new SqlParameter( "@MiddleName"		, _familydetails.Middlename ) ,
                new SqlParameter( "@LastName"		, _familydetails.Lastname) ,
                new SqlParameter( "@RelationShipId"		, _familydetails.Relationshipid) ,
                new SqlParameter( "@isNOK"		, _familydetails.IsNok) ,
                new SqlParameter( "@SexTypeId"		, _familydetails.SextypeId) ,
                new SqlParameter( "@PlaceOfBirth"		, _familydetails.Placeofbirth) ,
                new SqlParameter( "@NationalityId"		, _familydetails.Nationalityid) ,
                new SqlParameter( "@Address1"		, _familydetails.Address1) ,
                new SqlParameter( "@Address2"		, _familydetails.Address2) ,
                new SqlParameter( "@Address3"		, _familydetails.Address3) ,
                new SqlParameter( "@CountryId"		, _familydetails.Countryid) ,
                new SqlParameter( "@State"		, _familydetails.State) ,
                new SqlParameter( "@City"		, _familydetails.City) ,
                new SqlParameter( "@PinCode"		, _familydetails.Pin) ,
                new SqlParameter( "@NearestAirportId"		, _familydetails.NearestAirportid) ,
                new SqlParameter( "@TelephoneCountryId"		, _familydetails.TelCountryid) ,
                new SqlParameter( "@TelephoneAreaCode"		, _familydetails.TelAreaCode) ,
                new SqlParameter( "@TelephoneNumber"		, _familydetails.Telno) ,
                new SqlParameter( "@MobileCountryId"		, _familydetails.MobileCountryid) ,
                new SqlParameter( "@MobileNumber"		, _familydetails.Mobileno) ,
                new SqlParameter( "@FaxCountryId"		, _familydetails.FaxCountryid) ,
                new SqlParameter( "@FaxAreaCode"		, _familydetails.FaxAreaCode) ,
                new SqlParameter( "@FaxNumber"		, _familydetails.Faxno) ,
                new SqlParameter( "@Email1"		, _familydetails.Email1) ,
                new SqlParameter( "@Email2"		, _familydetails.Email2) ,
                new SqlParameter( "@PassportNumber"		, _familydetails.Passportno) ,
                new SqlParameter( "@IssueDate"		, _familydetails.Issuedate) ,
                new SqlParameter( "@ExpiryDate"		, _familydetails.Expirydate ) ,
                new SqlParameter( "@PlaceOfIssue"		, _familydetails.Placeofissue) ,
                new SqlParameter( "@BankName"		, _familydetails.Bankname ) ,
                new SqlParameter( "@BankAccountNumber"		, _familydetails.Bankaccountno) ,
                new SqlParameter( "@BankAddress"		, _familydetails.Bankaddress) ,
                new SqlParameter( "@BranchName"		, _familydetails.Branchname) ,
                new SqlParameter( "@Beneficiary"		, _familydetails.Beneficiary) ,
                new SqlParameter( "@PersonalCode"		, _familydetails.Personalcode) ,
                new SqlParameter( "@SwiftCode"		, _familydetails.Swiftcode) ,
                new SqlParameter( "@IBANNumber"		, _familydetails.Ibanno) ,
                new SqlParameter( "@TypeOfRemittanceId"		, _familydetails.Typeofremittanceid) ,
                new SqlParameter( "@DOB"		, _familydetails.DOB) ,
                new SqlParameter( "@ECNR"		, _familydetails.ECNR) ,
                new SqlParameter( "@PhotoPath"		, _familydetails.PhotoPath ) ,
                new SqlParameter( "@CreatedBy"		, _familydetails.Createdby ) ,
                new SqlParameter( "@Modifiedby"		, _familydetails.Modifiedby) ,
                new SqlParameter( "@Status"     , _familydetails.Status),
            };

            Parameters = parameters;
        }
        public FamilyDetails FamilyDetails
        {
            get { return _familydetails; }
            set { _familydetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

    }
}
