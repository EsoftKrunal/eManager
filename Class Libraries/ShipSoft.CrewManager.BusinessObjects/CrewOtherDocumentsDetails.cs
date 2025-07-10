using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
   public  class CrewOtherDocumentsDetails
    {
        private int _CrewOtherDocId;
        private int _CrewId;
        private int _CourseId;
        private string _DocumentNumber;
        private string _DateOfIssue;
        private string _ExpiryDate;
        private string _IssuedBy;
        private string _ImagePath;
        private int _CreatedBy;
        private DateTime _CreatedOn;
        private int _ModifiedBy;
        private DateTime _ModifiedOn;
       private char _IsActive;

        public int CrewOtherDocId
        {
            get { return _CrewOtherDocId; }
            set { _CrewOtherDocId = value; }
        }
        public int CrewId
        {
            get { return _CrewId; }
            set { _CrewId = value; }
        }
        public int CourseId
        {
            get { return _CourseId; }
            set { _CourseId = value; }

        }
        public string DocumentNumber
        {
            get { return _DocumentNumber; }
            set { _DocumentNumber = value; }
        }
        public string DateOfIssue
        {
            get { return _DateOfIssue; }
            set { _DateOfIssue = value; }
        }
        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        public string IssuedBy
        {
            get { return _IssuedBy; }
            set { _IssuedBy = value; }
        }

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
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
       public char IsActive
       { 
            get { return _IsActive; }
           set { _IsActive=value; }
       }

    }
}
