using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessAddCrewMemberExperienceDetails : IBusinessLogic
    {
       private ExperienceDetails _experiencedetails;
       public ProcessAddCrewMemberExperienceDetails()
        {

        }

        public void Invoke()
        {
            CrewMemberExperienceDetailsInsertData insertexperiencedetails = new CrewMemberExperienceDetailsInsertData();
            insertexperiencedetails.ExperienceDetails = this.ExperienceDetails;
            insertexperiencedetails.Add();
        }

       public ExperienceDetails ExperienceDetails
        {
            get { return _experiencedetails; }
            set { _experiencedetails = value; }
        }

    }
}
