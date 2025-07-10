using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
  public  class ProcessCrewCourseCertificateInsertData
    {
        private CrewCourseCertificateDetails _crewcoursecertificatedetails;
        public ProcessCrewCourseCertificateInsertData()
       {
       }
       public void Invoke()
       {
           CrewCourseCetificateDetailsInsetData crewcertificatedata = new CrewCourseCetificateDetailsInsetData();
           crewcertificatedata.CertificateDetails = this.certificatedetails;
           crewcertificatedata.Add();
            
       }
        public CrewCourseCertificateDetails certificatedetails
       {
           get { return _crewcoursecertificatedetails; }
           set { _crewcoursecertificatedetails = value; }
       }
    }
}
