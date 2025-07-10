using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Delete;

using ShipSoft.CrewManager.BusinessObjects; 

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessDeleteCrewMemberOtherDocuments:IBusinessLogic 
    {
         private CrewOtherDocumentsDetails   _crewotherdocdetails;

        public ProcessDeleteCrewMemberOtherDocuments()
        {

        }

        public void Invoke()
        {
            CrewMemberOtherDocumentDeleteData crewotherdocdelete = new CrewMemberOtherDocumentDeleteData();
            crewotherdocdelete.OtherDocDetails = this.OtherDocumentDetails;
            crewotherdocdelete.Delete();
        }

        public CrewOtherDocumentsDetails OtherDocumentDetails
        {
            get { return _crewotherdocdetails; }
            set { _crewotherdocdetails = value; }
        }
    }
}
