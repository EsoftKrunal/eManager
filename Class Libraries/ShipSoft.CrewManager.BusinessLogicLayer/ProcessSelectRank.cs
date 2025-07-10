using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectRank : IBusinessLogic
    {
        private DataSet _resultset;

        public ProcessSelectRank()
        {

        }

        public void Invoke()
        {
            RankSelectData crewmemberdata = new RankSelectData();
            ResultSet = crewmemberdata.Get();
        }

        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
