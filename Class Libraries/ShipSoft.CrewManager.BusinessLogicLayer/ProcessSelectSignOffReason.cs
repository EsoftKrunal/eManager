using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectSignOffReason : IBusinessLogic
    {
        private DataSet _resultset;

        public ProcessSelectSignOffReason()
        {

        }

        public void Invoke()
        {
            SignOffReasonSelectData crewmemberdata = new SignOffReasonSelectData();
            ResultSet = crewmemberdata.Get();
        }

        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
