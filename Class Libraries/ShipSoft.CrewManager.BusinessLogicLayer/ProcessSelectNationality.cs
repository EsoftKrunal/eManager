using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectNationality : IBusinessLogic
    {
        private DataSet _resultset;

        public ProcessSelectNationality()
        {

        }

        public void Invoke()
        {
            NationalitySelectData crewmemberdata = new NationalitySelectData();
            ResultSet = crewmemberdata.Get();
        }

        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
