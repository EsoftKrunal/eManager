using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;


namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessCrewLicenseInsertData:IBusinessLogic 
    {
       private CrewLicenseDetails _crewLicenseDetails;
        public ProcessCrewLicenseInsertData()
       {
       }
       public void Invoke()
       {
           CrewLicenseInsertData crewlicensedata = new CrewLicenseInsertData();
           crewlicensedata.LicenseDetails = this.Licensedetails;
           crewlicensedata.Add();
            
       }
       public CrewLicenseDetails Licensedetails
       {
           get { return _crewLicenseDetails; }
           set { _crewLicenseDetails = value; }
       }
    }
}
