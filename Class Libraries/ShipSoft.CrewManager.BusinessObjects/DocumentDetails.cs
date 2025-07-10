using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
   public class DocumentDetails
    {
       private int _crewdocumentid=-1;
       private int _crewid;
       private int _documenttypeid;
       private int _documentsubtypeid;
       private string _documentnumber;
       private string _documentname;
       private DateTime _issuedate;
       private DateTime _expirydate;
       private char _neverexpires;
       private string _placeofissue;
       private string _imagepath = String.Empty;
       private string _remarks;
       private int _createdby;
       private int _modifiedby;
       //private DateTime _modifiedon;
       public DocumentDetails()
       {
       }
       public int CrewDocumentId
       {
           get { return _crewdocumentid; }
           set { _crewdocumentid = value; }
       }
       public int CrewId
       {
           get { return _crewid; }
           set { _crewid = value; }
       }
        public int DocumentTypeId
       {
           get { return _documenttypeid; }
           set { _documenttypeid = value; }
       } 
        public int DocumentSubTypeId
       {
           get { return _documentsubtypeid; }
           set { _documentsubtypeid = value; }
       }
        public string DocumentNumber
       {
           get { return _documentnumber; }
           set { _documentnumber = value; }
       }
       public string DocumentName
       {
           get { return _documentname; }
           set {_documentname = value; }
       }
       public DateTime IssueDate
       {
           get { return _issuedate; }
           set { _issuedate = value; }
       }
       public DateTime ExpiryDate
       {
           get { return _expirydate; }
           set { _expirydate = value; }
       }
       public char NeverExpires
       {
           get { return _neverexpires; }
           set { _neverexpires = value; }
       }
       public string PlaceOfIssue
       {
           get { return _placeofissue; }
           set { _placeofissue = value; }
       }
       public string ImagePath
       {
           get { return _imagepath; }
           set { _imagepath = value; }
       }
       public string Remarks
       {
           get { return _remarks; }
           set {_remarks = value; }
       }
       public int CreatedBy
       {
           get { return _createdby; }
           set { _createdby = value; }
       }
       public int ModifiedBy
       {
           get { return _modifiedby; }
           set { _modifiedby = value; }
       }
       //public DateTime ModifiedOn
       //{
       //    get { return _modifiedon; }
       //    set { _modifiedon = value; }
       //}
    }
}
