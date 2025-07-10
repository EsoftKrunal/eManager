using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessAddCrewMemberTravelDocumentDetails : IBusinessLogic
    {
        private TravelDocumentDetails _documentdetails;
        public ProcessAddCrewMemberTravelDocumentDetails()
        {
        }
        public void Invoke()
        {
            CrewMemberTravelDocumentDetailsInsertData crewmemberdocumentdetailsinsertdata = new CrewMemberTravelDocumentDetailsInsertData();
            crewmemberdocumentdetailsinsertdata.DocumentDetails = this.DocumentDetails;
            crewmemberdocumentdetailsinsertdata.Add();
        }
        public TravelDocumentDetails DocumentDetails
        {
            get { return _documentdetails; }
            set { _documentdetails = value; }
        }
    }
}
