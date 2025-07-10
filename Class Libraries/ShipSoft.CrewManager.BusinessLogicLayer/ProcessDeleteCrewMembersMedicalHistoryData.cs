using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Delete;
using ShipSoft.CrewManager.BusinessObjects; 


namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessDeleteCrewMembersMedicalHistoryData
    {
        private CrewMemberMedicalHistory _CrewMemberMedicalHistory;

        public ProcessDeleteCrewMembersMedicalHistoryData()
        {

        }

        public void Invoke()
        {
            CrewMemberMedicalHistoryDetailsDeleteData crewmemberdata = new CrewMemberMedicalHistoryDetailsDeleteData();
            crewmemberdata.MedicalDetails = this.CrewMemberMedicalHistory;
            crewmemberdata.Delete();
        }

        public CrewMemberMedicalHistory CrewMemberMedicalHistory
        {
            get { return _CrewMemberMedicalHistory; }
            set { _CrewMemberMedicalHistory = value; }
        }
    }
}
