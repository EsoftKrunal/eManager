using System;
using System.Collections.Generic;
using System.Text;

using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.DataAccessLayer.Delete;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessDeleteCrewMemberTravelDetailsById : IBusinessLogic
    {
        private TravelDocumentDetails _DocumentDetails;

        public ProcessDeleteCrewMemberTravelDetailsById()
        {

        }

        public void Invoke()
        {
            CrewMemberTravelDetailsDeleteData crewmemberdata = new CrewMemberTravelDetailsDeleteData();
            crewmemberdata.DocumentDetails = this.DocumentDetails;
            crewmemberdata.Delete();
        }

        public TravelDocumentDetails DocumentDetails
        {
            get { return _DocumentDetails; }
            set { _DocumentDetails = value; }
        }
    }
}
