using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
  public class ProcessGetVesselPool : IBusinessLogic
    {
        private DataSet _result;

        public ProcessGetVesselPool()
    {
 
    }
        public void Invoke()
        {
            VesselSelectData vesselselectdata = new VesselSelectData();
            ResultSet = vesselselectdata.Get();
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
