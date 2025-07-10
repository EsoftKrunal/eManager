using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
   public class CrewTrainingRequirementInsertData:DataAccessBase 
    {
        private CrewTrainingRequirement _crewTrainingRequirement;
        private CrewTrainingRequirementInsetDataParameters _crewTrainingRequirementinsetdataparameters;
        public CrewTrainingRequirementInsertData()
       {
           StoredProcedureName = StoredProcedure.Name.InsertUpdateCrewTrainingRequirement.ToString();
       }
       public void Add()
       {
           CrewTrainingRequirementInsetDataParameters _crewTrainingRequirementinsetdataparameters = new CrewTrainingRequirementInsetDataParameters(TrainingRequirement);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           object id = dbhelper.RunScalar(base.ConnectionString, _crewTrainingRequirementinsetdataparameters.Parameters);

       }
        public CrewTrainingRequirement TrainingRequirement
       {
           get { return _crewTrainingRequirement; }
           set { _crewTrainingRequirement = value; }
       }
       public class CrewTrainingRequirementInsetDataParameters
       {
           private CrewTrainingRequirement _crewTrainingRequirement;
           private SqlParameter[] _parameters;
           public CrewTrainingRequirementInsetDataParameters(CrewTrainingRequirement treq)
           {
               this.TrainingRequirement = treq;
             
               Build();
           }
           private void Build()
           {
               SqlParameter[] parameters =
            {
                new SqlParameter("@TrainingRequirementId",TrainingRequirement.TrainingRequirementId),
                new SqlParameter("@CrewId",TrainingRequirement.CrewId),
                new SqlParameter("@TrainingId",TrainingRequirement.TrainingId),
                new SqlParameter("@Remark",TrainingRequirement.Remark),
                new SqlParameter("@CreatedBy",TrainingRequirement.CreatedBy),
                new SqlParameter("@ModifiedBy",TrainingRequirement.ModifiedBy),
                new SqlParameter("@N_DueDate",TrainingRequirement.N_DueDate),
                new SqlParameter("@N_CrewTrainingStatus",TrainingRequirement.N_CrewTrainingStatus),
                new SqlParameter("@N_CrewAppraisalId",TrainingRequirement.N_CrewAppraisalId),
                new SqlParameter("@N_CrewVerified",TrainingRequirement.N_CrewVerified),
                
            };
               Parameters = parameters;
           }
           public CrewTrainingRequirement TrainingRequirement
           {
               get { return _crewTrainingRequirement; }
               set { _crewTrainingRequirement = value; }
           }

           public SqlParameter[] Parameters
           {
               get { return _parameters; }
               set { _parameters = value; }
           }
       }
    }
}
