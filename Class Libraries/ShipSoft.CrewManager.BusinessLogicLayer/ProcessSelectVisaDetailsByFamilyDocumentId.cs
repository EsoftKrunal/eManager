using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectVisaDetailsByFamilyDocumentId : IBusinessLogic
    {
       private CrewFamilyDocumentDetails _crewFamilyDocumentDetails;
        private DataSet _result;

        public ProcessSelectVisaDetailsByFamilyDocumentId()
    {
 
    }
        public void Invoke()
        {
            VisaDetailsSelectDataByFamilyDocumentId visaDetailsSelectDataByFamilyDocumentId = new VisaDetailsSelectDataByFamilyDocumentId();
            visaDetailsSelectDataByFamilyDocumentId.CrewFamilyDocumentDetails = CrewFamilyDocumentDetails;
            ResultSet = visaDetailsSelectDataByFamilyDocumentId.Get();
            CrewFamilyDocumentDetails.DocumentName = ResultSet.Tables[0].Rows[0]["DocumentName"].ToString();
            CrewFamilyDocumentDetails.DocumentType = ResultSet.Tables[0].Rows[0]["DocumentType"].ToString();
            CrewFamilyDocumentDetails.DocumentNumber = ResultSet.Tables[0].Rows[0]["DocumentNumber"].ToString();
            CrewFamilyDocumentDetails.DocumentName = ResultSet.Tables[0].Rows[0]["DocumentName"].ToString();
            CrewFamilyDocumentDetails.Issuedate = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["IssueDate"])) ? "" : Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["IssueDate"].ToString()).ToString("MM/dd/yyyy"); 
            CrewFamilyDocumentDetails.Expirydate = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["ExpiryDate"])) ? "" : Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["ExpiryDate"].ToString()).ToString("MM/dd/yyyy");
            CrewFamilyDocumentDetails.Placeofissue = ResultSet.Tables[0].Rows[0]["PlaceOfIssue"].ToString();
            CrewFamilyDocumentDetails.DocumentFlag = Convert.ToChar(ResultSet.Tables[0].Rows[0]["DocumentFlag"].ToString());
        }

       public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
       {
           get { return _crewFamilyDocumentDetails; }
           set { _crewFamilyDocumentDetails = value; }
       }

        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
