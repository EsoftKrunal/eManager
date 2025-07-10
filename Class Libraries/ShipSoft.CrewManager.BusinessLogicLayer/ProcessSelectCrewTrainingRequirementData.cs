using System;
using System.Collections.Generic;
using System.Text;
using System.Data; 
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectCrewTrainingRequirementData:IBusinessLogic 
    {
        private CrewTrainingRequirement _crewTrainingRequirement;
       private DataSet _result;
        public ProcessSelectCrewTrainingRequirementData()
        {

        }
        public void Invoke()
        {
            CrewTrainingRequirementSelectData crewtraining = new CrewTrainingRequirementSelectData();

            crewtraining.TrainingRequirement  = this.TrainingRequirement;
            ResultSet = crewtraining.Get(); ;
        }
        public CrewTrainingRequirement TrainingRequirement
        {
            get { return _crewTrainingRequirement; }
            set { _crewTrainingRequirement = value; }
        }
         public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
