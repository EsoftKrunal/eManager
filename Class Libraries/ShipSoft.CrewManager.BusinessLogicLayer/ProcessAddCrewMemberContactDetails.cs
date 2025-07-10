using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessAddCrewMemberContactDetails:IBusinessLogic
    {
       private CrewContact _crewcontact;
       public ProcessAddCrewMemberContactDetails()
       {
       }
       public void Invoke()
       {
           CrewMemberContactDetailsInsertData crewrecordcontactdata = new CrewMemberContactDetailsInsertData();
           crewrecordcontactdata.CrewContact=this.CrewContact;
           crewrecordcontactdata.Add();
       }
       public CrewContact CrewContact
       {
           get { return _crewcontact; }
           set { _crewcontact = value; }
       }

    }
}
