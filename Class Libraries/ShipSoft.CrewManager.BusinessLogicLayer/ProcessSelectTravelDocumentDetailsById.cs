using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectTravelDocumentDetailsById:IBusinessLogic
	{
       private TravelDocumentDetails _documentdetails;
       private DataSet _resultset;
       public ProcessSelectTravelDocumentDetailsById()
       {
       }
       public void Invoke()
       {
           TravelDocumentDetailsSelectDataById documentdetailsselectdatabyid = new TravelDocumentDetailsSelectDataById();
           documentdetailsselectdatabyid.DocumentDetails = DocumentDetails;
           ResultSet = documentdetailsselectdatabyid.Get();
           if (ResultSet.Tables.Count > 0)
           {
               if (ResultSet.Tables[0].Rows.Count > 0)
               {
                   _documentdetails.TravelDocumentId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["TravelDocumentId"].ToString());
                   _documentdetails.CrewId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CrewId"].ToString());
                   _documentdetails.VisaName  =ResultSet.Tables[0].Rows[0]["VisaName"].ToString();
                   _documentdetails.ECNR   =ResultSet.Tables[0].Rows[0]["ECNR"].ToString();
                   _documentdetails.FlagStateId =Convert.ToInt32(ResultSet.Tables[0].Rows[0]["FlagStateId"].ToString());
                   _documentdetails.DocumentTypeId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["DocumentTypeId"].ToString());
                   _documentdetails.DocumentNumber = ResultSet.Tables[0].Rows[0]["DocumentNumber"].ToString();
                   _documentdetails.IssueDate = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["IssueDate"])) ? "" : ResultSet.Tables[0].Rows[0]["IssueDate"].ToString();
                   _documentdetails.ExpiryDate = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["ExpiryDate"])) ? "" : ResultSet.Tables[0].Rows[0]["ExpiryDate"].ToString();
                   _documentdetails.PlaceOfIssue = ResultSet.Tables[0].Rows[0]["PlaceOfIssue"].ToString();
                   _documentdetails.ImagePath = ResultSet.Tables[0].Rows[0]["ImagePath"].ToString();
               }
          }
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
