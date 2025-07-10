using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectCrewMemberExperienceDetailsById : IBusinessLogic
    {
       private ExperienceDetails _experiencedetails;
        private DataSet _result;

       public ProcessSelectCrewMemberExperienceDetailsById()
    {
 
    }
        public void Invoke()
        {
            CrewMemberExperienceDetailsSelectDataById experiencedetailsselectdata = new CrewMemberExperienceDetailsSelectDataById();
            experiencedetailsselectdata.ExperienceDetails = ExperienceDetails;
            ResultSet = experiencedetailsselectdata.Get();
            ExperienceDetails.Expflag =Convert.ToChar(ResultSet.Tables[0].Rows[0]["ExpFlag"].ToString());
            ExperienceDetails.Companyname = ResultSet.Tables[0].Rows[0]["CompanyName"].ToString();
            ExperienceDetails.RankId =Convert.ToInt32(ResultSet.Tables[0].Rows[0]["RankId"].ToString());
            ExperienceDetails.SignOnDate = Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["SignOn"].ToString());
            ExperienceDetails.SignOffDate = Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["SignOff"].ToString());
            ExperienceDetails.SignOffReasonId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["SignOffReasonId"].ToString());
            ExperienceDetails.Vesselname = ResultSet.Tables[0].Rows[0]["VesselName"].ToString();
            ExperienceDetails.VesseltypeId = Convert.ToInt32( ResultSet.Tables[0].Rows[0]["VesselTypeId"].ToString());
            //ExperienceDetails.Registry = ResultSet.Tables[0].Rows[0]["Registry"].ToString();
            //ExperienceDetails.Dwt = ResultSet.Tables[0].Rows[0]["DWT"].ToString();
            //ExperienceDetails.Gwt = ResultSet.Tables[0].Rows[0]["GWT"].ToString();
            ExperienceDetails.Bhp = ResultSet.Tables[0].Rows[0]["BHP"].ToString();
            ExperienceDetails.Bhp1 = ResultSet.Tables[0].Rows[0]["BHP1"].ToString();
        }

       public ExperienceDetails ExperienceDetails
       {
           get { return _experiencedetails; }
           set { _experiencedetails = value; }
       }

        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
