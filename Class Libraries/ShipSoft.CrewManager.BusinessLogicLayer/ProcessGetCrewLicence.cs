using System;
using System.Collections.Generic;
using System.Text;
using System.Data; 
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessGetCrewLicence:IBusinessLogic 
    {
         private CrewLicenseDetails  _crewlicensedetails;
       private DataSet _result;
        public ProcessGetCrewLicence()
        {

        }
        public void Invoke()
        {
            CrewLicenseSelectData crewlicense = new CrewLicenseSelectData();
            crewlicense.LicenseDetails = this.LicenseDetails;
            ResultSet = crewlicense.Get(); ;
        }
        public CrewLicenseDetails LicenseDetails
       {
           get { return _crewlicensedetails; }
           set { _crewlicensedetails = value; }
       }
         public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
