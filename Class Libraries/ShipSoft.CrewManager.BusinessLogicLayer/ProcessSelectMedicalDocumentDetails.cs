using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectMedicalDocumentDetails:IBusinessLogic
    {
       private MedicalDetails _medicaldetails;
       private DataSet _resultset;
       public ProcessSelectMedicalDocumentDetails()
       {
       }
       public void Invoke()
       {
           MedicalDocumentDetailsSelectData medicaldetailsselectdata = new MedicalDocumentDetailsSelectData();
           medicaldetailsselectdata.MedicalDetails = MedicalDetails;
           ResultSet = medicaldetailsselectdata.Get();
           if (ResultSet.Tables.Count > 0)
           {
               if (ResultSet.Tables[0].Rows.Count > 0)
               {
                   _medicaldetails.MedicalDetailsId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["MedicalDetailsId"].ToString());
                   _medicaldetails.CrewId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CrewId"].ToString());
                   _medicaldetails.DocumentTypeId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["DocumentTypeId"].ToString());
                   _medicaldetails.DocumentName = ResultSet.Tables[0].Rows[0]["DocumentName"].ToString();
                   _medicaldetails.BloodGroup = ResultSet.Tables[0].Rows[0]["BloodGroup"].ToString();
                   _medicaldetails.DocumentNumber = ResultSet.Tables[0].Rows[0]["DocumentNumber"].ToString();
                   _medicaldetails.IssueDate = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["IssueDate"])) ? "" : ResultSet.Tables[0].Rows[0]["IssueDate"].ToString();
                   _medicaldetails.ExpiryDate = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["ExpiryDate"])) ? "" : ResultSet.Tables[0].Rows[0]["ExpiryDate"].ToString();
                   _medicaldetails.PlaceOfIssue = ResultSet.Tables[0].Rows[0]["PlaceOfIssue"].ToString();
                   _medicaldetails.ImagePath = ResultSet.Tables[0].Rows[0]["ImagePath"].ToString();
               }
          }
       }
       public MedicalDetails MedicalDetails
       {
           get { return _medicaldetails; }
           set { _medicaldetails = value; }
       }
       public DataSet ResultSet
       {
           get { return _resultset; }
           set {_resultset = value; }
       }
       }
    }
