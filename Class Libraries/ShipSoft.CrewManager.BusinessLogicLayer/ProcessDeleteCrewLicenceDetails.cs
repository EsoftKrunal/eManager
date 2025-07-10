using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Delete;
using ShipSoft.CrewManager.BusinessObjects; 

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
  public class ProcessDeleteCrewLicenceDetails:IBusinessLogic 
    {
           private CrewLicenseDetails   _crewlicencedetails;

        public ProcessDeleteCrewLicenceDetails()
        {

        }

        public void Invoke()
        {
            CrewLicenseDetailsDeleteData  crewmemberdata = new  CrewLicenseDetailsDeleteData ();
            crewmemberdata.LicenseDetails  = this.LicenseDetails;
            crewmemberdata.Delete();
        }

        public CrewLicenseDetails LicenseDetails
        {
            get { return _crewlicencedetails; }
            set { _crewlicencedetails = value; }
        }
    }
}
