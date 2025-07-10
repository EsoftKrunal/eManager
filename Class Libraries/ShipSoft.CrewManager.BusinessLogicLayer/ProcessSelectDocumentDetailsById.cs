using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectDocumentDetailsById:IBusinessLogic
	{
       private TravelDocumentDetails _documentdetails;
       private DataSet _resultset;
       public ProcessSelectDocumentDetailsById()
       {
       }
       public void Invoke()
       {
           DocumentDetailsSelectDataById documentdetailsselectdatabyid = new DocumentDetailsSelectDataById();
           documentdetailsselectdatabyid.DocumentDetails = DocumentDetails;
           ResultSet = documentdetailsselectdatabyid.Get();
           DocumentDetails.DocumentTypeId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["DocumentTypeId"].ToString());
           //DocumentDetails./ = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["DocumentSubTypeId"].ToString());
           DocumentDetails.DocumentNumber = ResultSet.Tables[0].Rows[0]["DocumentNumber"].ToString();
           //DocumentDetails.DocumentName = ResultSet.Tables[0].Rows[0]["DocumentName"].ToString();
           DocumentDetails.IssueDate = ResultSet.Tables[0].Rows[0]["IssueDate"].ToString();
           //if (DocumentDetails.ExpiryDate.ToString() == "1/1/1900")
           //{
           //    DocumentDetails.ExpiryDate = Convert.ToDateTime("");
           //}
           //else
           //{
               DocumentDetails.ExpiryDate = ResultSet.Tables[0].Rows[0]["ExpiryDate"].ToString();
           //}
           DocumentDetails.PlaceOfIssue = ResultSet.Tables[0].Rows[0]["PlaceOfIssue"].ToString();
           //DocumentDetails.Remarks = ResultSet.Tables[0].Rows[0]["Remarks"].ToString();
       }
       public TravelDocumentDetails DocumentDetails
       {
           get { return _documentdetails; }
           set { _documentdetails = value; }
       }
       public DataSet ResultSet
       {
           get { return _resultset; }
           set {_resultset = value; }
       }
	}
}
