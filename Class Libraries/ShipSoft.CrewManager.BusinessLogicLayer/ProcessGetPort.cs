using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ShipSoft.CrewManager.DataAccessLayer.Select; 

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
  public class ProcessGetPort:IBusinessLogic
    {
         private DataSet _result;

      public ProcessGetPort()
    {
 
    }
        public void Invoke()
        {
            PortSelectData portselectdata = new PortSelectData();
            ResultSet = portselectdata.Get();
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
