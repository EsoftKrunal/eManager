using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
    public class CrewTrainingRequirementDeleteData:DataAccessBase 
    {
        private CrewTrainingRequirement _crewtrainingrequirement;
        private CrewTrainingRequirementDeleteDataParameters _crewtrainingrequirementdeletedataparameters;

        public CrewTrainingRequirementDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCrewTrainingRequirementById.ToString();
        }
        public void Delete()
        {
            _crewtrainingrequirementdeletedataparameters = new CrewTrainingRequirementDeleteDataParameters(TrainingRequirement);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewtrainingrequirementdeletedataparameters.Parameters;
            dbhelper.Run();
        }
       public CrewTrainingRequirement TrainingRequirement
        {
            get { return _crewtrainingrequirement; }
            set { _crewtrainingrequirement = value; }
        }
    }
     public class CrewTrainingRequirementDeleteDataParameters
    {
         private CrewTrainingRequirement _crewtrainingrequirement;
        private SqlParameter[] _parameters;

         public CrewTrainingRequirementDeleteDataParameters(CrewTrainingRequirement ctr)
        {
            this.TrainingRequirement = ctr;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
		        new SqlParameter( "@TrainingRequirementId"  , TrainingRequirement.TrainingRequirementId )
		    };

            Parameters = parameters;
        }

         public CrewTrainingRequirement TrainingRequirement
         {
             get { return _crewtrainingrequirement; }
             set { _crewtrainingrequirement = value; }
         }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
