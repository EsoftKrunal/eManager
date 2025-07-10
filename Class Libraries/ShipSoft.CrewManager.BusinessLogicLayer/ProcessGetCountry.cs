using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessGetCountry :IBusinessLogic
    {
       private DataSet _result;

       public ProcessGetCountry()
       {

       }
       
        public void Invoke()
        {
            CountrySelectData countryselectdata = new CountrySelectData();
            ResultSet = countryselectdata.Get();
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
