using System;
using System.Collections.Generic;
using System.Text;

using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.DataAccessLayer.Delete;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessDeleteCrewMemberExperienceDetailsById : IBusinessLogic
    {
        private ExperienceDetails _ExperienceDetails;

        public ProcessDeleteCrewMemberExperienceDetailsById()
        {

        }

        public void Invoke()
        {
            CrewMemberExperienceDetailsDeleteData crewmemberdata = new CrewMemberExperienceDetailsDeleteData();
            crewmemberdata.ExperienceDetails = this.ExperienceDetails;
            crewmemberdata.Delete();
        }

        public ExperienceDetails ExperienceDetails
        {
            get { return _ExperienceDetails; }
            set { _ExperienceDetails = value; }
        }
    }
}
