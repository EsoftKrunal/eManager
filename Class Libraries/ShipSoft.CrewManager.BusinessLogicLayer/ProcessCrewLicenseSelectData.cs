using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data; 
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessCrewLicenseSelectData:IBusinessLogic 
    {
         private CrewLicenseDetails  _crewLicenseDetails;
        private DataSet  _result;
        public ProcessCrewLicenseSelectData()
        {

        }
        public void Invoke()
        {
            CrewLicenceDetailsSelectData  crewlicensedetails = new CrewLicenceDetailsSelectData();
            crewlicensedetails.crewlicensedetails = this.LicenseDetails;
            ResultSet = crewlicensedetails.Get();
        }
        public CrewLicenseDetails LicenseDetails
        {
            get { return _crewLicenseDetails; }
            set { _crewLicenseDetails = value; }
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
