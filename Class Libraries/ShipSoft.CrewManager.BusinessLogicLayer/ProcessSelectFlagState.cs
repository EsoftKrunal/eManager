using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectFlagState :IBusinessLogic
    {
       private DataSet _result;

       public ProcessSelectFlagState()
       {

       }
       
        public void Invoke()
        {
            FlagStateSelectData countryselectdata = new FlagStateSelectData();
            ResultSet = countryselectdata.Get();
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
