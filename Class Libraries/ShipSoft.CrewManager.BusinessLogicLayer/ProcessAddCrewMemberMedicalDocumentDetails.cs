using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessAddCrewMemberMedicalDocumentDetails:IBusinessLogic
    {
        private MedicalDetails _medicaldetails;
       public ProcessAddCrewMemberMedicalDocumentDetails()
        {
        }
        public void Invoke()
        {
            CrewMemberMedicalDocumentDetailsInsertData crewmembermedicaldetailsinsertdata = new CrewMemberMedicalDocumentDetailsInsertData();
            crewmembermedicaldetailsinsertdata.MedicalDetails = this.MedicalDetails;
            crewmembermedicaldetailsinsertdata.Add();
        }
        public MedicalDetails MedicalDetails
        {
            get { return _medicaldetails; }
            set { _medicaldetails = value; }
        }
    }
}
