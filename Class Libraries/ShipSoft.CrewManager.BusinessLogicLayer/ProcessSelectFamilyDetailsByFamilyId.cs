using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;


namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectFamilyDetailsByFamilyId : IBusinessLogic
    {
       private FamilyDetails _familydetails;
        private DataSet _result;

        public ProcessSelectFamilyDetailsByFamilyId()
    {
 
    }
        public void Invoke()
        {
            FamilyDetailsSelectDataByFamilyId familyDetailsSelectDataByFamilyId = new FamilyDetailsSelectDataByFamilyId();
            familyDetailsSelectDataByFamilyId.FamilyDetails = FamilyDetails;
            ResultSet = familyDetailsSelectDataByFamilyId.Get();
            FamilyDetails.Firstname = ResultSet.Tables[0].Rows[0]["FirstName"].ToString();
            FamilyDetails.Middlename = ResultSet.Tables[0].Rows[0]["MiddleName"].ToString();
            FamilyDetails.Lastname = ResultSet.Tables[0].Rows[0]["LastName"].ToString();
            FamilyDetails.Relationshipid =Convert.ToInt32(ResultSet.Tables[0].Rows[0]["RelationshipId"].ToString());
            FamilyDetails.IsNok = Convert.ToChar(ResultSet.Tables[0].Rows[0]["IsNOK"].ToString());
            FamilyDetails.SextypeId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["SexTypeId"].ToString());
            FamilyDetails.Placeofbirth = ResultSet.Tables[0].Rows[0]["PlaceOfBirth"].ToString();
            FamilyDetails.Nationalityid = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["NationalityId"].ToString());
            FamilyDetails.Address1 = ResultSet.Tables[0].Rows[0]["Address1"].ToString();
            FamilyDetails.Address2 = ResultSet.Tables[0].Rows[0]["Address2"].ToString();
            FamilyDetails.Address3 = ResultSet.Tables[0].Rows[0]["Address3"].ToString();
            FamilyDetails.Countryid = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CountryId"].ToString());
            FamilyDetails.State = ResultSet.Tables[0].Rows[0]["State"].ToString();
            FamilyDetails.City = ResultSet.Tables[0].Rows[0]["City"].ToString();
            FamilyDetails.Pin = ResultSet.Tables[0].Rows[0]["PinCode"].ToString();
            FamilyDetails.NearestAirportid = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["NearestAirportId"].ToString());
            FamilyDetails.TelCountryid = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["TelephoneCountryId"].ToString());
            FamilyDetails.TelAreaCode = ResultSet.Tables[0].Rows[0]["TelephoneAreaCode"].ToString();
            FamilyDetails.Telno = ResultSet.Tables[0].Rows[0]["TelephoneNumber"].ToString();
            FamilyDetails.MobileCountryid = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["MobileCountryId"].ToString());
            FamilyDetails.Mobileno = ResultSet.Tables[0].Rows[0]["MobileNumber"].ToString();
            FamilyDetails.FaxCountryid = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["FaxCountryId"].ToString());
            FamilyDetails.FaxAreaCode = ResultSet.Tables[0].Rows[0]["FaxAreaCode"].ToString();
            FamilyDetails.Faxno = ResultSet.Tables[0].Rows[0]["FaxNumber"].ToString();
            FamilyDetails.Email1 = ResultSet.Tables[0].Rows[0]["Email1"].ToString();
            FamilyDetails.Email2 = ResultSet.Tables[0].Rows[0]["Email2"].ToString();
            FamilyDetails.Passportno = ResultSet.Tables[0].Rows[0]["PassportNumber"].ToString();
            FamilyDetails.Issuedate = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["IssueDate"])) ? "" : Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["IssueDate"].ToString()).ToString("MM/dd/yyyy");
            FamilyDetails.Expirydate = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["ExpiryDate"])) ? "" :Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["ExpiryDate"].ToString()).ToString("MM/dd/yyyy");
            FamilyDetails.Placeofissue = ResultSet.Tables[0].Rows[0]["PlaceOfIssue"].ToString();
            FamilyDetails.Bankname = ResultSet.Tables[0].Rows[0]["BankName"].ToString();
            FamilyDetails.Branchname = ResultSet.Tables[0].Rows[0]["BranchName"].ToString();
            FamilyDetails.Beneficiary = ResultSet.Tables[0].Rows[0]["Beneficiary"].ToString();
            FamilyDetails.Bankaccountno = ResultSet.Tables[0].Rows[0]["BankAccountNumber"].ToString();
            FamilyDetails.Personalcode = ResultSet.Tables[0].Rows[0]["PersonalCode"].ToString();
            FamilyDetails.Swiftcode = ResultSet.Tables[0].Rows[0]["SwiftCode"].ToString();
            FamilyDetails.Ibanno = ResultSet.Tables[0].Rows[0]["IBANNumber"].ToString();
            FamilyDetails.Bankaddress = ResultSet.Tables[0].Rows[0]["BankAddress"].ToString();
            FamilyDetails.DOB = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["DateOfBirth"])) ? "" : Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["DateOfBirth"].ToString()).ToString("MM/dd/yyyy");
            FamilyDetails.ECNR = ResultSet.Tables[0].Rows[0]["ECNR"].ToString();
            FamilyDetails.Typeofremittanceid = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["TypeOfRemittanceId"].ToString());
            FamilyDetails.PhotoPath = ResultSet.Tables[0].Rows[0]["PhotoPath"].ToString();
        }

       public FamilyDetails FamilyDetails
       {
           get { return _familydetails; }
           set { _familydetails = value; }
       }

        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
