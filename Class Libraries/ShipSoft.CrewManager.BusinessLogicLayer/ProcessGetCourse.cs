using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessGetCourse:IBusinessLogic
    {
        private DataSet _resultset;
        public ProcessGetCourse()
        {
        }
        public void Invoke()
        {
            CourseNameSelectData countrynameselectdata = new CourseNameSelectData();
            ResultSet = countrynameselectdata.Get();
        }
        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
