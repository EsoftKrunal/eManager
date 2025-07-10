using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Delete;
using ShipSoft.CrewManager.BusinessObjects; 

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessDeleteCrewCourseCertificateDetails:IBusinessLogic 
    {
          private CrewCourseCertificateDetails  _crewcoursecertificatedetails;

        public ProcessDeleteCrewCourseCertificateDetails()
        {

        }

        public void Invoke()
        {
            CrewCourseCertificateDetailsDeleteData crewmemberdata = new CrewCourseCertificateDetailsDeleteData();
            crewmemberdata.CertificateDetails = this.CertificateDetails;
            crewmemberdata.Delete();
        }

        public CrewCourseCertificateDetails CertificateDetails
        {
            get { return _crewcoursecertificatedetails; }
            set { _crewcoursecertificatedetails = value; }
        }
    }
}
