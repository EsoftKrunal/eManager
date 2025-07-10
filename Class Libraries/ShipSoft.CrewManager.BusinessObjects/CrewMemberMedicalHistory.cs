using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
    public class CrewMemberMedicalHistory
    {
        private int _MedicalCaseId = -1;
        private int _CrewId;
        private DateTime _CaseDate;
        private int _VesselId;
        private int _PortId;
        private string _CaseNumber ;
        private char  _CaseStatus;
        private string _Amount;
        private string _Description;
        private int _CreatedBy;
        private DateTime _CreatedOn;
        private int _ModifiedBy;
        private DateTime _ModifiedOn;

        public int MedicalCaseId
        {
            get { return _MedicalCaseId; }
            set { _MedicalCaseId = value; }
        }
        public int CrewId
        {
            get { return _CrewId; }
            set { _CrewId = value; }
        }
        public DateTime  CaseDate
        {
            get { return _CaseDate; }
            set { _CaseDate = value; }
        }
        public int VesselId
        {
            get { return _VesselId; }
            set { _VesselId = value; }
        }
        public int PortId
        {
            get { return _PortId; }
            set { _PortId = value; }
        }
        public string CaseNumber
        {
            get { return _CaseNumber; }
            set { _CaseNumber = value; }
        }
        public char CaseStatus
        {
            get { return _CaseStatus; }
            set { _CaseStatus = value; }
        }
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
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
        public int Modifiedby
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
