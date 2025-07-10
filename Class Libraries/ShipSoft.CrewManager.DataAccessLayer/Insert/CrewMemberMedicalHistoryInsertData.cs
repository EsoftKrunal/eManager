using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common; 
using ShipSoft.CrewManager.BusinessObjects;


namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
   public class CrewMemberMedicalHistoryInsertData:DataAccessBase 
    {
       private CrewMemberMedicalHistory _crewMemberMedicalHistory;
       private CrewMemberMedicalHistoryInsertDataParqameters _crewMemberMedicalHistoryInsertDataParqameters;

        public CrewMemberMedicalHistoryInsertData()
       {
           StoredProcedureName = StoredProcedure.Name.InsertUpdateCrewMemberMedicalHistoryDetails.ToString();
       }
       public void Add()
       {
           _crewMemberMedicalHistoryInsertDataParqameters = new CrewMemberMedicalHistoryInsertDataParqameters(CrewMemberMedicalHistory);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           dbhelper.Parameters = _crewMemberMedicalHistoryInsertDataParqameters.Parameters;
           dbhelper.Run();
           //object id = dbhelper.RunScalar(base.ConnectionString, _crewrecordcontactInsertDataparameters.Parameters);
           //this.CrewContact.Id = Convert.ToInt32(id.ToString());
       }
        public CrewMemberMedicalHistory CrewMemberMedicalHistory
       {
           get { return _crewMemberMedicalHistory; }
           set { _crewMemberMedicalHistory = value; }
       }
   }

   public class CrewMemberMedicalHistoryInsertDataParqameters
   {
       private CrewMemberMedicalHistory _CrewMemberMedicalHistory;
       private SqlParameter[] _parameters;

       public CrewMemberMedicalHistoryInsertDataParqameters(CrewMemberMedicalHistory crewmembermedicalhistory)
       {
           CrewMemberMedicalHistory = crewmembermedicalhistory;
           Build();
       }
       private void Build()
       {
           SqlParameter[] parameters =
            {
                new SqlParameter("MedicalCaseId",CrewMemberMedicalHistory.MedicalCaseId),
                new SqlParameter("@CrewId",CrewMemberMedicalHistory.CrewId),
                new SqlParameter("@CaseDate", CrewMemberMedicalHistory.CaseDate) ,
                new SqlParameter("@VesselId", CrewMemberMedicalHistory.VesselId ) ,
                new SqlParameter("@PortId", CrewMemberMedicalHistory.PortId) ,
                new SqlParameter("@CaseNumber",CrewMemberMedicalHistory.CaseNumber),
                new SqlParameter("@CaseStatus",CrewMemberMedicalHistory.CaseStatus),
                new SqlParameter("@Amount",CrewMemberMedicalHistory.Amount),
                new SqlParameter("@Description",CrewMemberMedicalHistory.Description),
                new SqlParameter("@CreatedBy",CrewMemberMedicalHistory.CreatedBy),
                new SqlParameter("@ModifiedBy",CrewMemberMedicalHistory.Modifiedby),
               
            };

           Parameters = parameters;
       }
       public CrewMemberMedicalHistory CrewMemberMedicalHistory
       {
           get { return _CrewMemberMedicalHistory; }
           set { _CrewMemberMedicalHistory = value; }
       }

       public SqlParameter[] Parameters
       {
           get { return _parameters; }
           set { _parameters = value; }
       }

    }
}
