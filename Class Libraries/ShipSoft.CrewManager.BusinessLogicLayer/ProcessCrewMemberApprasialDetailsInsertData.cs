using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessCrewMemberApprasialDetailsInsertData:IBusinessLogic 
    {
   private CrewApprasialDetails  _crewAprasialDetails;
        public ProcessCrewMemberApprasialDetailsInsertData()
       {
       }
       public void Invoke()
       {
           CrewMemberApprasialInsertData crewapprails = new CrewMemberApprasialInsertData();
           crewapprails.ApprasialDetails = this.ApprasialDetails;
           crewapprails.Add();
            
       }
       public CrewApprasialDetails ApprasialDetails
       {
           get { return _crewAprasialDetails; }
           set { _crewAprasialDetails = value; }
       }
    }
}
