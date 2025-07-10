using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;


namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessGetManningPool : IBusinessLogic
    {
        private DataSet _result;

        public ProcessGetManningPool()
    {
 
    }
        public void Invoke()
        {
            ManningPoolSelectData manningpooldata = new ManningPoolSelectData();
            ResultSet = manningpooldata.Get();
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
