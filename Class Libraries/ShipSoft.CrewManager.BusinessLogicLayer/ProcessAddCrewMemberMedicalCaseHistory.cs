using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessAddCrewMemberMedicalCaseHistory
    {
        private CrewMemberMedicalHistory _crewMemberMedicalHistory;
        public ProcessAddCrewMemberMedicalCaseHistory()
       {
       }
        public void Invoke()
        {
            CrewMemberMedicalHistoryInsertData medicalhistorydetails = new CrewMemberMedicalHistoryInsertData();
            medicalhistorydetails.CrewMemberMedicalHistory=this.CrewMemberMedicalHistory;
            medicalhistorydetails.Add() ;
        }
        public CrewMemberMedicalHistory CrewMemberMedicalHistory
       {
           get { return _crewMemberMedicalHistory; }
           set { _crewMemberMedicalHistory = value; }
       }
    }
}
