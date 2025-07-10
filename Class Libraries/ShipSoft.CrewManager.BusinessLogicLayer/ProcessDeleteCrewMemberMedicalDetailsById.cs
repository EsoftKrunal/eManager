using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.DataAccessLayer.Delete;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessDeleteCrewMemberMedicalDetailsById : IBusinessLogic
    {
         private MedicalDetails _MedicalDetails;

        public ProcessDeleteCrewMemberMedicalDetailsById()
        {

        }

        public void Invoke()
        {
            CrewMemberMedicalDetailsDeleteData crewmemberdata = new CrewMemberMedicalDetailsDeleteData();
            crewmemberdata.MedicalDetails = this.MedicalDetails;
            crewmemberdata.Delete();
        }

        public MedicalDetails MedicalDetails
        {
            get { return _MedicalDetails; }
            set { _MedicalDetails = value; }
        }
    }
}
