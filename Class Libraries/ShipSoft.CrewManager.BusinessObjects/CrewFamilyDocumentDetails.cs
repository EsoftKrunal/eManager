using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
   public class CrewFamilyDocumentDetails
    {
       private int _crewfamilydocumentid=-1;
        private int _crewfamilyid;
       private string _documentname;
       private string _documenttype;
       private string _documentnumber;
       private string _issuedate;
       private string _expirydate;
       private string _placeofissue;
       private char _documentflag;
       
       public CrewFamilyDocumentDetails()
        {

        }

       public int Crewfamilydocumentid
       {
           get { return _crewfamilydocumentid; }
           set { _crewfamilydocumentid = value; }
       }

       public int CrewFamilyId 
        {
            get { return _crewfamilyid; }
            set { _crewfamilyid = value; }
        }

       public string DocumentName
        {
            get { return _documentname; }
            set { _documentname = value; }
        }

       public string DocumentType
        {
            get { return _documenttype; }
            set { _documenttype = value; }
        }

       public string DocumentNumber
       {
           get { return _documentnumber; }
           set { _documentnumber = value; }
       }

       public string Issuedate
       {
           get { return _issuedate; }
           set { _issuedate = value; }
       }

       public string Expirydate
       {
           get { return _expirydate; }
           set { _expirydate = value; }
       }

       public string Placeofissue
       {
           get { return _placeofissue; }
           set { _placeofissue = value; }
       }

       public char DocumentFlag
       {
           get { return _documentflag; }
           set { _documentflag = value; }
       }
    }
}
