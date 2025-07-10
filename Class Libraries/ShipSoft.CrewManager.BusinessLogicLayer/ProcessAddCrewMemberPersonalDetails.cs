using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessAddCrewMemberPersonalDetails : IBusinessLogic
    {
        private CrewMember _crewMember;

        public ProcessAddCrewMemberPersonalDetails()
        {

        }

        public void Invoke()
        {
            CrewMemberPersonalDetailsInsertData crewmemberdata = new CrewMemberPersonalDetailsInsertData();
            crewmemberdata.CrewMember = this.CrewMember;
            crewmemberdata.Add();
        }

        public CrewMember CrewMember
        {
            get { return _crewMember; }
            set { _crewMember = value; }
        }

    }
}
