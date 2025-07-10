using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessCrewTrainingRequirementInsertData:IBusinessLogic 
    {
         private CrewTrainingRequirement  _crewTrainingRequirement;
        public ProcessCrewTrainingRequirementInsertData()
       {
       }
       public void Invoke()
       {
           CrewTrainingRequirementInsertData crewtrainingdata = new CrewTrainingRequirementInsertData();

           crewtrainingdata.TrainingRequirement = this.TrainingRequirement;
           crewtrainingdata.Add ();
            
       }
       public CrewTrainingRequirement TrainingRequirement
       {
           get { return _crewTrainingRequirement; }
           set { _crewTrainingRequirement = value; }
       }
    }
}
