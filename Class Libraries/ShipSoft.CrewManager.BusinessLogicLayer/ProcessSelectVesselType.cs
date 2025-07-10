using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectVesselType : IBusinessLogic
    {
        private DataSet _resultset;

        public ProcessSelectVesselType()
        {

        }

        public void Invoke()
        {
            VesselTypeSelectData crewmemberdata = new VesselTypeSelectData();
            ResultSet = crewmemberdata.Get();
        }

        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
