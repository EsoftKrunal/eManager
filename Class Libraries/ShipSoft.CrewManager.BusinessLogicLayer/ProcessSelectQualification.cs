using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectQualification : IBusinessLogic
    {
        private DataSet _resultset;

        public ProcessSelectQualification()
        {

        }

        public void Invoke()
        {
            QualificationSelectData crewmemberdata = new QualificationSelectData();
            ResultSet = crewmemberdata.Get();
        }

        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
