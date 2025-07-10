using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
  public class ProcessGetRecruitingOffice : IBusinessLogic
    {
        private DataSet _result;

        public ProcessGetRecruitingOffice()
    {
 
    }
        public void Invoke()
        {
            RecruitingOfficeSelectData recruitingofficeselectdata = new RecruitingOfficeSelectData();
            ResultSet = recruitingofficeselectdata.Get();
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
