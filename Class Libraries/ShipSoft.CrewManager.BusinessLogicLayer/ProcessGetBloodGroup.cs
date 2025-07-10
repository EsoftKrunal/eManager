using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessGetBloodGroup:IBusinessLogic 
    {
        private DataSet _resultset;
       public ProcessGetBloodGroup()
        {
        }
        public void Invoke()
        {
            SelectBloodGroup bloodgroupselect = new SelectBloodGroup();
            ResultSet = bloodgroupselect.Get();
        }
        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
