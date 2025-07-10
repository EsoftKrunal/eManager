using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectCrewMemberExperienceDetails_MTMSM : IBusinessLogic
    {
        private DataSet _resultset;
        private ExperienceDetails _experiencedetails;

        public ProcessSelectCrewMemberExperienceDetails_MTMSM()
        {
        }
        public void Invoke()
        {
            CrewMemberExperienceDetailsSelectData_MTMSM experienceselectdata = new CrewMemberExperienceDetailsSelectData_MTMSM();
            experienceselectdata.ExperienceDetails = _experiencedetails;
            ResultSet = experienceselectdata.Get();
            ExperienceDetails.CrewId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CrewId"].ToString());
        }
        public ExperienceDetails ExperienceDetails
        {
            get { return _experiencedetails; }
            set { _experiencedetails = value; }
        }
        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
