using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessAddCrewMemberDocumentDetails : IBusinessLogic
    {
        private TravelDocumentDetails _documentdetails;
        public ProcessAddCrewMemberDocumentDetails()
        {
        }
        public void Invoke()
        {
            CrewMemberDocumentDetailsInsertData crewmemberdocumentdetailsinsertdata = new CrewMemberDocumentDetailsInsertData();
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
