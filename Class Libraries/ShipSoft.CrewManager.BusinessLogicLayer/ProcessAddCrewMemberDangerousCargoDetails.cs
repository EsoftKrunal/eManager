using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessAddCrewMemberDangerousCargoDetails : IBusinessLogic
    {
       private CrewCargoDetails _crewCargoDetails;

       public ProcessAddCrewMemberDangerousCargoDetails()
        {

        }

        public void Invoke()
        {
            CrewMemberDangerousCargoDetailsInsertData obj = new CrewMemberDangerousCargoDetailsInsertData();
            obj.CrewCargoDetails = this.CrewCargoDetails;
            obj.Add();
        }

       public CrewCargoDetails CrewCargoDetails
        {
            get { return _crewCargoDetails; }
            set { _crewCargoDetails = value; }
        }
    }
}
