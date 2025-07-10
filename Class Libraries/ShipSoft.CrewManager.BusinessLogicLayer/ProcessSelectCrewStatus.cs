using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectCrewStatus : IBusinessLogic
    {
        private DataSet _resultset;

        public ProcessSelectCrewStatus()
        {

        }

        public void Invoke()
        {
            CrewStatusSelectData crewmemberdata = new CrewStatusSelectData();
            ResultSet = crewmemberdata.Get();
        }

        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
