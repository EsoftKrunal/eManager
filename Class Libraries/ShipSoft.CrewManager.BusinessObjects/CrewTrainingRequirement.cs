using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
   public  class CrewTrainingRequirement
    {
        private int _TrainingRequirementId;
        private int _CrewId;
        private int _TrainingId;
        private string _Remark;
        private int _CreatedBy;
        private DateTime _CreatedOn;
        private int _ModifiedBy;
        private DateTime _ModifiedOn;
        private string _N_DueDate;
        private string _N_CrewTrainingStatus;
        private Int32 _N_CrewAppraisalId;
        private string _N_CrewVerified;

        public int TrainingRequirementId
        {
            get { return _TrainingRequirementId; }
            set { _TrainingRequirementId = value; }
        }
        public int CrewId
        {
            get { return _CrewId; }
            set { _CrewId = value; }
        }

        public int TrainingId
        {
            get { return _TrainingId; }
            set { _TrainingId = value; }

        }
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
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
        public string N_DueDate
        {
            get { return _N_DueDate; }
            set { _N_DueDate = value; }
        }
       public string N_CrewTrainingStatus
       {
           get { return _N_CrewTrainingStatus; }
           set { _N_CrewTrainingStatus = value; }
       }

       public string N_CrewVerified
       {
           get { return _N_CrewVerified; }
           set { _N_CrewVerified = value; }
       }
       
       public Int32 N_CrewAppraisalId
       {
           get { return _N_CrewAppraisalId; }
           set { _N_CrewAppraisalId = value; }
       }
    }
}
