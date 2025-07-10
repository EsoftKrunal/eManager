using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
  public   class CrewLicenseDetails
    {
        private int _CrewLicenseId;
        private int _CrewId;
        private int _LicenseId;
        private string _Grade;
        private string _Number;
        private string _IssueDate;
        private string _ExpiryDate;
        private string _PlaceOfIssue;
        private string _ImagePath;
        private int _CountryId;
        private string _IsVerified;
        private int _VerifiedBy;
        
        private int _CreatedBy;
        private DateTime _CreatedOn;
        private int _ModifiedBy;
        private DateTime _ModifiedOn;

        public int CrewLicenseId
        {
            get { return _CrewLicenseId; }
            set { _CrewLicenseId = value; }
        }
        public int CrewId
        {
            get { return _CrewId; }
            set { _CrewId = value; }
        }
        
        public int LicenseId
        {
            get { return _LicenseId; }
            set { _LicenseId = value; }

        }
        public string Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }

        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        public string IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }
        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        public string PlaceOfIssue
        {
            get { return _PlaceOfIssue; }
            set { _PlaceOfIssue = value; }
        }

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
      public int CountryId
      {
          get { return _CountryId; }
          set { _CountryId = value; }

      }
        public int VerifiedBy
        {
              get { return _VerifiedBy; }
              set { _VerifiedBy = value; }
        }
        public string IsVerified
        {
              get { return _IsVerified ; }
              set { _IsVerified = value; }
        }
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime CreatedOn
        {
            get { return _CreatedOn; }
            set { _CreatedOn = value; }
        }
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime ModifiedOn
        {
            get { return _ModifiedOn; }
            set { _ModifiedOn = value; }
        } 
    }
}
