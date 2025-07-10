using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectCrewMemberPrimaryDetails_Search : IBusinessLogic
    {
        private BoCrewSearch _crewmember;
        private DataSet _resultset;

        public ProcessSelectCrewMemberPrimaryDetails_Search()
        {

        }

        public void Invoke()
        {
            CrewMemberPrimaryDetailsSelectData_Search selectcrewmember = new CrewMemberPrimaryDetailsSelectData_Search();
            selectcrewmember.CrewSearch = CrewSearch;
            ResultSet = selectcrewmember.Get();
        }

        public BoCrewSearch CrewSearch
        {
            get { return _crewmember; }
            set { _crewmember = value; }
        }

        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
