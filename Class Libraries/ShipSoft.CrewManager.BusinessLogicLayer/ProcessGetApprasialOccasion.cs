using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;


namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    
  public class ProcessGetApprasialOccasion:IBusinessLogic 
    {
      private DataSet _result;
        public void Invoke()
        {
            ApprasialSelectData apprasialdata = new ApprasialSelectData();
            ResultSet = apprasialdata.Get();
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
