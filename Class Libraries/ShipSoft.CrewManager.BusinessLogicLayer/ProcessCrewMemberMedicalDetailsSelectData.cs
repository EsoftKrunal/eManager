using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Select;
using System.Data; 
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
  public  class ProcessCrewMemberMedicalDetailsSelectData:IBusinessLogic 
    {
      private DataSet _result;
      private CrewMemberMedicalHistory _crewMemberMedicalcaseHistory;

      public ProcessCrewMemberMedicalDetailsSelectData()
       {

       }
       
        public void Invoke()
        {
            CrewMemberMedicalcaseHistorySelectData crewmedicaldata = new CrewMemberMedicalcaseHistorySelectData();
            crewmedicaldata.Medicaldetails = crewmedicalhistory;
           // ResultSet = crewmedicaldata.GetMedicalCaseHistoryDetail();
            ResultSet = crewmedicaldata.Get();
        }
      public CrewMemberMedicalHistory crewmedicalhistory
      {
          get { return _crewMemberMedicalcaseHistory; }
          set { _crewMemberMedicalcaseHistory = value; }
      }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
