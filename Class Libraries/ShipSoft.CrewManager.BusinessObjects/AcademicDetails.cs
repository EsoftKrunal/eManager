using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
    public class AcademicDetails
    {
        // Attributes
        private int _AcademicDetailsId = -1;
        private int _CrewId;
        private string _TypeOfCertificate;
        private string _Institute;
        private string _DurationForm;
        private string _DurationTo;
        private string _Grade;
        private string _ImagePath;
        private int _CreatedBy=0;
        private int _ModifiedBy=0;

        //Constructor
        public AcademicDetails()
        {
        }

        //Properties
        public int AcademicDetailsId 
        {
            get { return _AcademicDetailsId; }
            set { _AcademicDetailsId = value; }
        }
        public int CrewId
        {
            get { return _CrewId; }
            set { _CrewId = value; }
        }
        public string TypeOfCertificate
        {
            get { return _TypeOfCertificate; }
            set { _TypeOfCertificate = value; }
        }
        public string Institute
        {
            get { return _Institute; }
            set { _Institute = value; }
        }
        public string DurationForm
        {
            get { return _DurationForm; }
            set { _DurationForm = value; }
        }
        public string DurationTo
        {
            get { return _DurationTo; }
            set { _DurationTo = value; }
        }
        public string Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
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
        public int Modifiedby
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
    }
}
