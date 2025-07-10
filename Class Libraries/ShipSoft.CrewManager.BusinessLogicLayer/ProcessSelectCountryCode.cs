using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectCountryCode : IBusinessLogic
    {   
        private int _CountryId;
        private string _CountryCode;

        public int CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
        public ProcessSelectCountryCode()
        {

        }
        public void Invoke()
        {
            SelecttCountryCode cc = new SelecttCountryCode();
            cc.CountryId = CountryId;
            CountryCode = cc.CountryCode();
        }
    }
}
