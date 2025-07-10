using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Delete;
using ShipSoft.CrewManager.BusinessObjects; 
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
  public  class ProcessDeleteCrewTrainingRequirement:IBusinessLogic 
    {
  private CrewTrainingRequirement  _crewTrainingRequirement;
      public ProcessDeleteCrewTrainingRequirement()
        {

        }

        public void Invoke()
        {
            CrewTrainingRequirementDeleteData crewdeletedata = new CrewTrainingRequirementDeleteData();
            crewdeletedata.TrainingRequirement  = this.TrainingRequirement;
            crewdeletedata.Delete();
        }

       public CrewTrainingRequirement TrainingRequirement
       {
           get { return _crewTrainingRequirement; }
           set { _crewTrainingRequirement = value; }
       }
    }
}
