using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.DataAccessLayer.Delete;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessDeleteCrewMemberByDangerousCargoId : IBusinessLogic
    {
       private CrewCargoDetails _crewCargoDetails;

       public ProcessDeleteCrewMemberByDangerousCargoId()
        {

        }

        public void Invoke()
        {
            CrewMemberDangerousCargoDetailsDeleteData obj = new CrewMemberDangerousCargoDetailsDeleteData();
            obj.CrewCargoDetails = this.CrewCargoDetails;
            obj.Delete();
        }

       public CrewCargoDetails CrewCargoDetails
        {
            get { return _crewCargoDetails; }
            set { _crewCargoDetails = value; }
        }
    }
}
