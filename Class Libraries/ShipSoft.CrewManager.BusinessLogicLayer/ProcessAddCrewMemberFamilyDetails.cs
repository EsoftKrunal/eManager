using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessAddCrewMemberFamilyDetails : IBusinessLogic
    {
       private FamilyDetails _familydetails;
       public ProcessAddCrewMemberFamilyDetails()
        {

        }

        public void Invoke()
        {
            CrewMemberFamilyDetailsInsertData insertfamilydetails = new CrewMemberFamilyDetailsInsertData();
            insertfamilydetails.FamilyDetails = this.FamilyDetails;
            insertfamilydetails.Add();
        }

       public FamilyDetails FamilyDetails
        {
            get { return _familydetails; }
            set { _familydetails = value; }
        }

    }
}
