using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Delete;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data; 
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessDeleteCrewApprasialDetails:IBusinessLogic 
    {
        private CrewApprasialDetails _crewAprasialDetails;
        public ProcessDeleteCrewApprasialDetails()
        {

        }

        public void Invoke()
        {
            CrewApprasialDetailsDeleteData crewappdelete = new CrewApprasialDetailsDeleteData();
            crewappdelete.ApprasialDetails  = this.ApprasialDetails;
            crewappdelete.Delete();
        }

        public CrewApprasialDetails ApprasialDetails
        {
            get { return _crewAprasialDetails; }
            set { _crewAprasialDetails = value; }
        }
    }
}
