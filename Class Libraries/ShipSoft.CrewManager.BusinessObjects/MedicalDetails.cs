using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
   public class MedicalDetails
    {
       private int _m_medicaldetailsid=-1;
       private int _m_crewid;
       private int _m_documenttypeid;
       private string _m_documentname;
       private string _m_bloodgrp;
       private string _m_documentnumber;
       private string _m_issuedate;
       private string _m_expirydate;
       private string _m_placeofissue;
       private string _m_imagepath;
       private int _m_createdby;
       private int _m_modifiedby;
       public MedicalDetails()
       {
       }
       public int MedicalDetailsId
       {
           get { return _m_medicaldetailsid; }
           set { _m_medicaldetailsid = value; }
       }
       public int CrewId
       {
           get { return _m_crewid; }
           set { _m_crewid = value; }
       }
        public int DocumentTypeId
       {
           get { return _m_documenttypeid; }
           set { _m_documenttypeid = value; }
       }
       public string DocumentName
       {
           get { return _m_documentname; }
           set { _m_documentname = value; }
       }
       public string BloodGroup
       {
           get { return _m_bloodgrp; }
           set { _m_bloodgrp = value; }
       }
        public string DocumentNumber
       {
           get { return _m_documentnumber; }
           set { _m_documentnumber = value; }
       }
       public string IssueDate
       {
           get { return _m_issuedate; }
           set { _m_issuedate = value; }
       }
       public string ExpiryDate
       {
           get { return _m_expirydate; }
           set { _m_expirydate = value; }
       }
       public string PlaceOfIssue
       {
           get { return _m_placeofissue; }
           set { _m_placeofissue = value; }
       }
       public string ImagePath
       {
           get { return _m_imagepath; }
           set { _m_imagepath = value; }
       }
       public int CreatedBy
       {
           get { return _m_createdby; }
           set { _m_createdby = value; }
       }
       public int ModifiedBy
       {
           get { return _m_modifiedby; }
           set { _m_modifiedby = value; }
       }
    }
}
