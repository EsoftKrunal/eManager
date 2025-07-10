using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data; 

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessCrewMemberAppraisalSelectData:IBusinessLogic 
    {
        private CrewApprasialDetails _crewAprasialDetails;
        private DataSet  _result;
        public ProcessCrewMemberAppraisalSelectData()
        {

        }
        public void Invoke()
        {
            CrewMemberApprasialSelectDetailsIyCrewId crewappselectdata = new CrewMemberApprasialSelectDetailsIyCrewId();
            crewappselectdata.ApprasialDetails  = this.ApprasialDetails;
            ResultSet = crewappselectdata.Get();
            if ( ResultSet.Tables[0].Rows.Count==1)
            {
                        
                DataRow dr;
                dr = ResultSet.Tables[0].Rows[0];
                ApprasialDetails.AppraisalOccasionId = Convert.ToInt16(dr["AppraisalOccasionId"].ToString());
                ApprasialDetails.AverageMarks = Convert.ToDouble(dr["AvgMarks"].ToString());
                ApprasialDetails.ApprasialFrom = dr["FromDate"].ToString();
                ApprasialDetails.ApprasialTo = dr["ToDate"].ToString();
                ApprasialDetails.OfficeRemarks  = dr["OfficeRemarks"].ToString();
                ApprasialDetails.AppraiserRemarks = dr["AppraiserRemarks"].ToString();
                ApprasialDetails.ImagePath = dr["ImagePath"].ToString();
                ApprasialDetails.VesselId= Convert.ToInt32(dr["VesselId"].ToString());
                ApprasialDetails.Recommended = dr["Recommended"].ToString();
                ApprasialDetails.N_CompScore = dr["N_CompScore"].ToString();
                ApprasialDetails.N_PerfScore = dr["N_PerfScrore"].ToString();
                ApprasialDetails.N_DateJoinCompany = dr["N_DateJoinCompany"].ToString();
                ApprasialDetails.N_EOCAlertDate = dr["N_EOCAlertDate"].ToString();
                ApprasialDetails.N_Recommended = dr["N_RecommendedNew"].ToString();
                ApprasialDetails.N_ReportNo = dr["N_ReportNo"].ToString();
                ApprasialDetails.N_TrainingRequired = dr["N_TrainingRequired"].ToString();
                ApprasialDetails.N_SignOnDate = dr["N_SignOnDate"].ToString();
                ApprasialDetails.N_UpdatedBy = dr["N_UpdatedBy"].ToString();
                ApprasialDetails.N_UpdatedOn = dr["N_UpdatedOn"].ToString();
            }
        }

        public CrewApprasialDetails ApprasialDetails
        {
            get { return _crewAprasialDetails; }
            set { _crewAprasialDetails = value; }
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
