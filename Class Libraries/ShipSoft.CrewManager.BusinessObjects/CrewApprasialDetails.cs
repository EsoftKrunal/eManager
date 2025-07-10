using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
  public  class CrewApprasialDetails
    {
        private int _CrewAppraisalId;
        private int _CrewId;
        private int _AppraisalOccasionId;
        private double _AverageMarks;
        private string _ApprasialFrom;
        private string _ApprasialTo;
        private string _AppraiserRemarks;
        private string _AppraiseeRemarks;
        private string _OfficeRemarks;
        private int _VesselId;
        private string _Recommended;
        private int _CreatedBy;
        private DateTime _CreatedOn;
        private int _ModifiedBy;
        private DateTime _ModifiedOn;
        private string _imagepath;
      private string _n_PerfScore;
      private string _n_CompScore;
      private string _n_ReportNo;
      private string _n_Recommended;
      private string _n_EOCAlertDt;
      private string _n_TrainRequired;
      private string _n_DateJoinCompany;
      private string _n_SignOnDate;
      private string _n_UpdatedBy;
      private string _n_UpdatedOn;
        public int CrewAppraisalId
        {
            get { return _CrewAppraisalId; }
            set { _CrewAppraisalId = value; }
        }
        public int CrewId
        {
            get { return _CrewId; }
            set { _CrewId = value; }
        }
        public int AppraisalOccasionId
        {
            get { return _AppraisalOccasionId; }
            set { _AppraisalOccasionId = value; }

        }
        public double AverageMarks
        {
            get { return _AverageMarks; }
            set { _AverageMarks = value; }
        }
        public string ApprasialFrom
        {
            get { return _ApprasialFrom; }
            set { _ApprasialFrom = value; }
        }
        public string ApprasialTo
        {
            get { return _ApprasialTo; }
            set { _ApprasialTo = value; }
        }
        public string AppraiserRemarks
        {
            get { return _AppraiserRemarks; }
            set { _AppraiserRemarks = value; }
        }
        public string AppraiseeRemarks
        {
            get { return _AppraiseeRemarks; }
            set { _AppraiseeRemarks = value; }
        }
        public string OfficeRemarks
        {
            get { return _OfficeRemarks; }
            set { _OfficeRemarks = value; }
        }
        public int VesselId
      {
          get { return _VesselId; }
          set { _VesselId = value; }
      }
        public string Recommended
      {
          get { return _Recommended; }
          set { _Recommended = value; }
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
      public string ImagePath
      {
          get { return _imagepath; }
          set { _imagepath = value; }
      }
      public string N_PerfScore
      {
          get { return _n_PerfScore; }
          set { _n_PerfScore = value; }
      }
      public string N_CompScore
      {
          get { return _n_CompScore; }
          set { _n_CompScore = value; }
      }
      public string N_ReportNo
      {
          get { return _n_ReportNo; }
          set { _n_ReportNo = value; }
      }
      public string N_Recommended
      {
          get { return _n_Recommended; }
          set { _n_Recommended = value; }
      }
      public string N_EOCAlertDate
      {
          get { return _n_EOCAlertDt; }
          set { _n_EOCAlertDt = value; }
      }
      public string N_TrainingRequired
      {
          get { return _n_TrainRequired; }
          set { _n_TrainRequired = value; }
      }
      public string N_DateJoinCompany
      {
          get { return _n_DateJoinCompany; }
          set { _n_DateJoinCompany = value; }
      }
      public string N_SignOnDate
      {
          get { return _n_SignOnDate; }
          set { _n_SignOnDate = value; }
      }
      public string N_UpdatedBy
      {
          get { return _n_UpdatedBy; }
          set { _n_UpdatedBy = value; }
      }
      public string N_UpdatedOn
      {
          get { return _n_UpdatedOn; }
          set { _n_UpdatedOn = value; }
      }
    }
}
