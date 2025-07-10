using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
   public class TravelDocumentDetails
    {
       private int _t_traveldocumentid=-1;
       private int _t_crewid;
       private string _t_ECNR;
       private int _t_FlagStateId;
       private string _t_VisaName;
       private int _t_documenttypeid;
       private string _t_documentnumber;
       private string _t_issuedate;
       private string _t_expirydate;
       private string _t_placeofissue;
       private string _t_imagepath;
       private int _t_createdby;
       private int _t_modifiedby;
      
       public TravelDocumentDetails()
       {
       }
       public int TravelDocumentId
       {
           get { return _t_traveldocumentid; }
           set { _t_traveldocumentid = value; }
       }
       public int CrewId
       {
           get { return _t_crewid; }
           set { _t_crewid = value; }
       }
       public int DocumentTypeId
       {
           get { return _t_documenttypeid; }
           set { _t_documenttypeid = value; }
       }
       public int FlagStateId
       {
           get { return _t_FlagStateId; }
           set { _t_FlagStateId = value; }
       }
       public string ECNR
       {
           get { return _t_ECNR; }
           set { _t_ECNR = value; }
       }
       
       public string VisaName
       {
           get { return _t_VisaName; }
           set { _t_VisaName = value; }
       }
       public string DocumentNumber
       {
           get { return _t_documentnumber; }
           set { _t_documentnumber = value; }
       }
       public string IssueDate
       {
           get { return _t_issuedate; }
           set { _t_issuedate = value; }
       }
       public string ExpiryDate
       {
           get { return _t_expirydate; }
           set { _t_expirydate = value; }
       }
       public string PlaceOfIssue
       {
           get { return _t_placeofissue; }
           set { _t_placeofissue = value; }
       }
       public string ImagePath
       {
           get { return _t_imagepath; }
           set { _t_imagepath = value; }
       }
       public int CreatedBy
       {
           get { return _t_createdby; }
           set { _t_createdby = value; }
       }
       public int ModifiedBy
       {
           get { return _t_modifiedby; }
           set { _t_modifiedby = value; }
       }
    }
}
