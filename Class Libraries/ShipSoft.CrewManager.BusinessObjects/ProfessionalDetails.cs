using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
    public class ProfessionalDetails
    {
        // Attributes
        private int _CertificateId = -1;
        private int _CrewId;
        private int _DocumentTypeId;
        private int _DocumentSubTypeId;
        private int _CertificateTypeId;
        private int _CourseTypeId;
        private string _DocumentNumber;
        private DateTime _IssueDate;
        private DateTime _ExpiryDate;
        private char _NeverExpires;
        private string _PlaceOfIssue;
        private string _ImagePath;
        private char _CertificateFlag;
        private int _CreatedBy=0;
        private int _ModifiedBy=0;

        //Constructor
        public ProfessionalDetails()
        {
        }

        //Properties
        public int CertificateId 
        {
            get { return _CertificateId; }
            set { _CertificateId = value; }
        }
        public int CrewId
        {
            get { return _CrewId; }
            set { _CrewId = value; }
        }
        public int DocumentTypeId
        {
            get { return _DocumentTypeId; }
            set { _DocumentTypeId = value; }
        }
        public int DocumentSubTypeId
        {
            get { return _DocumentSubTypeId; }
            set { _DocumentSubTypeId = value; }
        }
        public int CertificateTypeId
        {
            get { return _CertificateTypeId; }
            set { _CertificateTypeId = value; }
        }
        public int CourseTypeId
        {
            get { return _CourseTypeId; }
            set { _CourseTypeId = value; }
        }
        public string DocumentNumber
        {
            get { return _DocumentNumber; }
            set { _DocumentNumber = value; }
        }
        public DateTime IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }
        public DateTime ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        public char NeverExpires
        {
            get { return _NeverExpires; }
            set { _NeverExpires = value; }
        }
        public String PlaceOfIssue
        {
            get { return _PlaceOfIssue; }
            set { _PlaceOfIssue = value; }
        }
        public String ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public char CertificateFlag
        {
            get { return _CertificateFlag; }
            set { _CertificateFlag = value; }
        }
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public int Modifiedby
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
    }
}
