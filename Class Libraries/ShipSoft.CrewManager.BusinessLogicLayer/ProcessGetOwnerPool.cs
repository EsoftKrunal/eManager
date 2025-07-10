using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessGetOwnerPool : IBusinessLogic
    {
        private DataSet _result;

        public ProcessGetOwnerPool()
    {
 
    }
        public void Invoke()
        {
            OwnerPoolSelectData ownerpooldata = new OwnerPoolSelectData();
            ResultSet = ownerpooldata.Get();
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
